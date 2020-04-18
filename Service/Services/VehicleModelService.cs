using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Data;
using Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ApplicationDbContext _context;

        public VehicleModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleModel[]> GetIncompleteItemsAsync()
        {
            return await _context.VehicleModels.ToArrayAsync();
        }
        
        public async Task<VehicleModel> GetItemsDetailsAsync(Guid id)
        {
            var item = await _context.VehicleModels
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();
            
            return item;
        }

        public async Task<bool> AddItemAsync(VehicleModel model)
        {
            model.ID = Guid.NewGuid();

            _context.VehicleModels.Add(model);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        
        public async Task<bool> EditItemAsync(Guid id, VehicleModel item)
        {
            var vehicle = await _context.VehicleModels
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();

            _context.Entry(vehicle).CurrentValues.SetValues(item);
            _context.VehicleModels.Update(vehicle);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _context.VehicleModels
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;
            item.Abbreviation = "";
            _context.VehicleModels.Remove(item);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<VehicleModel[]> SortFilterAsync(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var items = from s in _context.VehicleModels.Include(c => c.Manufacturer) select s;
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            } 
            
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString) || 
                s.Abbreviation.Contains(searchString));
            }

            if (sortOrder == "name_desc")
            {
                items = items.OrderByDescending(s => s.Name);
            }
            else if (sortOrder == "Abbreviation")
            {
                items = items.OrderBy(s => s.Abbreviation);
            }
            else if (sortOrder == "abbr_desc")
            {
                items = items.OrderByDescending(s => s.Abbreviation);                
            }
            else
            {
                items = items.OrderBy(s => s.Name);
            }

            return await items.ToArrayAsync();
        }

        public IQueryable<VehicleMake> GetManufacturersQuery()
        {
            var manufacturersQuery = from d in _context.Manufacturers
                                orderby d.Name
                                select d;
            return manufacturersQuery;
        }
    }
}