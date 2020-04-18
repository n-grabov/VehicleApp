using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using System.Data.Entity.Infrastructure;

namespace AspNetCoreVehicle.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService _vehicleModelService;
 
        public VehicleModelController(IVehicleModelService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {   
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbbrSortParm = sortOrder == "Abbreviation" ? "abbr_desc" : "Abbreviation";
            
            ViewBag.CurrentFilter = searchString;
            
            var manufacturersQuery = _vehicleModelService.GetManufacturersQuery();
            ViewBag.Manufacturer = new SelectList(manufacturersQuery, "ID", "Name");

            var models = await _vehicleModelService.SortFilterAsync(sortOrder, searchString, currentFilter, page);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(models.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            VehicleModel model = await _vehicleModelService.GetItemsDetailsAsync(id);

            var manufacturersQuery = _vehicleModelService.GetManufacturersQuery();
            ViewBag.Manufacturer = new SelectList(manufacturersQuery, "ID", "Name");

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _vehicleModelService.AddItemAsync(model);

            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id, VehicleModel item)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            var successful = await _vehicleModelService.EditItemAsync(id, item);

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

            var successful = await _vehicleModelService.DeleteItemAsync(id);
            
            if (!successful)
            {
                return BadRequest("Could not delete item.");
            }
            return RedirectToAction("Index");
        }
    }
}