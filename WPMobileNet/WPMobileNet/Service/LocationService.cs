using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace WPMobileNet.Service
{
    public class LocationService : BaseService
    {
        #region Strings
        private string SKLocationPermission = "LocationPermission";
        public string AskLocationPermissionTitle = "Location";
        public string AskLocationPermissionMessage = "This application needs access to your location. Are you agree?";
        #endregion

        #region Services
        private readonly MessageBoxService _messageBoxService;
        private readonly StateService _stateService;
        #endregion

        #region Properties
        private Geolocator _geolocator;
        private Geolocator Geolocator
        {
            get
            {

                if (_geolocator == null)
                {
                    _geolocator = new Geolocator();
                    _geolocator.DesiredAccuracy = PositionAccuracy.High;
                    _geolocator.DesiredAccuracyInMeters = 1;
                    _geolocator.MovementThreshold = 1;
                }
                return _geolocator;
            }
        }
        private bool? _hasPermission;
        public bool? HasPermission
        {
            get
            {
                if (_hasPermission == null) _hasPermission = _stateService.GetState<bool?>(SKLocationPermission, true);
                if (_hasPermission == null)
                {
                    var result = _messageBoxService.Show(AskLocationPermissionTitle, AskLocationPermissionMessage, MessageBoxService.MessageButtonType.YesNo);
                    if (result.Equals(MessageBoxResult.OK))
                    {
                        AnalyticsService.SendEvent(AnalyticsService.EventCategory.Actions, AnalyticsService.EventAction.AcceptAccessLocation);
                        _hasPermission = true;
                    }
                    else
                    {
                        AnalyticsService.SendEvent(AnalyticsService.EventCategory.Actions, AnalyticsService.EventAction.DenyAccessLocation);
                        _hasPermission = false;
                    }
                    _stateService.SetState(SKLocationPermission, _hasPermission, true);
                }
                return _hasPermission;
            }
            set
            {
                Set("HasPermission", ref _hasPermission, value);
                _stateService.SetState(SKLocationPermission, _hasPermission, true);
                CheckPermission();
            }
        }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        #endregion

        #region Events
        public event EventHandler<Geoposition> PositionChanged;
        public event EventHandler<string> StatusChanged;
        private void _geolocatorPositionChanged(Windows.Devices.Geolocation.Geolocator sender, PositionChangedEventArgs args)
        {
            this.Latitude = args.Position.Coordinate.Latitude;
            this.Longitude = args.Position.Coordinate.Longitude;
            if (PositionChanged != null) PositionChanged(this.Geolocator, args.Position);
        }
        private void _geolocator_StatusChanged(Windows.Devices.Geolocation.Geolocator sender, StatusChangedEventArgs args)
        {
            if (StatusChanged != null) StatusChanged(this.Geolocator, args.Status.ToString());
        }
        #endregion

        #region Constructors
        public LocationService(MessageBoxService messageBoxService, StateService stateService)
        {
            this._messageBoxService = messageBoxService;
            this._stateService = stateService;
            CheckPermission();
        }
        #endregion

        #region Methods
        private void CheckPermission()
        {
            if (HasPermission == true)
            {
                Geolocator.PositionChanged += _geolocatorPositionChanged;
                Geolocator.StatusChanged += _geolocator_StatusChanged;
            }
            else
            {
                Geolocator.PositionChanged -= _geolocatorPositionChanged;
                Geolocator.StatusChanged -= _geolocator_StatusChanged;
                if (StatusChanged != null) StatusChanged(null, "DeniedByUser");
            }
        }
        #endregion
    }
}
