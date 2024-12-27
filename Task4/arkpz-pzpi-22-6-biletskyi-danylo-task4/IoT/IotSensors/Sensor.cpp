#include "Sensor.h"
#include <random>

Sensor::Sensor() : id(0), type(0), currentValue(0) {}

Sensor::Sensor(int id, int type) : id(id), type(type), currentValue(0) {}

void Sensor::generateRandomValue() {
    std::random_device rd;
    std::mt19937 gen(rd());
    if (type == 0) {
        std::uniform_real_distribution<> dist(253.15, 323.15);
        currentValue = dist(gen);
        currentValue = convertKelvinToCelsius(currentValue);
    } else if (type == 1) {
        std::uniform_real_distribution<> dist(0.0, 30.0);
        currentValue = dist(gen);
    }
}

double Sensor::convertKelvinToCelsius(double kelvin) {
    double celsium = kelvin - 273.15;
    return celsium;
}
