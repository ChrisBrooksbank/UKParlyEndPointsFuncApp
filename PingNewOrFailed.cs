using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingNewOrFailed
    {
        private IFunctionServices _functionServices;

        private const string TimerSchedule = "0 6 * *";

        [FunctionName("PingNewOrFailed")]
        public async Task Run([TimerTrigger(TimerSchedule)] TimerInfo myTimer, ILogger log)
        {
            await ExecutePingNewOrFailed(log);
        }

        [FunctionName("PingNewOrFailedHttp")]
        public async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await ExecutePingNewOrFailed(log);
            return new OkObjectResult("PingNewOrFailed executed successfully");
        }

        private async Task ExecutePingNewOrFailed(ILogger log)
        {
            try
            {
                _functionServices = new FunctionServices();
                await _functionServices.PingNewOrFailedEndpoints();
            }
            catch (Exception ex)
            {
                log.LogError($"error : {ex.Message}");
            }
        }
    }
}
