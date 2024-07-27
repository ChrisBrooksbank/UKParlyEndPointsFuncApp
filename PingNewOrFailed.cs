using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingNewOrFailed
    {
        private IFunctionServices _functionServices;

      private const string TimerSchedule =  "0 6 * *";

        [FunctionName("PingNewOrFailed")]
        public async Task Run([TimerTrigger(TimerSchedule)]TimerInfo myTimer, ILogger log)
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
