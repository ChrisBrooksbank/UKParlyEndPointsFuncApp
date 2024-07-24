using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingAll
    {
        private IFunctionServices _functionServices;

        private const string TimerSchedule = "0 0 6-22 * * 1-6";

        [FunctionName("PingAll")]
        public async Task Run([TimerTrigger(TimerSchedule)]TimerInfo myTimer, ILogger log)
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
