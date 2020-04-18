using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Models;
using System.Net;
using System.Data;
using Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PagedList;
using AspNetCoreVehicle.Models;
using AutoMapper;

namespace AspNetCoreVehicle.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;
 
        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {   
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbbrSortParm = sortOrder == "Abbreviation" ? "abbr_desc" : "Abbreviation";
            ViewBag.CurrentFilter = searchString;
            
            var manufacturers = await _vehicleMakeService.SortFilterAsync(sortOrder, searchString, currentFilter, page);
            //var manufacturersDto = _mapper.Map<VehicleMakeViewModel>(manufacturers);
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(manufacturers.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            VehicleMake vehicle = await _vehicleMakeService.GetItemsDetailsAsync(id);
            //var vehicleDto = _mapper.Map<VehicleMakeViewModel>(vehicle);

            return View(vehicle);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMake manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _vehicleMakeService.AddItemAsync(manufacturer);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id, VehicleMake item)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            var successful = await _vehicleMakeService.EditItemAsync(id, item);

            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }
    
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _vehicleMakeService.DeleteItemAsync(id);
            if (!successful)
            {
                return BadRequest("Could not delete item.");
            }
            return RedirectToAction("Index");
        }
    }
}