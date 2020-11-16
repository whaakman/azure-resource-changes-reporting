using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChangeHistory
{
    public class APICall
    {
   
    public static async Task<List<ChangeProperties>> GetChanges(string token, string subscriptionId)
        {
            // 
            var httpClient = await APICall.HTTPClient();
            List<ChangeProperties> ChangeProperties = new List<ChangeProperties>();

            // Store the resourceIds in List "resourceIds"
            List<string> resourceIds = await APICall.GetAllResourceIds(subscriptionId);

            // Set Start and End date for changes (max 14 as ARG only stores history for 14 days)
            var startDateTime = DateTime.Now.AddDays(-14).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'").ToString();
            var endDateTime = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'").ToString();

            // URI of the Resource Changes API
            string URI = "https://management.azure.com/providers/Microsoft.ResourceGraph/resourceChanges?api-version=2018-09-01-preview";

            foreach (var resourceId in resourceIds)
            {

                // Build the body for the POST
                string str = $"{{ 'resourceId': '{resourceId}', 'interval': {{ 'start': '{startDateTime}', 'end': '{endDateTime}' }}, 'fetchPropertyChanges': true }}";
                var json  = JsonConvert.DeserializeObject(str);

                // Perform POST, store results in HttpsResponse
                HttpResponseMessage responsePost = await httpClient.PostAsJsonAsync(URI, json);
                var HttpsResponse = await responsePost.Content.ReadAsStringAsync();

                // Store values in List
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(HttpsResponse);

                // Foreach detected change get the changed properties object and filter out "System"
                // Currently only interested in changes made by users

                foreach (var detectedChange in myDeserializedClass.Changes)
                {
                    foreach (var propertyChange in detectedChange.PropertyChanges)
                    {
                        if (propertyChange.ChangeCategory != "System")
                        {
                            var timestamp = detectedChange.AfterSnapshot.Timestamp;
                            ChangeProperties.Add(new ChangeProperties(resourceId, propertyChange.PropertyName, propertyChange.BeforeValue, propertyChange.AfterValue, propertyChange.ChangeCategory, detectedChange.AfterSnapshot.Timestamp.ToString()));
                        }
                    }
                }

            }        
        
            return ChangeProperties;         

        }

        public static async Task<List<string>> GetAllResourceIds(string subscriptionId)
        {
            var httpClient = await APICall.HTTPClient();
            // Not all resources report a clean history and perhaps you're not interested in all resources
            // Get all Resource Ids for resources with Tag "ReportChanges" and tagValue "true"
            string URI = $"https://management.azure.com/subscriptions/{subscriptionId}/resources?$filter=tagName eq 'ReportChanges' and tagValue eq 'true'&api-version=2020-06-01";
            
            HttpResponseMessage responsePost = await httpClient.GetAsync(URI);
            var HttpsResponse = await responsePost.Content.ReadAsStringAsync();

            // Parse resourceIds in JObject
            JObject results = JObject.Parse(HttpsResponse);

            // Store all resource Ids in list
            var resourceIds =
                    from id in results["value"]
                    select (string)id["id"];    

            List<string> resourceIdsList = new List<string>();
            foreach (string item in resourceIds )
            {
                resourceIdsList.Add(item);
        
            }
            // Return the List
            return resourceIdsList;
        }
        
        public static async Task<string> GetToken()
        {
             // Get the access token from the managed identity
              var azureServiceTokenProvider = new AzureServiceTokenProvider();
              string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://management.azure.com");

              return accessToken;
        }
         public static async Task<HttpClient> HTTPClient()
        {
            // Get the access token
            var token = await APICall.GetToken();
            // Creat the HTTP Client
            var httpClient = new HttpClient();

            // Create the Headers
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return httpClient;

        }
    }
  
}