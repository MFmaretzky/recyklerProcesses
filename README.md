## Table of contents 
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Configuration](#configuration)
* [TODO](#TODO)

## General info
This project is focused on handling processes of Reverse-Vending machine using Raspberry PI 5 in .NET 8.0 enviroment.

## Technologies
Project is created with:
* .NET 8.0 SDK 
* C# System.Device.Gpio
* Raspberry PI 5 8GB
* more in the future :D

## Setup
To run this project download following NuGet packages:
* System.Device.Gpio
* System.IO
* Iot.Device.Pwm
Also make sure you got latest .NET SDK release and assemble your board which has to be connected to Raspberry PI 5. Fastest 64GB SD card recommended for the highest throughput due to high load. 
 
## Configuration
To properly run the device, set the parameters of the FrontHatch constructor with GPIO## with corresponding I/O pins on your Raspberry PI. Make sure always to initialise sensor pins with SensorInit method.

## TODO
* Ogarnij układ czujników npn
* I2C do kompomentu na nowe GPIO