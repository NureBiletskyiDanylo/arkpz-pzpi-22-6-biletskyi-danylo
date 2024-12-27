#include "IoTClient.h"
#include "Configurate.h"
#include <string>

int main() {
    IoTClient client;

    std::string fetchUrl;
    std::string updateUrl;
    int updateInterval;

    if (!loadValuesFromJson(&fetchUrl, &updateUrl, &updateInterval)) {
        return 1;
    }

    client.initialize(fetchUrl);
    client.simulate(updateUrl, updateInterval);

    return 0;
}
