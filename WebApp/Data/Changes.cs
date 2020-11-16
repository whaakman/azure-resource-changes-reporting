using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChangeHistoryWebApp
{

public class Changes
{
    public List<ChangeProperties> x;
    public Changes()
    {
        var x = new List<ChangeProperties>(){
        };
            
    }
    public async Task<List<ChangeProperties>> GetChangeProperties()
    {

    string azureFunctionAddress = Environment.GetEnvironmentVariable("AzureFunctionAddress");
    string subscriptionId = "69dc95d8-087e-4810-910e-526a1844217b";
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(azureFunctionAddress + subscriptionId);

     var detectedChanges = JsonConvert.DeserializeObject<List<ChangeProperties>>(
        await response.Content.ReadAsStringAsync());
        return detectedChanges;
    }
}

}