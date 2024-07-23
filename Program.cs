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
        static void Main() {
            FrontHatch frontHatch = new FrontHatch();
            var gpio = new GpioController();

            while (true) {
                frontHatch.Process(gpio);
                Console.Write("Do you wish to continue? (Y/n): ");
                string? input = Console.ReadLine(); // Read entire line which might return null

                if (input != null) {
                    input = input.Trim().ToLower();
                }

                if (input == "n") {
                    break;
                }
            }
            gpio.Dispose(); 
        }        
    }
}

            // while (true) {
            //     gpio.Write(25, PinValue.High);
            //     //Console.WriteLine("High");
            //     Console.WriteLine(gpio.Read(12));
            //     Thread.Sleep(1000);
            //     gpio.Write(25, PinValue.Low);
            //     //Console.WriteLine("Low");
            //     Thread.Sleep(1000);
            // }


    // FrontHatch frontHatch = new FrontHatch(
            //     upperSensorPin: (int)RPiPin.GPIO12,
            //     lowerSensorPin: (int)RPiPin.GPIO21,
            //     middleSensorPin: (int)RPiPin.GPIO14,
            //     bridgeEnablePin: (int)RPiPin.GPIO18,
            //     cwPolarisationPin: (int)RPiPin.GPIO24,
            //     ccwPolarisationPin: (int)RPiPin.GPIO23
            // );

            // frontHatch.SensorsInit(gpio);
            // Console.WriteLine("Wpisz 'o' by otworzyć przegrodę lub 'c' by zamknąć.");
            // Console.Write("Twoj wybor: ");
            // int i = 0;
            // while (i < 10) {
            //     frontHatch.Process(gpio);
            //     frontHatch.GetUpperStatus(gpio);
            //     i++;
            // }

