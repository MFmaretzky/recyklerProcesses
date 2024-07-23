using System.Device.Gpio;
using System.Device.Pwm;

namespace console1 {
    class RotorCtrl {
        private double dutyCycle = 0.01;
        public double DutyCycle {
            get { return dutyCycle; }
            set { dutyCycle = value; }
        }
        public void StartClockwise(double steepLevel) {
            var pwmPin18 = PwmChannel.Create(2, 2, 400, 0.0); // chip2 channel2 - pin18
            pwmPin18.Start();
            pwmPin18.DutyCycle = 0;

            while (pwmPin18.DutyCycle < 0.95) {
                pwmPin18.DutyCycle += steepLevel;

                Thread.Sleep(10);  
            }
            if (pwmPin18.DutyCycle >= 0.95) { 
                Thread.Sleep(1000);
            } 
            
            while (pwmPin18.DutyCycle > 0.05) {
                pwmPin18.DutyCycle -= steepLevel;
                
                Thread.Sleep(10);  
            }
            
            pwmPin18.DutyCycle = 0;
            pwmPin18.Stop();
            pwmPin18.Dispose();
        }
        public void StartCounterClockwise(double steepLevel) {
            var pwmPin19 = PwmChannel.Create(2, 3, 400, 0.0); // chip2 channel3 - pin19
            pwmPin19.Start();
            pwmPin19.DutyCycle = 0;

            while (pwmPin19.DutyCycle < 0.95) {
                pwmPin19.DutyCycle += steepLevel;

                Thread.Sleep(10);  
            }
            if (pwmPin19.DutyCycle >= 0.95) {
                Thread.Sleep(1000);
            }
            
            while (pwmPin19.DutyCycle > 0.05) {
                pwmPin19.DutyCycle -= steepLevel;
                
                Thread.Sleep(10);  
            }

            pwmPin19.DutyCycle = 0;
            pwmPin19.Stop();
            pwmPin19.Dispose();
        }
    }
}