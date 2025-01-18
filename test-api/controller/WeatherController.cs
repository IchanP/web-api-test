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
        [HttpGet]
        // NOTE - Task is for asynchronous fetching?
        public Item GetItem()
        {
            var item = new Item
            {
                Name = "",
                Id = 1
            };
            return item;
        }

    }
}
