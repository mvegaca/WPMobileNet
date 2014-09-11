using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Model;

namespace WPMobileNet.Service
{
    public class WeatherService : BaseService
    {
        #region Services
        private NetworkStatusService _networkStatusService;
        #endregion

        #region Properties
        private MWeather _weather;
        public MWeather Weather
        {
            get { return _weather; }
            private set { Set("Weather", ref _weather, value); }
        }        
        #endregion

        #region Constructors
        public WeatherService(NetworkStatusService networkStatusService) : base()
        {
            this._networkStatusService = networkStatusService;            
        }
        #endregion

        #region Methods
        //public async Task<MWeather> GetWeatherAsync()
        public async void GetWeather(double latitude, double longitude)        
        {
            string service = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}", latitude, longitude);            
            Uri apiUri = new Uri(service, UriKind.Absolute);
            //var result = await _networkStatusService.DownloadAsync(apiUri);
            var result = await _networkStatusService.DownloadAsync(apiUri);
            this.Weather = Newtonsoft.Json.JsonConvert.DeserializeObject<MWeather>(result);
        }
        #endregion
    }
}
