# CMakeList.txt : CMake project for IoTClient, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

project ("IoTClient")

include_directories(C:\\ProgrammingPackages\\CPP\\VCPKG\\vcpkg\\packages\\curl_x64-windows\\include)
link_directories(C:\\ProgrammingPackages\\CPP\\VCPKG\\vcpkg\\packages\\curl_x64-windows\\lib)

include_directories(C:\\ProgrammingPackages\\CPP\\VCPKG\\vcpkg\\packages\\nlohmann-json_x64-windows\\include)
# Add source to this project's executable.
add_executable (IoTClient "Sensor.h" "Sensor.cpp" "IoTClient.h" "IoTClient.cpp" "Configurate.h" "Configurate.cpp" "Main.cpp")

target_link_libraries(IoTClient libcurl)

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET IoTClient PROPERTY CXX_STANDARD 20)
endif()

# TODO: Add tests and install targets if needed.
