using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using test_api.Extensions;

namespace test_api.services
{
    public class WeatherFetcher(IHttpClientFactory factory, IConfiguration configuration, ILogger<WeatherFetcher> logger) : IWeatherFetcher
    {
        private readonly string clientName = configuration["WeatherClientName"]
            ?? throw new ArgumentNullException("WeatherClientName configuration is missing");
        public async Task<List<WeatherStation>> FetchWeather()
        {
            using HttpClient client = factory.CreateClient(clientName);

            try
            {

                var response = await client.GetAsync("parameter/1/station-set/all/period/latest-hour/data.json");

                response.EnsureSuccessStatusCode().WriteRequestToConsole(logger);


                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");

                JObject jObject = JObject.Parse(jsonResponse);

                List<WeatherStation>? stations = jObject["station"]?.ToObject<List<WeatherStation>>();
                if (stations == null)
                {
                    throw new ArgumentNullException(nameof(stations), "Deserialization resulted in null object");
                }

                // Should return the deserialized response so it can be written to the cache in service.
                logger.Log(LogLevel.Information, "Successfully deserialized response from hourly data.");
                return stations;
            }
            catch (Exception e)
            {
                // TODO should add our own custom message depending on the type of error
                logger.LogError(e.Message);
                throw; // Rethrow the error 
            }
        }
    }
}