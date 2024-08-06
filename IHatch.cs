using System.Device.Gpio;

namespace console1
{
    interface IHatch
    {
        void OpenHatch(GpioController gpio);
        void CloseHatch(GpioController gpio);
        void Process(GpioController gpio);
        public void SenInit(GpioController gpio);
        public void SenDeInit(GpioController gpio);
        List<PinValue> GetSenStat(GpioController gpio);
    }
}
