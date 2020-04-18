using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class VehicleMake
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}