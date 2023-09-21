using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCountries.Data
{
    public class Countries
    {
        [Required]
        public int CountryID { get;set; }
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
