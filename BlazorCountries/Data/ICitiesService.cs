//This is the Cities Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCountries.Data {
    //Each item below provices an interface to a methord in CitiesServices.cs
    public interface ICitiesService {
        Task<bool> CitiesInsert(Cities cities);
        Task<IEnumerable<Cities>> CitiesGetAll();
        Task<Cities> CitiesGetOne(int CityId);
        Task<bool> CitiesUpdate(Cities cities);
        Task<bool> CitiesDelete(int CityId);
    }
}
