﻿//This is the service for the Countries Class
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BlazorCountries.Data
{
    public class CountriesService : ICountriesService
    {
        //Data cinnection
        private readonly SqlConnectionConfiguration _configuration;
        public CountriesService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a Countries table row (SQL Insert)

        public async Task<int> CountriesInsertWithDuplicateChecking(string CountryName)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@CountryName", CountryName, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spCountries_InsertWithDuplicateChecking", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;

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

        public async Task<IEnumerable<Countries>> GetPopulationByCountry() {
            IEnumerable<Countries> countries;
            using (var conn = new SqlConnection(_configuration.Value)) {
                countries = await conn.QueryAsync<Countries>("spCountries_GetPopulationByCountry", commandType: CommandType.StoredProcedure);
            }
            return countries;
        }

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

        //Update one Countries row based on its CountriesID (SQL Update)
        public async Task<int> CountriesUpdateWithDuplicatesChecking(Countries countries)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", countries.CountryId, DbType.Int32);
            parameters.Add("CountryName", countries.CountryName, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spCountries_UpdateWithDuplicateChecking", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }

        //Physically delete one Countries row based on its CountriesID(SQL Delete)
        public async Task<bool> CountriesDelete(int CountryID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryID, DbType.Int32);
            
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spCountries_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }


    }
}
