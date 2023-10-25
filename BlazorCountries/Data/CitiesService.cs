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
        public async Task<int> CitiesInsertWithDuplicateChecking(string CityName, int CountryId, int CityPopulation){
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("CityName", CityName, DbType.String);
            parameters.Add("CountryId", CountryId, DbType.Int32);
            parameters.Add("CityPopulation", CityPopulation, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using(var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCities_InsertWithDuplicateChecking", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
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

        public async Task<IEnumerable<Cities>> Cities_GetByCountry(int @CountryId) {
            //Cities cities = new cities();
            IEnumerable<Cities> cities;
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                cities = await conn.QueryAsync<Cities>("spCities_GetByCountry", parameters, commandType: CommandType.StoredProcedure);
            }
            return cities;
        }


        //Update one Cities row based on tis CitiesID (SQL Update)
        public async Task<int> CitiesUpdateWithDuplicateChecking(string CityName, int CountryId, int CityPopulation, int CityId) {

            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("CityName", CityName, DbType.String);
            parameters.Add("CountryId", CountryId, DbType.Int32);
            parameters.Add("CityPopulation", CityPopulation, DbType.Int32);
            parameters.Add("CityId", CityId, dbType: DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCities_UpdateWithDuplicateChecking", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }

        //Physically delete one Cities row based on its CitiesID (SQL Delete)
        public async Task<bool> CitiesDelete(int CityId, int CountryId) {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", CityId, DbType.Int32);
            parameters.Add("@CountryId", CountryId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)){
                await conn.ExecuteAsync("spCities_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

    }
}
