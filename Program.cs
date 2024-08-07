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
            FrontHatch frontHatch = new(23, 16, 12, 25);
            GpioController gpio = new();
            frontHatch.SenInit(gpio);

            while (true)
            {
                frontHatch.Process(gpio);
                Console.Write("Do you wish to continue? (Y/n): ");
                string? input = Console.ReadLine();

                if (input != null)
                {
                    input = input.Trim().ToLower();
                }

                if (input == "n")
                {
                    break;
                }
            }

            frontHatch.SenDeInit(gpio);
            gpio.Dispose();
        }
    }
}