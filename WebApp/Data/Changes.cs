using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChangeHistoryWebApp
{

    public class Changes
    {
        public async Task<List<ChangeProperties>> GetChangeProperties(string subscriptionId)
        {
        string azureFunctionAddress = Environment.GetEnvironmentVariable("AzureFunctionAddress");
        HttpClient client = new HttpClient();
        string URI = $"{azureFunctionAddress}&subscriptionId={subscriptionId}";
        HttpResponseMessage response = await client.GetAsync(URI);

        var detectedChanges = JsonConvert.DeserializeObject<List<ChangeProperties>>(
            await response.Content.ReadAsStringAsync());
            return detectedChanges;
        }
    }
}