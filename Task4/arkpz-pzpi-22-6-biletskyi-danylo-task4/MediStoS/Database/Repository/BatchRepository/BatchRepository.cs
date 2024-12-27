using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.BatchRepository;

public class BatchRepository(ApplicationDbContext context) : IBatchRepository
{
    public async Task<Batch?> GetBatch(int batchId, bool tracking = true)
    {
        Batch? batch;
        if (tracking)
        {
            batch = await context.Batches.FirstOrDefaultAsync(a => a.Id == batchId);
        }
        else
        {
            batch = await context.Batches.AsNoTracking().FirstOrDefaultAsync(a => a.Id == batchId);
        }

        return batch;
    }

    public async Task<bool> AddBatch(Batch batch)
    {
        await context.Batches.AddAsync(batch);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteBatch(Batch batch)
    {
        context.Batches.Remove(batch);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateBatch(Batch newBatch)
    {
        context.Batches.Update(newBatch);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<Batch>> GetBatchesByMedicineId(int medicineId)
    {
        List<Batch> batches = await context.Batches.Where(a => a.MedicineId == medicineId).ToListAsync();
        if (batches == null)
        {
            return new List<Batch>();
        }
        return batches;
    }

    public async Task<bool> IsWarehouseExists(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }

    public async Task<bool> IsUserExists(int userId)
    {
        return await context.Users.AnyAsync(a => a.Id == userId);
    }

    public async Task<bool> IsMedicineExists(int medicineId)
    {
        return await context.Medicines.AnyAsync(a => a.Id == medicineId);
    }
}
