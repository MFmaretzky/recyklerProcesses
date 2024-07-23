using System.Device.Gpio;

namespace console1 {
    interface IHatch {
        void OpenOutPins(GpioController gpio);
        void CloseOutPins(GpioController gpio);
        void OpenHatch(GpioController gpio, double steepLevel);
        void CloseHatch(GpioController gpio, double steepLevel);
        PinValue GetUpperStatus(GpioController gpio);
        PinValue GetMiddleStatus(GpioController gpio);
        bool GetLowerStatus(GpioController gpio);
        void Process(GpioController gpio);
    }
}
