using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.WarehouseRepository
{
    public interface IWarehouseRepository
    {
        Task<bool> AddWarehouse(Warehouse warehouse);
        Task<bool> DeleteWarehouse(Warehouse warehouse);
        Task<Warehouse?> GetWarehouse(int id, bool tracking = true);
        Task<List<Warehouse>> GetWarehouses();
        Task<bool> UpdateWarehouse(Warehouse warehouse);
    }
}