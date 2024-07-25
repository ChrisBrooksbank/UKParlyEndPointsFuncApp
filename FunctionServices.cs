﻿using System;
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
        
        const string GetEndpointsPath = $"ParliamentEndpoint/endpoints?Skip=0&Take=500";

        public async Task<List<string>> PingAll()
        {
            return await PingEndpoints(endpoint => true);
        }

        public async Task<List<string>> PingNewOrFailedEndpoints()
        {
            return await PingEndpoints(endpoint => !endpoint.PingStatus.Equals("200"));
        }

        private async Task<List<string>> PingEndpoints(Func<EndpointData, bool> predicate)
        {
            try
            {
                var response = await httpClient.GetStringAsync(GetEndpointsPath);
                var endpointDataList = JsonConvert.DeserializeObject<List<EndpointData>>(response);

                var pingResults = new List<string>();
                foreach (var endpoint in endpointDataList)
                {
                    if (predicate(endpoint))
                    {
                        var pingUrl = $"ParliamentEndpoint/endpoints/{endpoint.Id}/ping";
                        var pingResponse = await httpClient.PostAsync(pingUrl, null);
                        var result = $"Endpoint {endpoint.Id}: Ping Status {pingResponse.StatusCode}";
                        pingResults.Add(result);
                    }
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
