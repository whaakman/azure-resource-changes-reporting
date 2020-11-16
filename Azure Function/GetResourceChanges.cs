using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace ChangeHistory
{
    public static class GetResourceChanges
    {
        [FunctionName("GetResourceChanges")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Wait wut? Let me get resource changes");

            string subscriptionId = req.Query["subscriptionId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            subscriptionId = subscriptionId ?? data?.subscriptionId;

            string responseMessage = string.IsNullOrEmpty(subscriptionId)
                ? "Please pass a subscriptionId"
                : $"Tried to check changes for {subscriptionId}";

           
            var APICall = new APICall();
           
            var accessToken = await APICall.GetToken();

            List<ChangeProperties> ChangeProperties = await APICall.GetChanges(accessToken, subscriptionId);
          
            return new OkObjectResult(ChangeProperties);
        }
    }
            
 }
 
