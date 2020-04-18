using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models;
using System.Linq;

namespace Service.Services
{
    public interface IVehicleModelService
    {
        Task<VehicleModel[]> GetIncompleteItemsAsync();
        Task<VehicleModel> GetItemsDetailsAsync(Guid id);
        Task<bool> AddItemAsync(VehicleModel model);
        Task<bool> DeleteItemAsync(Guid id);
        Task<bool> EditItemAsync(Guid id, VehicleModel item);
        Task<VehicleModel[]> SortFilterAsync(string sortOrder, string searchString, string currentFilter, int? page);
        IQueryable<VehicleMake> GetManufacturersQuery();
    }
}
