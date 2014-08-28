using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Windows.Devices.Geolocation;
using WPMobileNet.Service;
using WPMobileNet.Utils;

namespace WPMobileNet.ViewModel
{
    public class VMHome : VMBase
    {


        #region Services
        private readonly PingService _pingService;
        private readonly NetworkStatusService _networkStatusService;
        private readonly DeviceService _deviceService;
        private readonly LocationService _locationService;
        #endregion

        #region Properties
        private bool _isProgressIndicatorVisible;
        public bool IsProgressIndicatorVisible
        {
            get { return _isProgressIndicatorVisible; }
            set { Set("IsProgressIndicatorVisible", ref _isProgressIndicatorVisible, value); }
        }        
        
        #endregion

        #region Constructors
        public VMHome(NavigationService navigationService, PingService pingService, NetworkStatusService networkStatusService, DeviceService deviceService, LocationService locationService)
            : base(navigationService)
        {
            this._pingService = pingService;
            this._networkStatusService = networkStatusService;
            this._deviceService = deviceService;
            this._locationService = locationService;            
            this._locationService.PositionChanged += _locationService_PositionChanged;                                    
        }        
        #endregion

        #region Commands
        #region GetStatusCommand
        private RelayCommand _getStatusCommand;
        public RelayCommand GetStatusCommand
        {
            get
            {
                return _getStatusCommand ?? (_getStatusCommand = new RelayCommand(ExecuteGetStatusCommand));
            }
        }
        private void ExecuteGetStatusCommand()
        {
            GetData(EnumHelper.LocationOrigin.GetGeopositionAsync);
        }
        #endregion
        #endregion

        #region Methods
        private void GetData(EnumHelper.LocationOrigin locationOrigin)
        {
            this.IsProgressIndicatorVisible = true;
            this.Status.Model.LocationOrigin = locationOrigin;
            this.GetDeviceData();
            this.GetNetworkData();
            this.GetLocationData();
            this.IsProgressIndicatorVisible = false;
        }
        private async void GetNetworkData()
        {
            try
            {
                this.Status.Model.PingMilliseconds = await _pingService.PingAsyc();
            }
            catch (System.Exception)
            {
            }
            try
            {
                await this._networkStatusService.UpdateNetworkInterfaceInfo();
                this.Status.Model.NetWorkOperator = this._networkStatusService.GetNetWorkOperator();
                this.Status.Model.InterfaceName = this._networkStatusService.GetInterfaceName();
                this.Status.Model.InterfaceState = this._networkStatusService.GetInterfaceState();
                this.Status.Model.InterfaceType = this._networkStatusService.GetInterfaceType();
                this.Status.Model.InterfaceSubtype = this._networkStatusService.GetInterfaceSubtype();
                this.Status.Model.MobileDataTechnology = this._networkStatusService.GetMobileDataTechnology().ToString();
            }
            catch (System.Exception)
            {
            }
        }
        private void GetDeviceData()
        {
            try
            {
                this.Status.Model.DeviceName = this._deviceService.GetDeviceName();
                this.Status.Model.DeviceManufacturer = this._deviceService.GetDeviceManufacturer();
                this.Status.Model.DeviceHardwareVersion = this._deviceService.GetDeviceHardwareVersion();
                this.Status.Model.DeviceFirmwareVersion = this._deviceService.GetDeviceFirmwareVersion();
                this.Status.Model.DeviceID = this._deviceService.GetDeviceID();
                this.Status.Model.PublisherHostId = this._deviceService.GetPublisherHostId();
                this.Status.Model.IsPluggedIn = this._deviceService.IsPluggedIn();
                this.Status.Model.RemainingChargePercent = this._deviceService.GetRemainingChargePercent();
                this.Status.Model.IsPowerSavingModeEnabled = this._deviceService.IsPowerSavingModeEnabled();
            }
            catch (System.Exception)
            {
            }
        }
        private async void GetLocationData()
        {
            try
            {
                await this._locationService.GetCurrentLocation();
                this.Status.Model.Latitude = this._locationService.CurrentLocation.Coordinate.Latitude;
                this.Status.Model.Longitude = this._locationService.CurrentLocation.Coordinate.Longitude;
                var speed = this._locationService.CurrentLocation.Coordinate.Speed;
                if (this.Status.Model.Speed != speed)
                {
                    this.Status.Model.Speed = speed;
                }
                this.Status.Model.PositionSource = this._locationService.CurrentLocation.Coordinate.PositionSource.ToString();
                this.Status.Model.HorizontalDilutionOfPrecision = this._locationService.CurrentLocation.Coordinate.SatelliteData.HorizontalDilutionOfPrecision;
                this.Status.Model.VerticalDilutionOfPrecision = this._locationService.CurrentLocation.Coordinate.SatelliteData.VerticalDilutionOfPrecision;
                this.Status.Model.PositionDilutionOfPrecision = this._locationService.CurrentLocation.Coordinate.SatelliteData.PositionDilutionOfPrecision;
                this.Status.Model.Heading = this._locationService.CurrentLocation.Coordinate.Heading.Value;
                this.Status.Model.Accuracy = this._locationService.CurrentLocation.Coordinate.Accuracy;
                this.Status.Model.Altitude = this._locationService.CurrentLocation.Coordinate.Altitude;
                this.Status.Model.AltitudeAccuracy = this._locationService.CurrentLocation.Coordinate.AltitudeAccuracy;
                if (this._locationService.CurrentLocation.CivicAddress != null)
                {
                    this.Status.Model.City = this._locationService.CurrentLocation.CivicAddress.City;
                    this.Status.Model.State = this._locationService.CurrentLocation.CivicAddress.State;
                    this.Status.Model.Country = this._locationService.CurrentLocation.CivicAddress.Country;
                    this.Status.Model.PostalCode = this._locationService.CurrentLocation.CivicAddress.PostalCode;
                }
            }
            catch (System.Exception)
            {
            }
        }        
        #endregion

        #region Events
        private void _locationService_PositionChanged(object sender, Geoposition geoposition)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var speed = geoposition.Coordinate.Speed;
                if (this.Status.Model.Speed != speed)
                {
                    this.Status.Model.Speed = speed;
                }
                this.Status.Model.LocationOrigin = EnumHelper.LocationOrigin.PositionChangeEvent;
            });
        }        
        #endregion
    }
}
