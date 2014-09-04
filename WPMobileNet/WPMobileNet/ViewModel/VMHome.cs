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
        public LocationService LocationService
        {
            get { return _locationService; }
        }

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
            this._locationService.StatusChanged += _locationService_StatusChanged;
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
            GetData();
        }
        #endregion
        #endregion

        #region Methods
        private void GetData()
        {
            this.IsProgressIndicatorVisible = true;
            this.GetDeviceData();
            this.GetNetworkData();
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
        #endregion

        #region Events
        private void _locationService_PositionChanged(object sender, Geoposition geoposition)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.Status.Model.Latitude = geoposition.Coordinate.Latitude;
                this.Status.Model.Longitude = geoposition.Coordinate.Longitude;
                var speed = geoposition.Coordinate.Speed;
                if (this.Status.Model.Speed != speed)
                {
                    this.Status.Model.Speed = speed;
                }
                this.Status.Model.PositionSource = geoposition.Coordinate.PositionSource.ToString();
                this.Status.Model.HorizontalDilutionOfPrecision = geoposition.Coordinate.SatelliteData.HorizontalDilutionOfPrecision;
                this.Status.Model.VerticalDilutionOfPrecision = geoposition.Coordinate.SatelliteData.VerticalDilutionOfPrecision;
                this.Status.Model.PositionDilutionOfPrecision = geoposition.Coordinate.SatelliteData.PositionDilutionOfPrecision;
                this.Status.Model.Heading = geoposition.Coordinate.Heading.Value;
                this.Status.Model.Accuracy = geoposition.Coordinate.Accuracy;
                this.Status.Model.Altitude = geoposition.Coordinate.Altitude;
                this.Status.Model.AltitudeAccuracy = geoposition.Coordinate.AltitudeAccuracy;
                if (geoposition.CivicAddress != null)
                {
                    this.Status.Model.City = geoposition.CivicAddress.City;
                    this.Status.Model.State = geoposition.CivicAddress.State;
                    this.Status.Model.Country = geoposition.CivicAddress.Country;
                    this.Status.Model.PostalCode = geoposition.CivicAddress.PostalCode;
                }
            });
        }
        private void _locationService_StatusChanged(object sender, string e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.Status.Model.PositionStatus = e;
                if (e.Equals(PositionStatus.Disabled.ToString()) || e.Equals("DeniedByUser"))
                {
                    this.Status.Model.Latitude = 0;
                    this.Status.Model.Longitude = 0;
                    this.Status.Model.Speed = 0;
                    this.Status.Model.PositionSource = string.Empty;
                    this.Status.Model.HorizontalDilutionOfPrecision = 0;
                    this.Status.Model.VerticalDilutionOfPrecision = 0;
                    this.Status.Model.PositionDilutionOfPrecision = 0;
                    this.Status.Model.Heading = 0;
                    this.Status.Model.Accuracy = 0;
                    this.Status.Model.Altitude = 0;
                    this.Status.Model.AltitudeAccuracy = 0;
                    this.Status.Model.City = string.Empty;
                    this.Status.Model.State = string.Empty;
                    this.Status.Model.Country = string.Empty;
                    this.Status.Model.PostalCode = string.Empty;
                }                
            });
        }
        #endregion
    }
}
