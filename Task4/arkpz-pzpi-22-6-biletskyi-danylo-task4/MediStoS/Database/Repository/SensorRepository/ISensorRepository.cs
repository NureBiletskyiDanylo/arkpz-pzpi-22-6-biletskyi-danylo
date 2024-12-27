using MediStoS.Database.Models;

namespace MediStoS.Database.Repository.SensorRepository
{
    public interface ISensorRepository
    {
        Task<bool> AddSensor(Sensor sensor);
        Task<bool> DeleteSensor(Sensor sensor);
        Task<Sensor?> GetSensor(int id, bool tracking = true);
        Task<List<Sensor>> GetSensorsByWarehouseId(int warehouseId);
        Task<bool> IsWarehouseExist(int warehouseId);
        Task<bool> UpdateSensor(Sensor newSensor);
    }
}