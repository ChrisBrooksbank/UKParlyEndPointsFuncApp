using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingAllOnTimer
    {
        private IFunctionServices _functionServices;

        [FunctionName("PingAllOnTimer")]
        public async Task Run([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                _functionServices = new FunctionServices();
                var pingResults = await _functionServices.PingAll();
                foreach (var result in pingResults)
                {
                    log.LogInformation(result);
                }
            }
            catch (Exception ex)
            {
                log.LogError($"error : {ex.Message}");
            }
        }
    }
}
