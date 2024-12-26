using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.MedicineRepository
{
    public interface IMedicineRepository
    {
        Task<bool> AddMedicine(Medicine medicine);
        Task<bool> DeleteMedicine(Medicine medicine);
        Task<Medicine?> GetMedicine(int id, bool tracking = true);
        Task<List<Medicine>> GetMedicines();
        Task<bool> UpdateMedicine(Medicine newMedicine);
    }
}