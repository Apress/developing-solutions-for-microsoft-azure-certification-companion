using Microsoft.Extensions.Configuration;
using Azure.Data.Tables;

namespace WorkingWithAzureTableStorage
{
    public class Program
    {
        private static IConfigurationRoot _configuration;

        public static async Task Main(string[] args)
        {
            BuildOptions();
            Console.WriteLine("Working with Azure Blob Storage");

            //get the connection string from config
            var storageCNSTR = _configuration["Storage:ConnectionString"];
            Console.WriteLine($"Storage Connection: {storageCNSTR}");
            
            //compose the Service
            var tableServiceClient = new TableServiceClient(storageCNSTR);

            //Create a new Table "Universities"
            var tableClient = tableServiceClient.GetTableClient(
                tableName: "Universities"
            );

            await tableClient.CreateIfNotExistsAsync();

            //add an item to the service
            var iowaStateUniversity = new University()
            {
                RowKey = "iowa-state-university-ames-iowa",
                PartitionKey = "iowa-state-university",
                Name = "Iowa State University",
                Location = "Ames, Iowa",
                YearFounded = 1858
            };

            await tableClient.DeleteEntityAsync(iowaStateUniversity.PartitionKey, iowaStateUniversity.RowKey);
            await tableClient.AddEntityAsync<University>(iowaStateUniversity);

            var isu = await tableClient.GetEntityAsync<University>(iowaStateUniversity.PartitionKey, iowaStateUniversity.RowKey);

            var secondIsu = tableClient.Query<University>(x => x.Name.Equals("Iowa State University"));
            var secondIsuName = string.IsNullOrWhiteSpace(secondIsu?.FirstOrDefault()?.Name) ? "Not Found" : secondIsu.FirstOrDefault().Name;
            var anotherIsu = tableClient.Query<University>
                                (x => x.PartitionKey == "iowa-state-university")
                                    .SingleOrDefault();
            var anotherIsuName = string.IsNullOrWhiteSpace(anotherIsu?.Name) ? "Not Found" : anotherIsu.Name;
            Console.WriteLine("Items:");
            Console.WriteLine($"Get Entity Async: {isu.Value.Name}");
            Console.WriteLine($"tableClient Query by Name: {secondIsuName}");
            Console.WriteLine($"tableClient Query by Partition: {anotherIsuName}");

        }

        private static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        }
    }
}
