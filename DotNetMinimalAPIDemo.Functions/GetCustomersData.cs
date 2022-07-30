using System;
using System.Collections.Generic;
using DotNetMinimalAPIDemo.Functions.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class GetCustomersData
    {
      
        // Visit https://aka.ms/sqlbindingsinput to learn how to use this input binding
    [FunctionName("GetCustomersDataFromDb")]
         public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getcustomersdatafromdb")] HttpRequest req,
            [Sql("SELECT id FROM [SalesLT].[Customer]",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "AzureSqlDBConnectionString")] IEnumerable<Object> result,
            ILogger log)
        {
            //TODO Use customer object
            var checkResult = result;
            log.LogInformation("C# HTTP trigger with SQL Input Binding function processed a request.");

            return new OkObjectResult(result);
        }
    }
}
