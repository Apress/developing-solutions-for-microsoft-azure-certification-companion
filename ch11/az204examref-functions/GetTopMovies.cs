using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace az204examref_functions
{
    public static class GetTopMovies
    {
        [FunctionName("GetTopMovies")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var movieData = TopMovies();

            return new OkObjectResult(JsonConvert.SerializeObject(movieData));
        }
        
        private static List<SampleDataItem> TopMovies()
        {
            return new List<SampleDataItem>()
            {
                new SampleDataItem() { Id = 1, Rating = "R", Title = "The Shawshank Redemption", Type = "Movie"},
                new SampleDataItem() { Id = 2, Rating = "R", Title = "The Godfather", Type = "Movie"},
                new SampleDataItem() { Id = 3, Rating = "PG-13", Title = "The Dark Knight", Type = "Movie"},
                new SampleDataItem() { Id = 4, Rating = "NR", Title = "12 Angry Men", Type = "Movie"},
                new SampleDataItem() { Id = 5, Rating = "R", Title = "Schindler's List", Type = "Movie"},
                new SampleDataItem() { Id = 6, Rating = "PG-13", Title = "The Lord of the Rings: The Return of the King", Type = "Movie"},
                new SampleDataItem() { Id = 7, Rating = "R", Title = "Pulp Fiction", Type = "Movie"},
            };
        }
    }
}
