using MediStoS.Enums;

namespace MediStoS.DataTransferObjects;

public class SensorUpdateValueModel
{
    public int Id { get; set; }
    public SensorType Type { get; set; }
    public float Value { get; set; }
}
