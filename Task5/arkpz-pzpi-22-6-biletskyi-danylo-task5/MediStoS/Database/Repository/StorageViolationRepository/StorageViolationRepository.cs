using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.StorageViolationRepository;

public class StorageViolationRepository(ApplicationDbContext context) : IStorageViolationRepository
{
    public async Task<StorageViolation?> GetStorageViolation(int id, bool tracking = true)
    {
        StorageViolation? violation;
        if (tracking)
        {
            violation = await context.StorageViolations.FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            violation = await context.StorageViolations.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        return violation;
    }

    public async Task<bool> AddStorageViolation(StorageViolation violation)
    {
        await context.StorageViolations.AddAsync(violation);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteStorageViolation(StorageViolation violation)
    {
        context.StorageViolations.Remove(violation);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<StorageViolation>> GetStorageViolationsByWarehouseId(int warehouseId)
    {
        List<StorageViolation> violations = await context.StorageViolations.Where(a => a.WarehouseId == warehouseId).ToListAsync();
        if (violations == null)
        {
            return new List<StorageViolation>();
        }
        return violations;
    }

    public async Task<bool> IsWarehouseExist(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }
}
