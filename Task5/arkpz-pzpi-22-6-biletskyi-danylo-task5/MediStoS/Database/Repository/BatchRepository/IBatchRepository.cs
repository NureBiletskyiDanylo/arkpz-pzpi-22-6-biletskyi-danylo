using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.BatchRepository
{
    public interface IBatchRepository
    {
        Task<bool> AddBatch(Batch batch);
        Task<bool> DeleteBatch(Batch batch);
        Task<Batch?> GetBatch(int batchId, bool tracking = true);
        Task<List<Batch>> GetBatchesByMedicineId(int medicineId);
        Task<bool> IsMedicineExists(int medicineId);
        Task<bool> IsUserExists(int userId);
        Task<bool> IsWarehouseExists(int warehouseId);
        Task<bool> UpdateBatch(Batch newBatch);
    }
}