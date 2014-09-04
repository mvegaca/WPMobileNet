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
            this._sensor.ReadingChanged += InclinometerReadingChanged;
        }

        internal void Stop()
        {
            this._sensor.ReadingChanged -= InclinometerReadingChanged;
        }
        #endregion
    }
}
