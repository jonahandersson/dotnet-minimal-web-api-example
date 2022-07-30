using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetMinimalAPIDemo.Functions
{
    public class KeyVaultService
    {
        //https://www.c-sharpcorner.com/article/learn-how-to-authorize-your-key-vault-secrets-to-serverless-azure-function/
        public async Task<string> GetSecretValue(string keyName)
        {
            string secret = "";
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            //slow without ConfigureAwait(false)    
            //keyvault should be keyvault DNS Name    
            var secretBundle = await keyVaultClient.GetSecretAsync(Environment.GetEnvironmentVariable("AzureKeyVaultUri") + keyName).ConfigureAwait(false);
            secret = secretBundle.Value;
            Console.WriteLine(secret);
            return secret;
        }
    }
}
