using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace WPMobileNet.Service
{
    public class GyrometerService : BaseService
    {
        #region Properties
        private readonly Gyrometer _sensor;        
        #endregion

        #region Constructors
        public GyrometerService() : base()
        {
            _sensor = Gyrometer.GetDefault();
        }
        #endregion

        #region Events
        public event EventHandler<GyrometerReadingChangedEventArgs> ReadingChanged;
        private void GyrometerReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
        }
        #endregion

        #region Methods
        internal void Start()
        {
            this._sensor.ReadingChanged += GyrometerReadingChanged;
        }

        internal void Stop()
        {
            this._sensor.ReadingChanged -= GyrometerReadingChanged;
        }
        #endregion
    }
}
