using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using multithreading.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace multithreading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
  
        public PopulationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        // GET api/population/
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://restcountries.eu");
            string content = await client.GetStringAsync("/rest/v2/all");
            return Ok(JsonConvert.DeserializeObject<List<Country>>(content));
        }

        // GET api/population/name/uk
        [HttpGet("name/{str}")]
        public async Task<ActionResult<List<Country>>> GetByQueryString(string str)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://restcountries.eu");
            string content = await client.GetStringAsync(String.Format("/rest/v2/name/{0}", str));
            Console.WriteLine(content);
            return Ok(JsonConvert.DeserializeObject<List<Country>>(content));
        }

    }
}
