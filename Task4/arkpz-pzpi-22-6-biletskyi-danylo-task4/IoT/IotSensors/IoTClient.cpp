#include "IoTClient.h"
#include <curl/curl.h>
#include <iostream>
#include <thread>
#include <chrono>
#include "nlohmann/json.hpp"

size_t IoTClient::WriteCallback(void* contents, size_t size, size_t nmemb, std::string* out) {
    size_t totalSize = size * nmemb;
    out->append((char*)contents, totalSize);
    return totalSize;
}

std::string IoTClient::fetchSensorsFromServer(const std::string& url) {
    CURL* curl;
    CURLcode res;
    std::string response;

    curl = curl_easy_init();
    if (curl) {
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &response);

        res = curl_easy_perform(curl);
        if (res != CURLE_OK) {
            std::cerr << "curl_easy_perform() failed: " << curl_easy_strerror(res) << std::endl;
        }

        curl_easy_cleanup(curl);
    }

    return response;
}

void IoTClient::sendSensorData(const std::string& url, const Sensor& sensor) {
    CURL* curl;
    CURLcode res;

    curl = curl_easy_init();
    if (curl) {
        std::string patchData = "{\"id\":" + std::to_string(sensor.id) +
                                ",\"type\":" + std::to_string(sensor.type) +
                                ",\"value\":" + std::to_string(sensor.currentValue) + "}";

        struct curl_slist* headers = nullptr;
        headers = curl_slist_append(headers, "Content-Type: application/json");
        curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
        curl_easy_setopt(curl, CURLOPT_VERBOSE, 0L); // Disable verbose output
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
        curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
        curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, "PATCH");
        curl_easy_setopt(curl, CURLOPT_POSTFIELDS, patchData.c_str());

        res = curl_easy_perform(curl);
        std::cout << "\n";
        if (res != CURLE_OK) {
            std::cerr << "Failed to send data: " << curl_easy_strerror(res) << std::endl;
        }

        curl_easy_cleanup(curl);
    }
}

void IoTClient::initialize(const std::string& apiUrl) {
    std::string response = fetchSensorsFromServer(apiUrl);

    try {
        auto json = nlohmann::json::parse(response);
        for (const auto& item : json) {
            Sensor sensor;
            sensor.id = item["id"];
            sensor.type = item["type"];
            sensors.push_back(sensor);
        }
    } catch (const std::exception& e) {
        std::cerr << "Error parsing JSON: " << e.what() << std::endl;
    }
}

void IoTClient::simulate(const std::string& updateUrl, int updateIntervalMs) {
    while (true) {
        for (auto& sensor : sensors) {
            sensor.generateRandomValue();
            sendSensorData(updateUrl, sensor);
        }
		std::this_thread::sleep_for(std::chrono::milliseconds(updateIntervalMs));
    }
}
