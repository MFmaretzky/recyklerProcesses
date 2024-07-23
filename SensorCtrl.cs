using System.Device.Gpio;

namespace console1 {
    class SensorCtrl {
        private int sensorPin;
        public int SensorPin {
            get { return sensorPin; }
            set { sensorPin = value; }
        }
        public void Init(GpioController gpio) {
            gpio.OpenPin(sensorPin, PinMode.InputPullDown);
        }
        public void DeInit(GpioController gpio) {
            gpio.ClosePin(sensorPin);
        }
        public bool GetStatus(GpioController gpio) {
            if (gpio.Read(this.sensorPin) != PinValue.Low) {
                return true;
            } else {
                return false;
            }
        }
    }
}