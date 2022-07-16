using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using AdventureWorksMinimalAPIDemo.EntityClasses;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace MinimalAPIDemo.Functions.Activities
{
    public static class GetCustomersFromMinimalAPI
    {
        [FunctionName("GetCustomersFromMinimalAPIEndpoint")]
        public static List<Customer> GetCustomerFromMinimalAPI([ActivityTrigger] ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"Getting the list of customers from Minimal API.");
            
            try
            {
                //TODO Get Customers json data from minimal api 
                var localConfig = new ConfigurationBuilder()
                       .SetBasePath(executionContext.FunctionAppDirectory)
                       .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables()
                       .Build();

                var minimalAPIEndpointUrl = localConfig["LocalApiEndpointUrl"];
                List<Customer> customers;

                //Reading JSON data from Minimimal API using HttpWebClient approach
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(minimalAPIEndpointUrl);
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/customer").Result;
                    response.EnsureSuccessStatusCode();
                    customers = response.Content.ReadAsAsync<List<Customer>>().Result;
                }

                customers.ForEach(Console.WriteLine);

                //TODO Conversion Json to Customer class 
                return customers;
            }
            catch (Exception)
            {
                //TODO error handling
                throw;
            }

         
        }
    }
}
