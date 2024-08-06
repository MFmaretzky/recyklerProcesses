using System.Device.Gpio;
using System.Threading;
using Iot.Device.Gpio.Drivers;

namespace console1
{
    public sealed class FrontHatch : Hatch
    {
        public FrontHatch()
        {
            BridgeEnablePin = 23;
        }

        public override void OpenHatch(GpioController gpio)
        {
            RotorCtrl rotor = new(2, 2, 400, 0.0, 0.05, 0.95);
            var pwmPin = rotor.Init();
            gpio.OpenPin(_bridgeEnablePin, PinMode.Output);
            gpio.Write(_bridgeEnablePin, PinValue.High);

            rotor.StartClockwise(pwmPin);
            do
            {
                ;
            } while (false);
            rotor.Stop(pwmPin);

            gpio.Write(_bridgeEnablePin, PinValue.Low);
            gpio.ClosePin(_bridgeEnablePin);
        }

        public override void CloseHatch(GpioController gpio)
        {
            RotorCtrl rotor = new(2, 3, 400, 0.0, 0.05, 0.95);
            var pwmPin = rotor.Init();
            gpio.OpenPin(_bridgeEnablePin, PinMode.Output);
            gpio.Write(_bridgeEnablePin, PinValue.High);

            rotor.StartCounterClockwise(pwmPin);
            do
            {
                ;
            } while (false);
            rotor.Stop(pwmPin);

            gpio.Write(_bridgeEnablePin, PinValue.Low);
            gpio.ClosePin(_bridgeEnablePin);
        }

        public override void Process(GpioController gpio)
        {
            Console.Write("(O)pen or (c)lose?: ");
            string? input = Console.ReadLine();

            if (input != null)
            {
                input = input.Trim().ToLower();
            }

            if (input == "o")
            {
                this.OpenHatch(gpio);
            }
            else if (input == "c")
            {
                this.CloseHatch(gpio);
            }
        }
    }
}
