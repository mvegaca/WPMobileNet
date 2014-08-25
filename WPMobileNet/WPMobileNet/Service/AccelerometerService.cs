using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace WPMobileNet.Service
{
    public class AccelerometerService : BaseService
    {
        #region Properties
        private readonly Accelerometer _sensor;        
        #endregion

        #region Events        
        public event EventHandler<AccelerometerReadingChangedEventArgs> ReadingChanged;
        private void _sensor_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
        }
        #endregion

        #region Constructor
        public AccelerometerService()
        {
            this._sensor = Accelerometer.GetDefault();
            this._sensor.ReadingChanged += _sensor_ReadingChanged;
        }
        #endregion
    }
}
