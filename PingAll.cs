using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UkParlyEndPointsFuncApp
{
    public class PingAll
    {
        private IFunctionServices _functionServices;

        [FunctionName("PingAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                _functionServices = new FunctionServices();
                var pingResults = await _functionServices.PingAll();
                foreach (var result in pingResults)
                {
                    log.LogInformation(result);
                }
                return new OkObjectResult("PingAll function executed successfully.");
            }
            catch (Exception ex)
            {
                log.LogError($"error : {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
