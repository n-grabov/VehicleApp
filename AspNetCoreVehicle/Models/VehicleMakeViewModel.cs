using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreVehicle.Models
{
    public class VehicleMakeViewModel
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }   
    }
}