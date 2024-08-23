using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingAll
    {
        private IFunctionServices _functionServices;

        private const string TimerSchedule = "0 6 * * 1";

        [FunctionName("PingAll")]
        public async Task Run([TimerTrigger(TimerSchedule)] TimerInfo myTimer, ILogger log)
        {
            await ExecutePingAll(log);
        }

        [FunctionName("PingAllHttp")]
        public async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await ExecutePingAll(log);
            return new OkObjectResult("PingAll executed successfully");
        }

        private async Task ExecutePingAll(ILogger log)
        {
            try
            {
                _functionServices = new FunctionServices();
                await _functionServices.PingAll();
            }
            catch (Exception ex)
            {
                log.LogError($"error : {ex.Message}");
            }
        }
    }
}
