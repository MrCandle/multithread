using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Multithreading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
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

        /// <summary>Will process a string in one Thread.</summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpGet("single")]
        public async Task<ActionResult<String>> GetSingle()
        {
            String res = await Task.Run<String>(() =>
            {
                Thread.Sleep(1000);
                return String.Format("Thread {0} processed this wonderful String to you.", Thread.CurrentThread.ManagedThreadId);
            });

            return Ok(res);
        }

        // GET api/examples/multiple
        [HttpGet("multiple")]
        public async Task<ActionResult<IList<String>>> GetMultiple()
        {
            Task<String>[] tasks = list.Select(name => GetNameTask(name)).ToArray();
            await Task.WhenAll(tasks);

            var result = GetResults(tasks);

            return Ok(result);
        }

        // GET api/examples/semaphore/5
        [HttpGet("semaphore/{count:int}")]
        public async Task<ActionResult<IList<String>>> GetMultipleWithSemaphore(int count)
        {

            Semaphore semaphoreObject = new Semaphore(initialCount: count, maximumCount: count);

            Task<String>[] tasks = list.Select(name => GetNameTask(name, semaphoreObject)).ToArray();
            await Task.WhenAll(tasks);

            var result = GetResults(tasks);

            return Ok(result);
        }

        private static Task<String> GetNameTask(String name)
        {
            return GetNameTask(name, null);
        }

        private static Task<String> GetNameTask(String name, Semaphore semaphore)
        {
            return Task<String>.Run(() =>
            {
                if (semaphore != null)
                {
                    semaphore.WaitOne();
                }
                Console.WriteLine("[START] - Thread {0} started processing {1}", Thread.CurrentThread.ManagedThreadId, name);
                Thread.Sleep(5000);
                Console.WriteLine(String.Format("[END] - Thread {0} processed {1}", Thread.CurrentThread.ManagedThreadId, name));
                if (semaphore != null)
                {
                    semaphore.Release();
                }
                return String.Format("Thread {0} processed {1}", Thread.CurrentThread.ManagedThreadId, name);
            });
        }

        private static IList<String> GetResults(Task<String>[] tasks)
        {
            IList<String> result = new List<String>();

            for (int i = 0; i < tasks.Count(); i++)
            {
                result.Add(tasks[i].Result);
            }

            return result;
        }
    }
}
