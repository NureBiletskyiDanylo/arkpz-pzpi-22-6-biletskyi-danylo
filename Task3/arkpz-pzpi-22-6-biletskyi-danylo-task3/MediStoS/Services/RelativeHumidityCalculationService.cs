namespace MediStoS.Services;

public class RelativeHumidityCalculationService
{
    const double empiricalCoefficientOfTempIncrease = 17.67;
    const double differenceBetweenKelvinAndCelsius = 243.5;
    const double saturationAt0Degrees = 6.112;
    public float CalculateRelativeHumidity(double temperature, double absoluteHumidity)
    {
        double saturationVaporPressure = saturationAt0Degrees * 
            Math.Exp((empiricalCoefficientOfTempIncrease * temperature) 
            / (temperature + differenceBetweenKelvinAndCelsius));
        double dewPoint = CalculateDewPoint(absoluteHumidity);
        double actualVaporPressure = saturationAt0Degrees * 
            Math.Exp((empiricalCoefficientOfTempIncrease * dewPoint) 
            / (dewPoint + differenceBetweenKelvinAndCelsius));
        return (float)((actualVaporPressure / saturationVaporPressure) * 100);
    }
    private double CalculateDewPoint(double absoluteHumidity)
    {

        double gamma = Math.Log((absoluteHumidity / saturationAt0Degrees) + 1);
        double dewPoint = (differenceBetweenKelvinAndCelsius * gamma) 
            / (empiricalCoefficientOfTempIncrease - gamma);
        return dewPoint;
    }
}
