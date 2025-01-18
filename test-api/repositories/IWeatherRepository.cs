namespace test_api.repositories;

public interface IWeatherRepository
{
    List<WeatherStation> GetWeatherStations();
}
