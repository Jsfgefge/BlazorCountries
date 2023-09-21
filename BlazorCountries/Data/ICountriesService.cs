//This is the Countries Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCountries.Data
{
    //Each item belo provices an interface to a method in Counties Services.cs
    public interface ICountriesService
    {
        Task<bool> CountriesInsert(Countries countries);
        Task<IEnumerable<Countries>> CountriesGetAll();
        Task<Countries> CountriesGetOne(int CountryId);
        Task<bool> CountriesUpdate(Countries countries);
        Task<bool> CountriesDelete(int CountryId);        
    }
}
