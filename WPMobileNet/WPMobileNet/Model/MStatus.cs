using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Utils;
using WPMobileNet.ViewModel;

namespace WPMobileNet.Model
{
    [DataContract]
    public class MStatus : SimpleViewModelBase
    {
        #region Properties
        #region GlobalProperties
        private DateTime _creationTime;
        [DataMember]
        public DateTime CreationTime
        {
            get { return _creationTime; }
            set { Set("CreationTime", ref _creationTime, value); }
        }

        private DateTime _modificationTime;
        [DataMember]
        public DateTime ModificationTime
        {
            get { return _modificationTime; }
            set { Set("ModificationTime", ref _modificationTime, value); }
        }

        private string _from;
        [DataMember]
        public string From
        {
            get { return _from; }
            set { Set("From", ref _from, value); }
        }
        #endregion
        #region DeviceNetworkInfo
        private string _netWorkOperator;
        [DataMember]
        public string NetWorkOperator
        {
            get { return _netWorkOperator; }
            set { Set("NetWorkOperator", ref _netWorkOperator, value); }
        }
        private bool? _isWifiEnabled;
        [DataMember]
        public bool? IsWifiEnabled
        {
            get { return _isWifiEnabled; }
            set { Set("IsWifiEnabled", ref _isWifiEnabled, value); }
        }
        private bool? _isCellularDataEnabled;
        [DataMember]
        public bool? IsCellularDataEnabled
        {
            get { return _isCellularDataEnabled; }
            set { Set("IsCellularDataEnabled", ref _isCellularDataEnabled, value); }
        }
        private bool? _isCellularDataRoamingEnabled;
        [DataMember]
        public bool? IsCellularDataRoamingEnabled
        {
            get { return _isCellularDataRoamingEnabled; }
            set { Set("IsCellularDataRoamingEnabled", ref _isCellularDataRoamingEnabled, value); }
        }
        private bool? _isNetworkAvailable;
        [DataMember]
        public bool? IsNetworkAvailable
        {
            get { return _isNetworkAvailable; }
            set { Set("IsNetworkAvailable", ref _isNetworkAvailable, value); }
        }        
        #endregion
        #region NetworkInterfaceInfo
        private string _bandwidth;
        [DataMember]
        public string Bandwidth
        {
            get { return _bandwidth; }
            set { Set("Bandwidth", ref _bandwidth, value); }
        }
        private string _descripcion;
        [DataMember]
        public string Descripcion
        {
            get { return _descripcion; }
            set { Set("Descripcion", ref _descripcion, value); }
        }
        private string _interfaceName;
        [DataMember]
        public string InterfaceName
        {
            get { return _interfaceName; }
            set { Set("InterfaceName", ref _interfaceName, value); }
        }
        private string _interfaceState;
        [DataMember]
        public string InterfaceState
        {
            get { return _interfaceState; }
            set { Set("InterfaceState", ref _interfaceState, value); }
        }
        private string _interfaceType;
        [DataMember]
        public string InterfaceType
        {
            get { return _interfaceType; }
            set { Set("InterfaceType", ref _interfaceType, value); }
        }
        private string _interfaceSubtype;
        [DataMember]
        public string InterfaceSubtype
        {
            get { return _interfaceSubtype; }
            set { Set("InterfaceSubtype", ref _interfaceSubtype, value); }
        }
        private double _pingMilliseconds;
        [DataMember]
        public double PingMilliseconds
        {
            get { return _pingMilliseconds; }
            set { Set("PingMilliseconds", ref _pingMilliseconds, value); }
        }
        private string _mobileDataTechnology;
        [DataMember]
        public string MobileDataTechnology
        {
            get { return _mobileDataTechnology; }
            set { Set("MobileDataTechnology", ref _mobileDataTechnology, value); }
        }
        #endregion
        #region LocationInfo
        private double? _altitude;
        [DataMember]
        public double? Altitude
        {
            get { return _altitude; }
            set { Set("Altitude", ref _altitude, value); }
        }

        private double? _latitude;
        [DataMember]
        public double? Latitude
        {
            get { return _latitude; }
            set { Set("Latitude", ref _latitude, value); }
        }

        private double? _longitude;
        [DataMember]
        public double? Longitude
        {
            get { return _longitude; }
            set { Set("Longitude", ref _longitude, value); }
        }

        private double? _speed;
        [DataMember]
        public double? Speed
        {
            get { return _speed; }
            set { Set("Speed", ref _speed, value); }
        }

        private EnumHelper.LocationOrigin _locationOrigin;
        [DataMember]
        public EnumHelper.LocationOrigin LocationOrigin
        {
            get { return _locationOrigin; }
            set { Set("LocationOrigin", ref _locationOrigin, value); }
        }
        
        private string _positionSource;
        [DataMember]
        public string PositionSource
        {
            get { return _positionSource; }
            set { Set("PositionSource", ref _positionSource, value); }
        }

        private double? _horizontalDilutionOfPrecision;
        [DataMember]
        public double? HorizontalDilutionOfPrecision
        {
            get { return _horizontalDilutionOfPrecision; }
            set { Set("HorizontalDilutionOfPrecision", ref _horizontalDilutionOfPrecision, value); }
        }

        private double? _positionDilutionOfPrecision;
        [DataMember]
        public double? PositionDilutionOfPrecision
        {
            get { return _positionDilutionOfPrecision; }
            set { Set("PositionDilutionOfPrecision", ref _positionDilutionOfPrecision, value); }
        }

        private double? _verticalDilutionOfPrecision;
        [DataMember]
        public double? VerticalDilutionOfPrecision
        {
            get { return _verticalDilutionOfPrecision; }
            set { Set("VerticalDilutionOfPrecision", ref _verticalDilutionOfPrecision, value); }
        }
        private double? _heading;
        [DataMember]
        public double? Heading
        {
            get { return _heading; }
            set { Set("Heading", ref _heading, value); }
        }
        private double? _accuracy;
        [DataMember]
        public double? Accuracy
        {
            get { return _accuracy; }
            set { Set("Accuracy", ref _accuracy, value); }
        }
        private double? _altitudeAccuracy;
        [DataMember]
        public double? AltitudeAccuracy
        {
            get { return _altitudeAccuracy; }
            set { Set("AltitudeAccuracy", ref _altitudeAccuracy, value); }
        }
        private string _city;
        [DataMember]
        public string City
        {
            get { return _city; }
            set { Set("City", ref _city, value); }
        }
        private string _state;
        [DataMember]
        public string State
        {
            get { return _state; }
            set { Set("State", ref _state, value); }
        }
        private string _country;
        [DataMember]
        public string Country
        {
            get { return _country; }
            set { Set("Country", ref _country, value); }
        }
        private string _postalCode;
        [DataMember]
        public string PostalCode
        {
            get { return _postalCode; }
            set { Set("PostalCode", ref _postalCode, value); }
        }
        private bool? _isDeviceMoving;
        [DataMember]
        public bool? IsDeviceMoving
        {
            get { return _isDeviceMoving; }
            set { Set("IsDeviceMoving", ref _isDeviceMoving, value); }
        }

        #endregion
        #region Accelerometer
        private bool? _isMoving;
        [DataMember]
        public bool? IsMoving
        {
            get { return _isMoving; }
            set { Set("IsMoving", ref _isMoving, value); }
        }
        #endregion
        #region DeviceInformation
        private string _deviceName;
        [DataMember]
        public string DeviceName
        {
            get { return _deviceName; }
            set { Set("DeviceName", ref _deviceName, value); }
        }
        private string _deviceManufacturer;
        [DataMember]
        public string DeviceManufacturer
        {
            get { return _deviceManufacturer; }
            set { Set("DeviceManufacturer", ref _deviceManufacturer, value); }
        }
        private string _deviceHardwareVersion;
        [DataMember]
        public string DeviceHardwareVersion
        {
            get { return _deviceHardwareVersion; }
            set { Set("DeviceHardwareVersion", ref _deviceHardwareVersion, value); }
        }
        private string _deviceFirmwareVersion;
        [DataMember]
        public string DeviceFirmwareVersion
        {
            get { return _deviceFirmwareVersion; }
            set { Set("DeviceFirmwareVersion", ref _deviceFirmwareVersion, value); }
        }
        private string _deviceID;
        [DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { Set("DeviceID", ref _deviceID, value); }
        }
        private string _publisherHostId;
        [DataMember]
        public string PublisherHostId
        {
            get { return _publisherHostId; }
            set { Set("PublisherHostId", ref _publisherHostId, value); }
        }
        private long _deviceTotalMemory;
        [DataMember]
        public long DeviceTotalMemory
        {
            get { return _deviceTotalMemory; }
            set { Set("DeviceTotalMemory", ref _deviceTotalMemory, value); }
        }
        private bool _isPluggedIn;
        [DataMember]
        public bool IsPluggedIn
        {
            get { return _isPluggedIn; }
            set { Set("IsPluggedIn", ref _isPluggedIn, value); }
        }
        private bool _isPowerSavingModeEnabled;
        [DataMember]
        public bool IsPowerSavingModeEnabled
        {
            get { return _isPowerSavingModeEnabled; }
            set { Set("IsPowerSavingModeEnabled", ref _isPowerSavingModeEnabled, value); }
        }
        private int _remainingChargePercent;
        [DataMember]
        public int RemainingChargePercent
        {
            get { return _remainingChargePercent; }
            set { Set("RemainingChargePercent", ref _remainingChargePercent, value); }
        }
        #endregion
        #region Accelerometer
        private double _accelerationX;
        [DataMember]
        public double AccelerationX { get { return _accelerationX; } set { Set("AccelerationX", ref _accelerationX, value); } }
        private double _accelerationY;
        [DataMember]
        public double AccelerationY { get { return _accelerationY; } set { Set("AccelerationY", ref _accelerationY, value); } }
        private double _accelerationZ;
        [DataMember]
        public double AccelerationZ { get { return _accelerationZ; } set { Set("AccelerationZ", ref _accelerationZ, value); } }
        #endregion
        #endregion

        #region Constructors
        public MStatus() { this.CreationTime = DateTime.Now; }

        public MStatus(MStatus status)
        {
            this.Altitude = status.Altitude;
            this.Bandwidth = status.Bandwidth;
            this.CreationTime = status.CreationTime;
            this.Descripcion = status.Descripcion;
            this.From = status.From;
            this.HorizontalDilutionOfPrecision = status.HorizontalDilutionOfPrecision;
            this.InterfaceName = status.InterfaceName;
            this.InterfaceState = status.InterfaceState;
            this.InterfaceSubtype = status.InterfaceSubtype;
            this.InterfaceType = status.InterfaceType;
            this.IsCellularDataEnabled = status.IsCellularDataEnabled;
            this.IsCellularDataRoamingEnabled = status.IsCellularDataRoamingEnabled;
            this.IsMoving = status.IsMoving;
            this.IsNetworkAvailable = status.IsNetworkAvailable;
            this.IsPluggedIn = status.IsPluggedIn;
            this.IsWifiEnabled = status.IsWifiEnabled;
            this.Latitude = status.Latitude;
            this.Longitude = status.Longitude;
            this.ModificationTime = status.ModificationTime;
            this.NetWorkOperator = status.NetWorkOperator;
            this.PositionDilutionOfPrecision = status.PositionDilutionOfPrecision;
            this.PositionSource = status.PositionSource;
            this.Speed = status.Speed;
            this.VerticalDilutionOfPrecision = status.VerticalDilutionOfPrecision;
        }
        #endregion
    }
}
