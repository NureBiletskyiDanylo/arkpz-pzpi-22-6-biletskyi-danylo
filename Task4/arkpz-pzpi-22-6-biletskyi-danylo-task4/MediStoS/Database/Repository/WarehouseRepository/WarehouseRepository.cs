using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.WarehouseRepository;

public class WarehouseRepository(ApplicationDbContext context) : IWarehouseRepository
{
    public async Task<Warehouse?> GetWarehouse(int id, bool tracking = true)
    {
        Warehouse? warehouse;
        if (tracking)
        {
            warehouse = await context.Warehouses.FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            warehouse = await context.Warehouses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        return warehouse;
    }

    public async Task<bool> AddWarehouse(Warehouse warehouse)
    {
        await context.Warehouses.AddAsync(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteWarehouse(Warehouse warehouse)
    {
        context.Remove(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateWarehouse(Warehouse warehouse)
    {
        context.Warehouses.Update(warehouse);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<Warehouse>> GetWarehouses()
    {
        List<Warehouse> warehouses = await context.Warehouses.ToListAsync();
        if (warehouses == null) return new List<Warehouse>();
        return warehouses;
    }
}
