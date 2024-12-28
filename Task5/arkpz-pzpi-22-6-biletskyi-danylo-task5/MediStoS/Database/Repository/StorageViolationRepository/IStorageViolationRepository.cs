using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.StorageViolationRepository
{
    public interface IStorageViolationRepository
    {
        Task<bool> AddStorageViolation(StorageViolation violation);
        Task<bool> DeleteStorageViolation(StorageViolation violation);
        Task<StorageViolation?> GetStorageViolation(int id, bool tracking = true);
        Task<List<StorageViolation>> GetStorageViolationsByWarehouseId(int warehouseId);
        Task<bool> IsWarehouseExist(int warehouseId);
    }
}