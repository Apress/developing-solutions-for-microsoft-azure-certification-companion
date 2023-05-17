// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using System.Threading.Tasks;
using System.IO;
using AZ204ExamRefSimpleFunctionApp.ParseExcel;

namespace az204examref_functions
{
    public static class ProcessExcelToCosmos
    {
        /*
        [FunctionName("ProcessExcelToCosmos")]
        public static async Task Run([EventGridTrigger]EventGridEvent eventGridEvent,
                [Blob(blobPath: "{data.url}", access: FileAccess.Read,
                    Connection = "myStorageConnection")] Stream fileToProcess,
                [CosmosDB(
                    databaseName: "SampleDataItems",
                    collectionName: "Items",
                    ConnectionStringSetting = "myCosmosConnection")]
                    IAsyncCollector<SampleDataItem> sampleDataItemDocuments,
                ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
            log.LogInformation($"FileInfo: {fileToProcess.Length}");

            // Convert the incoming image stream to a byte array.
            byte[] data;

            using (var br = new BinaryReader(fileToProcess))
            {
                data = br.ReadBytes((int)fileToProcess.Length);
            }

            using (var ms = new MemoryStream(data))
            {
                log.LogInformation("Parsing file..");
                var parseResults = ParseFile.ParseDataFile(ms);
                foreach (var pr in parseResults)
                {
                    log.LogInformation($"Adding {pr.Title} to cosmos db output documents");
                    await sampleDataItemDocuments.AddAsync(pr);
                }
            }
        }
        */
    }
}
