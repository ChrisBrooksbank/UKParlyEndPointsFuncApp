using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UkParlyEndPointsFuncApp
{
    public static class FunctionCheck
    {
        private const string TimerSchedule = "*/20 * * * *";

        [FunctionName("Check")]
        public static async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully";
            responseMessage += "\n See repo at : <a href=\"https://github.com/ChrisBrooksbank/UKParlyEndPointsFuncApp\">https://github.com/ChrisBrooksbank/UKParlyEndPointsFuncApp</a>";

            return new ContentResult
            {
                Content = responseMessage,
                ContentType = "text/html",
                StatusCode = 200
            };
        }

        [FunctionName("CheckTimer")]
        public static async Task RunTimer(
            [TimerTrigger(TimerSchedule)] TimerInfo myTimer,
            ILogger log)
        {
            await ExecuteCheck(log);
        }

        private static async Task ExecuteCheck(ILogger log)
        {
            string responseMessage = "This timer triggered function executed successfully every 20 minutes.";
            responseMessage += "\n See repo at : <a href=\"https://github.com/ChrisBrooksbank/UKParlyEndPointsFuncApp\">https://github.com/ChrisBrooksbank/UKParlyEndPointsFuncApp</a>";

            log.LogInformation(responseMessage);
            await Task.CompletedTask;
        }
    }
}
