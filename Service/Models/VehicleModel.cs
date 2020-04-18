using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Service.Models
{
    public class VehicleModel
    {
        public Guid ID { get; set; }

        [ForeignKey("Manufacturer")]
        public Guid ManufacturerID { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        public VehicleMake Manufacturer { get; set; }
    }
}