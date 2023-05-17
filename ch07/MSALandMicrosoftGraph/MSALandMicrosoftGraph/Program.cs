using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace MSALandMicrosoftGraph
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        
        public static async Task Main(string[] args)
        {
            await BuildOptions();

            var tenantId = _configuration["AppRegistration:TenantID"];
            var clientId = _configuration["AppRegistration:ClientID"];

            Console.WriteLine($"tenantID: {tenantId}");
            Console.WriteLine($"clientID: {clientId}");

            //use MSAL Identity:
            await UtilizeMSALIdentity(tenantId, clientId);
            await UtilizeGraphServiceClient(tenantId, clientId);
        }

        private static async Task UtilizeMSALIdentity(string tenantId, string clientId)
        {
            //create the application:
            var msalAPP = PublicClientApplicationBuilder
                            .Create(clientId)
                            .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                            .WithRedirectUri("http://localhost")
                            .Build();

            //set the scopes for user permissions:
            string[] scopes = { "user.read", "email" };

            //get the token
            var result = await msalAPP.AcquireTokenInteractive(scopes).ExecuteAsync();

            //print token
            Console.WriteLine($"Your Token: {result.AccessToken}");
        }

        private static async Task UtilizeGraphServiceClient(string tenantId, string clientId)
        {
            //create the application:
            var graphAPP = PublicClientApplicationBuilder
                            .Create(clientId)
                            .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                            .WithRedirectUri("http://localhost")
                            .Build();

            //set the scopes for user permissions:
            string[] scopes = { "user.read", "email" };

            //create the DeviceCodeCredential
            var credentialOptions = new DeviceCodeCredentialOptions() { TenantId = tenantId, ClientId = clientId };
            var credential = new DeviceCodeCredential(credentialOptions);

            // Create a graph service client
            var gsc = new GraphServiceClient(credential, scopes);

            var myInfo = await gsc.Me.Request().GetAsync();
            Console.WriteLine($"{myInfo.GivenName} {myInfo.Surname}");
        }

        private static async Task BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        }
    }
}
