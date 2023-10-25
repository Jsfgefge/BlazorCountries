//This is the Cities Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCountries.Data {
    //Each item below provices an interface to a methord in CitiesServices.cs
    public interface ICitiesService {
        Task<int> CitiesInsertWithDuplicateChecking(string CityName, int CountryId, int CityPopulation);
        Task<IEnumerable<Cities>> CitiesGetAll();
        Task<Cities> CitiesGetOne(int CityId);
        Task<IEnumerable<Cities>> Cities_GetByCountry(int @CountryId);
        Task<int> CitiesUpdateWithDuplicateChecking(string CityName, int CountryId, int CityPopulation, int CityId);
        Task<bool> CitiesDelete(int CityId, int CountryId);
    }
}
