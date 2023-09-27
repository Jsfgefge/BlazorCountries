//This is the model for one eow in the data table.
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCountries.Data
{
    public class Countries
    {
        [Required]
        public int CountryId { get;set; }
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
