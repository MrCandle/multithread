using System.Collections.Generic;
using System.Threading.Tasks;
using multithreading.Model;

namespace multithreading.services
{
    public interface ICountryService
    {

        Task<IList<Country>> GetAll();

    }
}