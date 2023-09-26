using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCountries.Data{
    public class CitiesService : ICitiesService{
        //Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public CitiesService(SqlConnectionConfiguration configuration){
            _configuration = configuration;
        }
        //Add (create) a Cities table row (SQL Insert)
        public async Task<bool> CitiesInsert(Cities cities){
            using (var conn = new SqlConnection(_configuration.Value)){
                var parameters = new DynamicParameters();
                parameters.Add("CityName", cities.CityName, DbType.String);
                parameters.Add("CountryId", cities.CountryId, DbType.Int32);
                parameters.Add("CityPopulation", cities.CityPopulation, DbType.Int32);

                //Stored procedure method
                await conn.ExecuteAsync("spCities_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        //Get a list of cities rows (SQL Select)
        public async Task<IEnumerable<Cities>> CitiesGetAll(){
            IEnumerable<Cities> citiess;
            using (var conn = new SqlConnection(_configuration.Value)){
                citiess = await conn.QueryAsync<Cities>("spCities_GetAll", commandType: CommandType.StoredProcedure);
            }
            return citiess;
        }
        //Get one cities based on tis CitiesID (SQL Select)
        public async Task<Cities> CitiesGetOne(int @CityId){
            Cities cities = new Cities();
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", CityId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)){
                cities = await conn.QueryFirstOrDefaultAsync<Cities>("spCities_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return cities;
        }

        //Update one Cities row based on tis CitiesID (SQL Update)
        public async Task<bool> CitiesUpdate(Cities cities) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("CityId", cities.CityId, DbType.Int32);
                parameters.Add("CityName", cities.CityId, DbType.String);
                parameters.Add("CountryId", cities.CountryId, DbType.Int32);
                parameters.Add("CityPopulation", cities.CityPopulation, DbType.Int32);
                await conn.ExecuteAsync("spCities_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        //Physically delete one Cities row based on its CitiesID (SQL Delete)
        public async Task<bool> CitiesDelete(int CityId) {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", CityId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)){
                await conn.ExecuteAsync("spCities_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

    }
}
