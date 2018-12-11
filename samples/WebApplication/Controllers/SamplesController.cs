using System.Collections.Generic;
using Essensoft.AspNetCore.Snowflake;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly IIdWorker _idWorker;
        public SamplesController(IIdWorker idWorker)
        {
            _idWorker = idWorker;
        }

        // GET api/samples
        [HttpGet]
        public IEnumerable<long> Get()
        {
            var list = new List<long>();

            for (var i = 0; i < 1000; i++)
            {
                list.Add(_idWorker.NextId());
            }

            return list;
        }
    }
}
