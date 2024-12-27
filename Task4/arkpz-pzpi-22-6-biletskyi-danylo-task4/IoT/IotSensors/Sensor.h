#ifndef SENSOR_H
#define SENSOR_H

#include <string>
#include "nlohmann/json.hpp"

class Sensor {
public:
    int id;
    int type;
    float currentValue;

    Sensor();
    Sensor(int id, int type);

    void generateRandomValue();
    double convertKelvinToCelsius(double kelvin);
};

#endif // SENSOR_H