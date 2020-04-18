using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Data;
using Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;

namespace Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly ApplicationDbContext _context;

        public VehicleMakeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleMake[]> GetIncompleteItemsAsync()
        {
            return await _context.Manufacturers.ToArrayAsync();
        }
        
        public async Task<VehicleMake> GetItemsDetailsAsync(Guid id)
        {
            var item = await _context.Manufacturers
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();
            
            return item;
        }
        
        public async Task<bool> AddItemAsync(VehicleMake manufacturer)
        {
            manufacturer.ID = Guid.NewGuid();

            _context.Manufacturers.Add(manufacturer);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> EditItemAsync(Guid id, VehicleMake item)
        {
            var vehicle = await _context.Manufacturers
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();

            _context.Entry(vehicle).CurrentValues.SetValues(item);
            _context.Manufacturers.Update(vehicle);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _context.Manufacturers
                .Where(x => x.ID == id)
                .SingleOrDefaultAsync();

            _context.Manufacturers.Remove(item);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<VehicleMake[]> SortFilterAsync(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var manufacturers = from s in _context.Manufacturers select s;
            
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
                manufacturers = manufacturers.Where(s => s.Name.Contains(searchString) || 
                s.Abbreviation.Contains(searchString));
            }

            if (sortOrder == "name_desc")
            {
                manufacturers = manufacturers.OrderByDescending(s => s.Name);
            }
            else if (sortOrder == "Abbreviation")
            {
                manufacturers = manufacturers.OrderBy(s => s.Abbreviation);
            }
            else if (sortOrder == "abbr_desc")
            {
                manufacturers = manufacturers.OrderByDescending(s => s.Abbreviation);                
            }
            else
            {
                manufacturers = manufacturers.OrderBy(s => s.Name);
            }

            return await manufacturers.ToArrayAsync();
        }
    }
}