using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BlazorCountries.Data
{
    public class CountryService : ICountriesService
    {
        //Data cinnection
        private readonly SqlConnectionConfiguration _configuration;
        public CountriesService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Countries table row (SQL Insert)

        public async Task<bool> CountriesInsert(Countries countries)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("CountryName", countries.CountryName, DbType.String);
                //Stored procedure method
                await conn.ExecuteAsync("spCountries_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;

        }
        // Get a list of countries rows (SQL Select)
        public async Task<IEnumerable<Countries>> CountriesGetAll()
        {
            IEnumerable<Countries> countries;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                countries = await conn.QueryAsync<Countries>("spCountries_GetAll", commandType: CommandType.StoredProcedure);
            }
            return countries;
        }

        //Get one country based on its CountriesID (SQL Select)

        public async Task<Countries> CountriesGetOne(int @CountryId)
        {
            Countries countries = new Countries();
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                countries = await conn.QueryFirstOrDefaultAsync<Countries>("spCountries_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return countries;
        }

    }
}
