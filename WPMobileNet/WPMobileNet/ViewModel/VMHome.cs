using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Service;

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
        private VMStatus _status;
        public VMStatus Status
        {
            get { return _status; }
            set { Set("Status", ref _status, value); }
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
            this.Status = new VMStatus();
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
        private async void GetData()
        {
            this.IsProgressIndicatorVisible = true;
            this.Status.Model.PingMilliseconds = await _pingService.PingAsyc();
            await this._networkStatusService.UpdateNetworkInterfaceInfo();
            this.Status.Model.NetWorkOperator = this._networkStatusService.GetNetWorkOperator();
            this.Status.Model.InterfaceName = this._networkStatusService.GetInterfaceName();
            this.Status.Model.InterfaceState = this._networkStatusService.GetInterfaceState();
            this.Status.Model.InterfaceType = this._networkStatusService.GetInterfaceType();
            this.Status.Model.InterfaceSubtype = this._networkStatusService.GetInterfaceSubtype();
            this.Status.Model.MobileDataTechnology = this._networkStatusService.GetMobileDataTechnology().ToString();
            this.Status.Model.DeviceName = this._deviceService.GetDeviceName();
            this.Status.Model.DeviceManufacturer = this._deviceService.GetDeviceManufacturer();
            this.Status.Model.DeviceHardwareVersion = this._deviceService.GetDeviceHardwareVersion();
            this.Status.Model.DeviceFirmwareVersion = this._deviceService.GetDeviceFirmwareVersion();
            this.Status.Model.IsPluggedIn = this._deviceService.IsPluggedIn();
            this.Status.Model.RemainingChargePercent = this._deviceService.GetRemainingChargePercent();
            await this._locationService.GetCurrentLocation();
            this.IsProgressIndicatorVisible = false;
        }
        #endregion
    }
}
