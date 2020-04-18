using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Services
{
    public interface IVehicleMakeService
    {
        Task<VehicleMake[]> GetIncompleteItemsAsync();
        Task<VehicleMake> GetItemsDetailsAsync(Guid id);
        Task<bool> AddItemAsync(VehicleMake manufacturer);
        Task<bool> DeleteItemAsync(Guid id);
        Task<bool> EditItemAsync(Guid id, VehicleMake item);
        Task<VehicleMake[]> SortFilterAsync(string sortOrder, string searchString, string currentFilter, int? page);
    }
}