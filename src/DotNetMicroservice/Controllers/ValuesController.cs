using System.Collections.Generic;
using DotNetMicroservice.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMicroservice.Controllers
{
    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Subscription>> Get()
        {
            return new Subscription[] { new Subscription() };
        }

        [HttpGet("{id}")]
        public ActionResult<Subscription> Get(int id)
        {
            return default;
        }

        [HttpPost]
        public void Post([FromBody] Subscription value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Subscription value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
