using System.Device.Gpio;
using System.Threading;

namespace console1 {
    public abstract class Hatch : IHatch {
        private int _upperSensorPin;
        private int _lowerSensorPin;
        private int _middleSensorPin;
        private int _bridgeEnablePin;
        private int _CWPolarisationPin;
        private int _CCwPolarisationPin;

        public int UpperSensorPin {
            get { return _upperSensorPin; }
            set { _upperSensorPin = value; }
        }

        public int LowerSensorPin {
            get { return _lowerSensorPin; }
            set { _lowerSensorPin = value; }
        }

        public int MiddleSensorPin {
            get { return _middleSensorPin; }
            set { _middleSensorPin = value; }
        }

        public int BridgeEnablePin {
            get { return _bridgeEnablePin; }
            set { _bridgeEnablePin = value; }
        }

        public int CWPolarisationPin {
            get { return _CWPolarisationPin; }
            set { _CWPolarisationPin = value; }
        }

        public int CCwPolarisationPin {
            get { return _CCwPolarisationPin; }
            set { _CCwPolarisationPin = value; }
        }

        public virtual void OpenOutPins(GpioController gpio) {
            gpio.OpenPin(this.BridgeEnablePin, PinMode.Output);
            gpio.OpenPin(this.CWPolarisationPin, PinMode.Output);
            gpio.OpenPin(this.CCwPolarisationPin, PinMode.Output);
        }

        public virtual void CloseOutPins(GpioController gpio) {
            gpio.ClosePin(this.BridgeEnablePin);
            gpio.ClosePin(this.CWPolarisationPin);
            gpio.ClosePin(this.CCwPolarisationPin);
        }

        public virtual void OpenHatch(GpioController gpio, double steepLevel) {
            RotorCtrl rotor = new RotorCtrl();
            gpio.OpenPin(23, PinMode.Output);
            gpio.Write(23, PinValue.High);

            rotor.StartClockwise(steepLevel);

            gpio.Write(23, PinValue.Low);
            gpio.ClosePin(23);
        }
        public virtual void CloseHatch(GpioController gpio, double steepLevel) {
            RotorCtrl rotor = new RotorCtrl();
            gpio.OpenPin(23, PinMode.Output);
            gpio.Write(23, PinValue.High);

            rotor.StartCounterClockwise(steepLevel);

            gpio.Write(23, PinValue.Low);
            gpio.ClosePin(23);
        }

        public virtual PinValue GetUpperStatus(GpioController gpio) {
            Console.WriteLine("Stan górnego czujnika: " + gpio.Read(this.UpperSensorPin));
            return gpio.Read(this.UpperSensorPin); 
        }

        public virtual PinValue GetMiddleStatus(GpioController gpio) {
            Console.WriteLine("Stan dolnego czujnika " + gpio.Read(this.MiddleSensorPin));
            return gpio.Read(this.MiddleSensorPin); 
        }

        public virtual bool GetLowerStatus(GpioController gpio) {
            // Console.WriteLine("Stan dolnego czujnika " + (bool)gpio.Read(this.LowerSensorPin));
            // return (bool)gpio.Read(this.LowerSensorPin); 
            return true;
        }

        public virtual void Process(GpioController gpio) {
            throw new NotImplementedException();
        }
    }
}