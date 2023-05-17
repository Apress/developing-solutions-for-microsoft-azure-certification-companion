using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace WorkingWithBlobStorage
{
    public class Program
    {
        private static IConfigurationRoot _configuration;

        public static void Main(string[] args)
        {
            BuildOptions();
            Console.WriteLine("Working with Azure Blob Storage");

            //get the connection string from config
            var storageCNSTR = _configuration["Storage:ConnectionString"];

            //create blob client
            var blobStorageClient = new BlobServiceClient(storageCNSTR);

            //create notes container
            var exists = false;
            var containers = blobStorageClient.GetBlobContainers().AsPages();
            foreach (var containerPage in containers)
            {
                foreach (var containerItem in containerPage.Values)
                {
                    if (containerItem.Name.Equals("notes"))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists) break;
            }
            if (!exists)
            {
                blobStorageClient.CreateBlobContainer("notes");
            }
            var containerClient = blobStorageClient.GetBlobContainerClient("notes");

            //upload
            var path = "./notes/affirmations.txt";
            var blobClient = containerClient.GetBlobClient("affirmations.txt");
            var fileBytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(fileBytes);
            blobClient.Upload(ms, overwrite: true);

            //list blobs
            exists = false;
            foreach (var blob in containerClient.GetBlobs())
            {
                Console.WriteLine($"Blob {blob.Name} found!");
                if (blob.Name.Contains("affirmations.txt"))
                {
                    exists = true;
                }
            }

            //get blob
            if (exists)
            {
                blobClient = containerClient.GetBlobClient("affirmations.txt");
                Console.WriteLine($"Blob {blobClient.Name} exists at {blobClient.Uri}");
                var downloadFileStream = new MemoryStream();
                blobClient.DownloadTo(downloadFileStream);
                var downloadFileBytes = downloadFileStream.ToArray();
                using (var f = File.Create($"{Environment.CurrentDirectory}/notes/affirmations-download.txt"))
                {
                    f.Write(downloadFileBytes, 0, downloadFileBytes.Length);
                }


            }

            // metadata
            foreach (var blob in containerClient.GetBlobs())
            {
                Console.WriteLine($"Blob {blob.Name} found! {blob.Properties}");
                if (blob.Name.Contains("affirmations.txt"))
                {
                    //add metadata
                    blob.Metadata.Add("createdby", "yourname");
                    blob.Metadata.Add("reason", "success");
                    blob.Metadata.Add("filter", "important");

                    //review metadata
                    var metadata = blob.Metadata;
                    foreach (var key in metadata.Keys)
                    {
                        Console.WriteLine($"Metadata {key} has value {metadata[key]}");
                    }
                }
            }

            //delete blob
            blobClient = containerClient.GetBlobClient("affirmations.txt");
            blobClient.DeleteIfExists();

            //delete container
            containerClient.DeleteIfExists();
        }

        private static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        }
    }
}
