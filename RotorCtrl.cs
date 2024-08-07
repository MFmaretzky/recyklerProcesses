using System.Device.Gpio;
using System.Device.Pwm;

namespace console1
{
    class RotorCtrl(
        int chip,
        int channel,
        int frequency,
        double dutyCycle,
        double steepLevel,
        double maxSpeed
    )
    {
        private readonly int _chip = chip;
        private readonly int _channel = channel;
        private readonly int _frequency = frequency;
        private readonly double _dutyCycle = dutyCycle;
        private readonly double _steepLevel = steepLevel;
        private readonly double _maxSpeed = maxSpeed;
        public PwmChannel Init()
        {
            var pwmPin = PwmChannel.Create(_chip, _channel, _frequency, _steepLevel);
            pwmPin.Start();
            return pwmPin;
        }
        public void StartClockwise(PwmChannel pwmPin)
        {
            // chip2 channel2 - pin18
            while (pwmPin.DutyCycle < 0.95)
            {
                pwmPin.DutyCycle += _steepLevel;
                Thread.Sleep(10);
            }
        }

        public void StartCounterClockwise(PwmChannel pwmPin)
        {
            // chip2 channel3 - pin19
            pwmPin.Start();
            while (pwmPin.DutyCycle < 0.95)
            {
                pwmPin.DutyCycle += _steepLevel;
                Thread.Sleep(10);
            }
        }
        public void Stop(PwmChannel pwmPin)
        {
            pwmPin.DutyCycle = 0;
            pwmPin.Stop();
            pwmPin.Dispose();
        }
    }
}
