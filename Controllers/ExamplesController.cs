using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace multithreading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {

        // GET api/examples/single
        [HttpGet("single")]
        public async Task<ActionResult<String>> GetSingle()
        {
            String res = await Task.Run<String>(() =>
            {
                Thread.Sleep(1000);
                return String.Format("Thread {0} is returning this wonderful String to you.", Thread.CurrentThread.ManagedThreadId);
            });

            return Ok(res);
        }

        // GET api/examples/multiple
        [HttpGet("multiple")]
        public async Task<ActionResult<IList<String>>> GetMultiple()
        {
            List<String> list = new List<String>
            { "Kellye",
            "Tressie",
            "Tamatha",
            "Nigel",
            "Majorie",
            "Loida",
            "Chuck",
            "Sheilah",
            "Tillie",
            "Norbert" };

            List<String> res = new List<String>();

            List<Task> tasks = list.Select(name => ProcessName(name)).ToList();
            var call = Task.WhenAll(tasks).ContinueWith(result => {
                Console.WriteLine(result);
            });

            await call;

            return Ok(res);
        }

        static async Task ProcessName(String name)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine(String.Format("Thread {0} processed {1}", Thread.CurrentThread.ManagedThreadId, name));
            });
        }
    }
}
