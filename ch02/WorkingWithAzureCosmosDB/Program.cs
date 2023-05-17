using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;

namespace WorkingWithAzureCosmosDB
{
    public class Program
    {
        private static IConfigurationRoot _configuration;

        public static async Task Main(string[] args)
        {
            BuildOptions();
            Console.WriteLine("Working with Azure Cosmos DB for NoSQL:");

            //get the connection string from config
            var cosmosCNSTR = _configuration["Cosmos:ConnectionString"];
            var primaryKey = _configuration["Cosmos:PrimaryKey"];
            var endpointURI = _configuration["Cosmos:EndpointURI"];
            Console.WriteLine($"Cosmos DB Info: URI: {endpointURI} | Key {primaryKey}");

            //Connect to Cosmos (use this to test the connection with just the connection string):
            /*
            using (CosmosClient client = new(connectionString: cosmosCNSTR))
            {
                //commands...
            };
            */

            //use this composition to see the code with the combination of the endpoint and primary key:
            using (CosmosClient client = new CosmosClient(endpointURI,primaryKey))
            {
                //Create Database:
                var db = await client.CreateDatabaseIfNotExistsAsync("Universities");
                var dbInfo = db.Database;
                Console.WriteLine($"Database created or exists: {dbInfo.Id}");

                //Create Container:
                var containerProperties = new ContainerProperties();
                containerProperties.Id = "Public";
                containerProperties.PartitionKeyPath = "/Name";
                containerProperties.IndexingPolicy.Automatic = true;
                containerProperties.IndexingPolicy.IndexingMode = IndexingMode.Consistent;
                
                var container = await client
                                        .GetDatabase("Universities")
                                        .CreateContainerIfNotExistsAsync(containerProperties, 400);
                var containerInfo = container.Container;
                Console.WriteLine($"Container Created or exists: {container.Container.Id}");

                //Add Items
                var isu = new University()
                {
                    Id = "iowa-state-university",
                    Name = "Iowa State University",
                    Location = "Ames, Iowa",
                    YearFounded = 1858
                };
                var iowa = new University()
                {
                    Id = "university-of-iowa",
                    Name = "University of Iowa",
                    Location = "Iowa City, Iowa, USA",
                    YearFounded = 1847
                };

                //create requires you prove it doesn't exist first:
                try
                {
                    var isuExists = await containerInfo.ReadItemAsync<University>
                                            (isu.Id, new PartitionKey(isu.Name));
                    Console.WriteLine("ISU Document existed, so not created");
                }
                catch (CosmosException cosmosEx)
                {
                    if (cosmosEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        var ISUDocument = await containerInfo.CreateItemAsync(isu);
                        Console.WriteLine("ISU Document created");

                        isu.Location = "Ames, Iowa, USA";
                        await containerInfo.UpsertItemAsync(isu);
                    }
                }

                //upsert:
                var iowaDocument = await containerInfo.UpsertItemAsync(iowa);
                Console.WriteLine("Iowa document created or updated with upsert");

                //point read:
                var isuDoc = await containerInfo.ReadItemAsync<University>
                                (isu.Id, new PartitionKey(isu.Name));
                Console.WriteLine($"ISU Doc: {isuDoc.Resource.Name} is located in {isuDoc.Resource.Location}");

                //iterate all results
                var query = new QueryDefinition("SELECT * FROM c");

                using (var feed = containerInfo.GetItemQueryIterator<University>(query))
                {
                    while (feed.HasMoreResults)
                    {
                        var allItems = await feed.ReadNextAsync();
                        foreach (var item in allItems)
                        {
                            Console.WriteLine($"Next: {item.Name}, " +
                                                $"founded {item.YearFounded}, " +
                                                $"is located in {item.Location}");
                        }
                    }
                }

                //use LINQ
                var universities = containerInfo.GetItemLinqQueryable<University>();
                using (var feed = universities.ToFeedIterator())
                {
                    while (feed.HasMoreResults)
                    {
                        var data = await feed.ReadNextAsync();
                        foreach (var item in data)
                        {
                            Console.WriteLine($"LINQ result: {item.Name}, " +
                                                $"founded {item.YearFounded}, " +
                                                $"is located in {item.Location}");
                        }
                    }
                }

                //Delete Items
                var dIowa = await containerInfo.DeleteItemAsync<University>
                                (iowa.Id, new PartitionKey(iowa.Name));
                var dISU = await containerInfo.DeleteItemAsync<University>
                                (isu.Id, new PartitionKey(isu.Name));
                Console.WriteLine("Items deleted");

                //Delete Container
                var containerToDelete = client.GetDatabase("Universities").GetContainer("Public");
                await containerToDelete.DeleteContainerAsync();
                Console.WriteLine("Container: Public -> Deleted");

                //Delete Database
                var dbDeleteResponse = await client.GetDatabase("Universities").DeleteAsync();
                Console.WriteLine("Database: Universities -> Deleted");
            };
        }

        private static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        }
    }
}
