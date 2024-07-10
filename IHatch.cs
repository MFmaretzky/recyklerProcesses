using System.Device.Gpio;

namespace console1 {
    interface IHatch {
        void SensorsInit(GpioController gpio);
        void OpenOutPins(GpioController gpio);
        void CloseOutPins(GpioController gpio);
        void OpenHatch(GpioController gpio);
        void CloseHatch(GpioController gpio);
        PinValue GetUpperStatus(GpioController gpio);
        PinValue GetMiddleStatus(GpioController gpio);
        bool GetLowerStatus(GpioController gpio);
        void Process(GpioController gpio);
    }
}
