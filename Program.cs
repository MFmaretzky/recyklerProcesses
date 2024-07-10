using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using Iot.Device.Pwm;
using System.Device.Pwm;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace console1 {
    class Program {
        static void Main(string[] args) {
            GpioController gpio = new();
            
            FrontHatch frontHatch = new FrontHatch(
                upperSensorPin: (int)RPiPin.GPIO12,
                lowerSensorPin: (int)RPiPin.GPIO21,
                middleSensorPin: (int)RPiPin.GPIO14,
                bridgeEnablePin: (int)RPiPin.GPIO18,
                cwPolarisationPin: (int)RPiPin.GPIO24,
                ccwPolarisationPin: (int)RPiPin.GPIO23
            );

            frontHatch.SensorsInit(gpio);
            Console.WriteLine("Wpisz 'o' by otworzyć przegrodę lub 'c' by zamknąć.");
            Console.Write("Twoj wybor: ");

            while (true) {
                frontHatch.Process(gpio);
                frontHatch.GetUpperStatus(gpio);
            }
        }        
    }
}

