using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace WPMobileNet.Service
{
    public class InclinometerService : BaseService
    {
        #region Properties
        private readonly Inclinometer _sensor;
        #endregion

        #region Constructors
        public InclinometerService()
            : base()
        {
            _sensor = Inclinometer.GetDefault();
            if (_sensor == null)
            {
                var title = StringResourceService.GetResource(StringResourceService.ResourceKeys.SensorNotFoundTitle);
                var message = StringResourceService.GetResource(StringResourceService.ResourceKeys.SensorNotFoundMessage);
                MessageBoxService.Show(title, message, MessageBoxService.MessageButtonType.Ok);
            }
        }
        #endregion

        #region Events
        public event EventHandler<InclinometerReadingChangedEventArgs> ReadingChanged;
        private void InclinometerReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
        }
        #endregion

        #region Methods
        internal void Start()
        {
            if (_sensor != null)
            {
                this._sensor.ReadingChanged += InclinometerReadingChanged;
            }            
        }

        internal void Stop()
        {
            if (_sensor != null)
            {
                this._sensor.ReadingChanged -= InclinometerReadingChanged;
            }
        }
        #endregion
    }
}
