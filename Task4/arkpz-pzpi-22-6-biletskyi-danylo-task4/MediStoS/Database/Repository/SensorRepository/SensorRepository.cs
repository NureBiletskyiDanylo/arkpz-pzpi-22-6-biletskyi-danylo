using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace MediStoS.Database.Repository.SensorRepository;

public class SensorRepository(ApplicationDbContext context) : ISensorRepository
{
    public async Task<Sensor?> GetSensor(int id, bool tracking = true)
    {
        Sensor? sensor;
        if (tracking)
        {
            sensor = await context.Sensors.FirstOrDefaultAsync(a => a.Id == id);
        }
        else
        {
            sensor = await context.Sensors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        return sensor;
    }

    public async Task<bool> AddSensor(Sensor sensor)
    {
        await context.Sensors.AddAsync(sensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> DeleteSensor(Sensor sensor)
    {
        context.Sensors.Remove(sensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<bool> UpdateSensor(Sensor newSensor)
    {
        context.Sensors.Update(newSensor);
        var result = await context.SaveChangesAsync();
        return result != 0;
    }

    public async Task<List<Sensor>> GetSensorsByWarehouseId(int warehouseId)
    {
        List<Sensor> sensors = await context.Sensors.Where(a => a.WarehouseId == warehouseId).ToListAsync();
        if (sensors == null)
        {
            sensors = new List<Sensor>();
        }
        return sensors;
    }

    public async Task<bool> IsWarehouseExist(int warehouseId)
    {
        return await context.Warehouses.AnyAsync(a => a.Id == warehouseId);
    }
}
