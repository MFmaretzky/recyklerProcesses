using System.Device.Gpio;

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

        public Hatch(int upperSensorPin, int lowerSensorPin, int middleSensorPin, int bridgeEnablePin, int cwPolarisationPin, int ccwPolarisationPin) {
            _upperSensorPin = upperSensorPin;
            _lowerSensorPin = lowerSensorPin;
            _middleSensorPin = middleSensorPin;
            _bridgeEnablePin = bridgeEnablePin;
            _CWPolarisationPin = cwPolarisationPin;
            _CCwPolarisationPin = ccwPolarisationPin;
        }

        public virtual void SensorsInit(GpioController gpio) {
            gpio.OpenPin(this.UpperSensorPin, PinMode.Input);
            gpio.OpenPin(this.LowerSensorPin, PinMode.Input);
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

        public virtual void OpenHatch(GpioController gpio) {
            OpenOutPins(gpio);
            gpio.Write(this.BridgeEnablePin, PinValue.Low);
            gpio.Write(this.CWPolarisationPin, PinValue.Low);
            gpio.Write(this.CCwPolarisationPin, PinValue.Low);

            while (GetLowerStatus(gpio)) {
                gpio.Write(this.CWPolarisationPin, PinValue.High);
                gpio.Write(this.BridgeEnablePin, PinValue.High);
                Thread.Sleep(2000);
                if (1 == 1) {
                    break;
/* TODO: napisz warunek if middleSensor == high then break */
                }
            }

            gpio.Write(this.CWPolarisationPin, PinValue.Low);
            gpio.Write(this.CCwPolarisationPin, PinValue.Low);
            gpio.Write(this.BridgeEnablePin, PinValue.Low);

            CloseOutPins(gpio);
        }
        public virtual void CloseHatch(GpioController gpio) {
            OpenOutPins(gpio);
            gpio.Write(this.CWPolarisationPin, PinValue.Low);
            gpio.Write(this.CCwPolarisationPin, PinValue.Low);
            gpio.Write(this.BridgeEnablePin, PinValue.Low);

            while (true/*GetUpperStatus(gpio)*/) {
                gpio.Write(this.BridgeEnablePin, PinValue.High);
                gpio.Write(this.CCwPolarisationPin, PinValue.High);
                Thread.Sleep(2000);
                if (1 == 1) {
                    break;
/* TODO: napisz warunek if middleSensor == high then break */
                }
            }

            gpio.Write(this.CWPolarisationPin, PinValue.Low);
            gpio.Write(this.CCwPolarisationPin, PinValue.Low);
            gpio.Write(this.BridgeEnablePin, PinValue.Low);

            CloseOutPins(gpio);
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
            int query = Console.Read();
            if (query == 'o') {
                this.OpenHatch(gpio);
            } else if (query == 'c') {
                this.CloseHatch(gpio);                
            }
        }
    }
}