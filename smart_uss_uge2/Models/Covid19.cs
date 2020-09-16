using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace smart_uss_uge2.Models
{
    public class Covid19
    {
        [HiddenInput(DisplayValue = false)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [RegularExpression("[0-9]{6}-[0-9]{4}",
        ErrorMessage = "CPR: xxxxxx-xxxx")]
        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "cpr")]
        public string Cpr { get; set; }

        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "adresse")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "by")]
        public string By { get; set; }

        [RegularExpression("[0-9]{4}",
        ErrorMessage = "Postnummer er xxxx")]
        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "postnummer")]
        public string Postnummer { get; set; }

        [Required(ErrorMessage = "Please fill")]
        [JsonProperty(PropertyName = "telefonnummer")]
        public string Telefonnummer { get; set; }

        
        [JsonProperty(PropertyName = "testtaget")]
        public bool TestTaget { get; set; }

        [JsonProperty(PropertyName = "testerpositiv")]
        public bool ErPositiv { get; set; }


    }
}
