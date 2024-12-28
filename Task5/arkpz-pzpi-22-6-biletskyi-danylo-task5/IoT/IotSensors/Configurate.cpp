#include "Configurate.h"
#include <fstream>
#include <iostream>
#include "nlohmann/json.hpp"

bool loadValuesFromJson(std::string* fetchUrl, std::string* updateUrl, int* updateInterval) {
    std::ifstream settingsFile("settings.json");
    if (!settingsFile) {
        std::cerr << "Error: Could not open settings.json file!" << "\n";
        return false;
    }

    nlohmann::json settings;
    try {
        settingsFile >> settings;
    } catch (const nlohmann::json::parse_error& e) {
        std::cerr << "Error: Failed to parse settings.json - " << e.what() << "\n";
        return false;
    }

    *fetchUrl = settings.value("FetchSensorsUrl", "");
    *updateUrl = settings.value("SendSensorDataUrl", "");
    *updateInterval = settings.value("UpdateInterval", 15000);

    return true;
}
