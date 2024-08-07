using System.Device.Gpio;
using System.Threading;
using Iot.Device.Gpio.Drivers;

namespace console1
{
    public sealed class FrontHatch : Hatch
    {
        public FrontHatch(int bridgeEnablePin, int upperSensorPin, int lowerSensorPin, int middleSensorPin)
        {
            BridgeEnablePin = bridgeEnablePin;
            UpperSensorPin = upperSensorPin;
            LowerSensorPin = lowerSensorPin;
            MiddleSensorPin = middleSensorPin;
        }

        public override void OpenHatch(GpioController gpio)
        {
            gpio.OpenPin(_bridgeEnablePin, PinMode.Output);
            RotorCtrl rotor = new(2, 2, 400, 0.0, 0.05, 0.95);
            var pwmPin = rotor.Init();

            List<PinValue> statuses = GetSenStat(gpio);

            if (statuses[1] == PinValue.Low)
            {
                gpio.Write(_bridgeEnablePin, PinValue.High);
                rotor.StartClockwise(pwmPin);
                do
                {
                    statuses = GetSenStat(gpio);
                } while (statuses[0] == PinValue.High || statuses[2] == PinValue.High);
                rotor.Stop(pwmPin);
            }
            else
            {
                Console.WriteLine("Error, exiting now.");
            }

            gpio.Write(_bridgeEnablePin, PinValue.Low);
            gpio.ClosePin(_bridgeEnablePin);
        }

        public override void CloseHatch(GpioController gpio)
        {
            gpio.OpenPin(_bridgeEnablePin, PinMode.Output);
            RotorCtrl rotor = new(2, 3, 400, 0.0, 0.05, 0.95);
            var pwmPin = rotor.Init();

            List<PinValue> statuses = GetSenStat(gpio);

            if (statuses[0] == PinValue.Low)
            {
                gpio.Write(_bridgeEnablePin, PinValue.High);
                rotor.StartCounterClockwise(pwmPin);
                do
                {
                    statuses = GetSenStat(gpio);
                } while (statuses[1] == PinValue.High);
                rotor.Stop(pwmPin);
            }
            else
            {
                Console.WriteLine("Error, exiting now.");
            }

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
