using Newtonsoft.Json;
using test_api.Extensions;

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

                var deserialized = JsonConvert.DeserializeObject(jsonResponse);
                // Deserialize the response and throw if it's null
                if (deserialized is null)
                {
                    throw new ArgumentNullException(nameof(deserialized), "Deserialization resulted in null object");
                }

                // Should return the deserialized response so it can be written to the cache in service.
                logger.Log(LogLevel.Information, "Deserialized response: {Response}", deserialized);

            }
            catch (Exception e)
            {
                // TODO should add our own custom message depending on the type of error
                logger.LogError(e.Message);
            }
        }
    }
}