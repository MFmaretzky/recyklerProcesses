using System.Device.Gpio;
using Iot.Device.Gpio.Drivers;
using System.Threading;

namespace console1 {
    public sealed class FrontHatch : Hatch {
        public FrontHatch(int upperSensorPin, int lowerSensorPin, int middleSensorPin, int bridgeEnablePin, int cwPolarisationPin, int ccwPolarisationPin) 
        : base(upperSensorPin, lowerSensorPin, middleSensorPin, bridgeEnablePin, cwPolarisationPin, ccwPolarisationPin) {

        }
        
    }
}
