using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPMobileNet.Service;

namespace WPMobileNet.ViewModel
{
    public class VMWeather : VMBase
    {
        #region Services
        private WeatherService _weatherService;
        public WeatherService WeatherService { get { return _weatherService; } }
        public LocationService _locationService;
        #endregion

        #region Constructors
        public VMWeather(LocationService locationService, WeatherService weatherService)
            : base()
        {
            this._locationService = locationService;
            this._weatherService = weatherService;
            this._locationService.PositionChanged += PositionChanged;
            if (this._locationService.Latitude != null && this._locationService.Longitude != null)
            {
                this.WeatherService.GetWeather(this._locationService.Latitude.Value, this._locationService.Longitude.Value);
            }
        }
        private void PositionChanged(object sender, Windows.Devices.Geolocation.Geoposition e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (this.WeatherService.Weather == null) this.WeatherService.GetWeather(e.Coordinate.Latitude, e.Coordinate.Longitude);
            });
        }
        #endregion
    }
}
