#ifndef IOTCLIENT_H
#define IOTCLIENT_H

#include <string>
#include <vector>
#include "Sensor.h"

class IoTClient {
private:
    std::vector<Sensor> sensors;

    static size_t WriteCallback(void* contents, size_t size, size_t nmemb, std::string* out);
    std::string fetchSensorsFromServer(const std::string& url);
    void sendSensorData(const std::string& url, const Sensor& sensor);

public:
    void initialize(const std::string& apiUrl);
    void simulate(const std::string& updateUrl, int updateIntervalMs);
};

#endif // IOTCLIENT_H