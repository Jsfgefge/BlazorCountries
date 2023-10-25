//This id the model for one eow in the database table
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCountries.Data
{
    public class Cities
    {
        [Required]
        public int CityId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long - it cannot be longer than 50 characters.")]
        public string CityName { get; set; }
        public int CountryId { get; set; }
        [Required]
        [Range(10000, 32000000, ErrorMessage ="Population must be between 10,000 and 32 million")]
        public int CityPopulation { get; set; }

    }
}
