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
        public GyrometerService()
            : base()
        {
            _sensor = Gyrometer.GetDefault();
            if (_sensor == null)
            {
                var title = StringResourceService.GetResource(StringResourceService.ResourceKeys.SensorNotFoundTitle);
                var message = StringResourceService.GetResource(StringResourceService.ResourceKeys.SensorNotFoundMessage);
                MessageBoxService.Show(title, message, MessageBoxService.MessageButtonType.Ok);
            }
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
            if (_sensor != null) this._sensor.ReadingChanged += GyrometerReadingChanged;
        }

        internal void Stop()
        {
            if (_sensor != null) this._sensor.ReadingChanged -= GyrometerReadingChanged;
        }
        #endregion
    }
}
