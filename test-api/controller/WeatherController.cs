using Microsoft.AspNetCore.Mvc;
using test_api.model;
using test_api.services;

namespace test_api.controller
{
    // NOTE - MVC is used for normal REST API's too and not limited to model view controller apps.
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IWeatherFetcher weatherFetcher;

        public TestController(IWeatherFetcher weatherFetcher)
        {
            this.weatherFetcher = weatherFetcher;
        }

        /*         [HttpGet]
                // NOTE - Task is for asynchronous fetching?
                public async Task<ActionResult<Item>> GetItem(int id)
                {

                } */
        [HttpGet]
        public async Task<IActionResult> GetItem()
        {
            await this.weatherFetcher.FetchWeather();
            return Ok("Fetched weather");
        }
    }
}