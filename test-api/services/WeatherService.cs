
using test_api.repositories;

namespace test_api.services
{
    public class WeatherService(IWeatherRepository repository, IWeatherFetcher fetcher, ILogger<WeatherService> logger) : BackgroundService
    {
        // The stoppingToken is passed from the ASP.NET Core's background stuff and handles graceful shutdowns.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    TimeSpan delay = CalculateNextRuntime(1, 1);
                    // TODO sleep until XX:01
                    await Task.Delay(delay, stoppingToken);
                    List<WeatherStation> stationData = await fetcher.FetchWeather();

                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("Weather service shutting down");
                    break;
                }
                catch (Exception e)
                {
                    // TODO depending on the error we might want to send out info about Fetched data being outdated? To other services
                    logger.LogError(e.Message);
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Add a retry after a delay to avoid repeated errors
                }
            }
        }

        private TimeSpan CalculateNextRuntime(int hoursToAdd, int minute)
        {
            DateTime timeNow = DateTime.Now;
            DateTime nextRunTime = timeNow.Date.AddHours(timeNow.Hour + hoursToAdd).AddMinutes(minute);
            return nextRunTime - timeNow;
        }
    }
}