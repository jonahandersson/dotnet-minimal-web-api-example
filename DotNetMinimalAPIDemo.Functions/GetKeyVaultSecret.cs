using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DotNetMinimalAPIDemo.Functions
{
    /// <summary>
    /// Function that gets key vault secret 
    /// Reference Example: https://www.c-sharpcorner.com/article/learn-how-to-authorize-your-key-vault-secrets-to-serverless-azure-function/
    /// </summary>
    public static class GetKeyVaultSecret
    {
        [FunctionName("GetKeyVaultSecret")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            KeyVaultService _keyVaultservice = new KeyVaultService();

            //secret name applicationSecret2    
            string secretValue = await _keyVaultservice.GetSecretValue("jonahsdemosqlserver-connectionstring");
            log.LogInformation("Secret value retrived via Secret Uri" + secretValue);

            // parse query parameter    
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var name = data?.name;
            var secret = data?.secret;          

            string responseMessage = string.IsNullOrEmpty(name)
              ? "HTTP triggered function executed to get the secret from Azure Key Vault successfully."
               : $"The value of the key vault {name} secret is {secret}";

            return new OkObjectResult(responseMessage);
        }
    }
}
