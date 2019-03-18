using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using multithreading.Model;
using Newtonsoft.Json;

namespace multithreading.services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://restcountries.eu");
        }

        public async Task<IList<Country>> GetAll(){
            var response = await this._httpClient.GetStringAsync("/rest/v2/all");
            return JsonConvert.DeserializeObject<List<Country>>(response);
        }

    }

}
