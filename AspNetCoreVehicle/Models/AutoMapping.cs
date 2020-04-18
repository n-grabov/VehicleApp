using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Models;
using AutoMapper;

namespace AspNetCoreVehicle.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
        }
    }
}