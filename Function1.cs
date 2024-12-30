using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    { 
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDBInput( databaseName: "algira",
        containerName: "alfita",
        Connection  = "CosmosDBConnection",
        Id = "2",
        PartitionKey = "2")] Resume toDoItem )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            if (toDoItem == null)
            {
                _logger.LogInformation($"ToDo item not found");
            }
            else
            {
                _logger.LogInformation($"Found ToDo item, Description={toDoItem.Education}");
            }
            return new OkObjectResult(toDoItem);
        }
    }
}
