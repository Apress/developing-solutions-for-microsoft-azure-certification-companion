using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace az204examref_functions
{
    public static class FanInFanOutOrchestration
    {
        /*
        [FunctionName("FanInFanOutOrchestrator")]
        public static async Task<int> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context
            , ILogger log)
        {
            // Initialize: Get a number of work items to process in parallel:
            var workBatch = await context.CallActivityAsync<int>("FirstFunction", 0);

            log.LogInformation($"Starting the fan out/in orchestration with {workBatch} workload function calls");

            //use parallel tasks to fan out and call n operations simultaneously
            var parallelTasks = new List<Task<int>>();

            for (int i = 1; i <= workBatch; i++)
            {
                Task<int> nextWorker = context.CallActivityAsync<int>("WorkloadFunction", i);
                parallelTasks.Add(nextWorker);
            }

            log.LogInformation("Parallel Tasks completed!");
            // Aggregate all N outputs and send the result to Final Function.

            await Task.WhenAll(parallelTasks);

            //get the total from all execution calculations:
            var total = parallelTasks.Sum(w => w.Result);

            log.LogInformation($"Total sent to final function: {total}");
            await context.CallActivityAsync("FinalFunction", total);

            return total;
        }

        [FunctionName(nameof(FirstFunction))]
        public static int FirstFunction([ActivityTrigger] int starter, ILogger log)
        {
            //do some setup work here, other startup function tasks
            var numberOfWorkersToProcess = starter;
            try
            {
                bool success = int.TryParse(Environment.GetEnvironmentVariable("NumberOfWorkerFunctions")
                                                , out numberOfWorkersToProcess);
            }
            catch (Exception ex)
            {
                log.LogError("The environment variable NumberOfWorkerFunctions is unset!", ex);
            }

            log.LogInformation($"Current number of workers {numberOfWorkersToProcess}.");
            return numberOfWorkersToProcess;
        }

        [FunctionName(nameof(WorkloadFunction))]
        public static int WorkloadFunction([ActivityTrigger] int nextWorkload, ILogger log)
        {
            //do the work
            var computed = nextWorkload * 2;
            log.LogInformation($"Current detail {nextWorkload} | Computed: {computed}.");
            return computed;
        }

        [FunctionName(nameof(FinalFunction))]
        public static int FinalFunction([ActivityTrigger] int total, ILogger log)
        {
            //complete the work here
            log.LogInformation($"Final Function [value]: {total}.");
            return total;
        }

        [FunctionName("FanInFanOutOrchestrator_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("FanInFanOutOrchestrator", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
        */
    }
}