//This is the model for one eow in the data table.
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCountries.Data
{
    public class Countries
    {
        [Required]
        public int CountryId { get;set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long - it cannot be longer than 50 characters.")]
        public string CountryName { get; set; }
    }
}
