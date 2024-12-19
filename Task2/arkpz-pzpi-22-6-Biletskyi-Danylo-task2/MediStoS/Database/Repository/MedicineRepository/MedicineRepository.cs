using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.MedicineRepository;

public class MedicineRepository(ApplicationDbContext context) : IMedicineRepository
{
    public async Task<Medicine?> GetMedicine(int id, bool tracking = true)
    {
        Medicine? medicine;
        if (!tracking)
        {
            medicine = await context.Medicines.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            medicine = await context.Medicines.FirstOrDefaultAsync(a => a.Id == id);
        }
        return medicine;
    }


    public async Task<List<Medicine>> GetMedicines()
    {
        return await context.Medicines.ToListAsync();
    }

    public async Task<bool> AddMedicine(Medicine medicine)
    {
        await context.Medicines.AddAsync(medicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteMedicine(Medicine medicine)
    {
        context.Medicines.Remove(medicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateMedicine(Medicine newMedicine)
    {
        context.Entry(newMedicine).State = EntityState.Detached;
        context.Medicines.Update(newMedicine);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }
}
