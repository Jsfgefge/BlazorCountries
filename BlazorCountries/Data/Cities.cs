//This id the model for one eow in the database table
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCountries.Data
{
    public class Cities
    {
        [Required]
        public int CityId { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public int CityPopulation { get; set; }

    }
}
