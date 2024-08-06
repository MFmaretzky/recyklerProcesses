using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Device.Pwm;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.Pwm;
using Swan;

namespace console1
{
    class Program
    {
        static void Main()
        {
            FrontHatch frontHatch = new FrontHatch
            {
                UpperSensorPin = 16,
                LowerSensorPin = 12,
                MiddleSensorPin = 25
            };
            var gpio = new GpioController();
            frontHatch.SenInit(gpio);
            while (true)
            {
                var statuses = frontHatch.GetSenStat(gpio);
                foreach (var status in statuses)
                {
                    Console.WriteLine($"{!status}");
                }
                Console.WriteLine("");
                Thread.Sleep(2000);
            }

            // while (true) {
            //     frontHatch.Process(gpio);
            //     Console.Write("Do you wish to continue? (Y/n): ");
            //     string? input = Console.ReadLine(); // Read entire line which might return null

            //     if (input != null) {
            //         input = input.Trim().ToLower();
            //     }

            //     if (input == "n") {
            //         break;
            //     }
            // }
            // gpio.Dispose();
        }
    }
}




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
