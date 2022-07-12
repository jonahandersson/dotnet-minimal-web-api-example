using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MinimalAPIDemo.Functions.Entities;

namespace MinimalAPIDemo.Functions
{
    public static class GetCustomersOrchestration
    {
        [FunctionName("GetCustomer_Orchestration")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var customers = new List<Customer>();
            var printedCustomerNames = new List<string>();
            //TODO Get customer data from the minimal API 

            foreach (var cx in customers)
            {
                printedCustomerNames.Add(await context.CallActivityAsync<string>("GetCustomers_Print", cx));
            }

            // returns list of customers from Minimal API after retrieval and print 
            return printedCustomerNames;
        }

        [FunctionName("GetCustomers_Print")]
        public static string SayHello([ActivityTrigger] Customer customer, ILogger log)
        {
            log.LogInformation($"Saying hello to {customer.FirstName + " " + customer.LastName }.");
            var customerFullName = customer.FirstName + " " + customer.LastName;
            return $"Hello {customerFullName}!";
        }

        [FunctionName("GetCustomers_HttpFunction")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("GetCustomer_Orchestration", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}