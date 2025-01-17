using test_api.Extensions;
using Microsoft.Extensions.Configuration;

namespace test_api.services
{
    public class WeatherFetcher(IHttpClientFactory factory, IConfiguration configuration, ILogger<WeatherFetcher> logger) : IWeatherFetcher
    {
        private readonly string clientName = configuration["WeatherClientName"]
            ?? throw new ArgumentNullException("WeatherClientName configuration is missing");
        public async Task FetchWeather()
        {
            using HttpClient client = factory.CreateClient(clientName);

            try
            {

                var response = await client.GetAsync("parameter/1/station-set/all/period/latest-hour/data.json");

                response.EnsureSuccessStatusCode().WriteRequestToConsole(logger);


                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");

                /* Unknown x = await client.GetFromJsonAsync<Unknown>(
                     ""
                 // TODO - Serializer? 
                 ); */

            }
            catch (Exception e)
            {
                // TODO should add our own custom message depending on the type of error
                logger.LogError(e.Message);
            }
        }
    }
}