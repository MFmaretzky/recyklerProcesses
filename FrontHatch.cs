using System.Device.Gpio;
using Iot.Device.Gpio.Drivers;
using System.Threading;

namespace console1 {
    public sealed class FrontHatch : Hatch {
        public override void Process(GpioController gpio) {
            Console.Write("(O)pen or (c)lose?: ");
            string? input = Console.ReadLine();

            if (input != null) {
                input = input.Trim().ToLower();
            }

            if (input == "o") {
                OpenHatch(gpio, 0.002);
            } else if (input == "c") {
                CloseHatch(gpio, 0.002);                
            }
        }
    }
}
