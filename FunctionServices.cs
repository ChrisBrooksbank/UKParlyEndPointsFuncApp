using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UkParlyEndPointsFuncApp
{
    public class FunctionServices : IFunctionServices
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://ukparliamentendpoints-services.azurewebsites.net")
        };
        
        const string GetAllEndpointsPath = $"ParliamentEndpoint/endpoints?Skip=0&Take=500";
        const string GetNewOrFailedEndpointsPath = $"ParliamentEndpoint/endpoints?NewOrFailed=true&Skip=0&Take=500";

        public async Task<List<string>> PingAll()
        {
            return await PingEndpoints(GetAllEndpointsPath);
        }

        public async Task<List<string>> PingNewOrFailedEndpoints()
        {
            return await PingEndpoints(GetNewOrFailedEndpointsPath);
        }

        private async Task<List<string>> PingEndpoints(string getEndpointsPath)
        {
            try
            {
                var response = await httpClient.GetStringAsync(getEndpointsPath);
                var endpointDataList = JsonConvert.DeserializeObject<List<EndpointData>>(response);

                var pingResults = new List<string>();
                foreach (var endpoint in endpointDataList)
                {
                    var pingUrl = $"ParliamentEndpoint/endpoints/{endpoint.Id}/ping";
                    var pingResponse = await httpClient.PostAsync(pingUrl, null);
                    var result = $"Endpoint {endpoint.Id}: Ping Status {pingResponse.StatusCode}";
                    pingResults.Add(result);
                }

                return pingResults;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
        }
    }
}
