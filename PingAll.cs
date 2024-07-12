using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UkParlyEndPointsFuncApp
{
    public static class PingAll
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://ukparliamentendpoints-services.azurewebsites.net")
        };
        
        const string GetEndpointsPath = $"ParliamentEndpoint/endpoints?Skip=0&Take=100";

        [FunctionName("PingAll")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var response = await httpClient.GetStringAsync(GetEndpointsPath);
                var endpointDataList = JsonConvert.DeserializeObject<List<EndpointData>>(response);
            
                var pingResults = new List<string>();
                foreach (var endpoint in endpointDataList)
                {
                    var pingUrl = $"ParliamentEndpoint/endpoints/{endpoint.Id}/ping";
                    var pingResponse = await httpClient.PostAsync(pingUrl, null);
                    var result = $"Endpoint {endpoint.Id}: Ping Status {pingResponse.StatusCode}";
                    pingResults.Add(result);
                    log.LogInformation(result);
                }
                log.LogInformation("C# trigger function processed pingAll request.");
                return new OkObjectResult(string.Join("\n", pingResults));
            }
            catch (Exception ex)
            {
                log.LogError($"error : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
