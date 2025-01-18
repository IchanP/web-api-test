
namespace test_api.repositories
{
    // TODO  needs to accept a redis database?
    public class WeatherRepository() : IWeatherRepository
    {
        // TODO  maybe this should return something more primitive?
        public List<WeatherStation> GetWeatherStations()
        {
            throw new NotImplementedException();
        }
    }
}