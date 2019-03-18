using System.Collections.Generic;
using System.Threading.Tasks;
using Multithreading.Model;

namespace Multithreading.Services
{
    public interface ICountryService
    {

        Task<IList<Country>> GetAll();

    }
}