namespace test_api.services;

public interface IWeatherFetcher
{
    Task<List<WeatherStation>> FetchWeather();
}