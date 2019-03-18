using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Multithreading.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Multithreading.Services;

namespace Multithreading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationController : ControllerBase
    {

        private readonly ICountryService _countryService;
  
        public PopulationController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        
        // GET api/population/
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
        {
            var result = await this._countryService.GetAll();
            return Ok(result);
        }

        // GET api/population/name/uk
        // [HttpGet("name/{str}")]
        // public async Task<ActionResult<List<Country>>> GetByQueryString(string str)
        // {
        //     string content = await _countryClient.GetStringAsync(String.Format("/rest/v2/name/{0}", str));
        //     return Ok(JsonConvert.DeserializeObject<List<Country>>(content));
        // }

    }
}
