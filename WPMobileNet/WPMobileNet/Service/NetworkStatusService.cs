using Microsoft.Phone.Net.NetworkInformation;
using System.Net;
using System.Threading.Tasks;
using WPMobileNet.Exception;

namespace WPMobileNet.Service
{
    public class NetworkStatusService : BaseService
    {
        public enum MobileDataTechnology { Technology2G, Technology3G, Technology4G, TechnologyWifi, TechnologyUnknown }
        #region Properties
        private NetworkInterfaceInfo _networkInterfaceInfo;
        #endregion

        #region Constructors
        public NetworkStatusService()
        {
        }
        #endregion

        #region Methods
        private Task<NetworkInterfaceInfo> GetNetworkInterfaceInfo()
        {
            var tcs = new TaskCompletionSource<NetworkInterfaceInfo>();
            DnsEndPoint endPoint = new DnsEndPoint("microsoft.com", 80);
            NameResolutionCallback callBack = (result => tcs.SetResult(result.NetworkInterface));
            DeviceNetworkInformation.ResolveHostNameAsync(endPoint, callBack, null);
            return tcs.Task;
        }
        internal string GetBandwidth() { return ToString(_networkInterfaceInfo.Bandwidth); }
        internal string GetDescripcion() { return _networkInterfaceInfo.Description.ToString(); }
        internal string GetInterfaceName() { return _networkInterfaceInfo.InterfaceName.ToString(); }
        internal string GetInterfaceState() { return _networkInterfaceInfo.InterfaceState.ToString(); }
        internal string GetInterfaceType() { return _networkInterfaceInfo.InterfaceType.ToString(); }
        internal string GetInterfaceSubtype() { return _networkInterfaceInfo.InterfaceSubtype.ToString(); }
        internal string ToString(object value)
        {
            if (value == null) return string.Empty;
            else return value.ToString();
        }
        internal async Task UpdateNetworkInterfaceInfo()
        {
            this._networkInterfaceInfo = await GetNetworkInterfaceInfo();
            if (_networkInterfaceInfo == null) throw new NetworkInterfaceInfoException();
        }       
        internal MobileDataTechnology GetMobileDataTechnology()
        {
            switch (_networkInterfaceInfo.InterfaceSubtype)
            {
                case NetworkInterfaceSubType.Cellular_1XRTT: return MobileDataTechnology.Technology2G;                
                case NetworkInterfaceSubType.Cellular_EDGE: return MobileDataTechnology.Technology2G;
                case NetworkInterfaceSubType.Cellular_GPRS: return MobileDataTechnology.Technology2G;
                case NetworkInterfaceSubType.Cellular_3G: return MobileDataTechnology.Technology3G;                
                case NetworkInterfaceSubType.Cellular_EVDO: return MobileDataTechnology.Technology3G;
                case NetworkInterfaceSubType.Cellular_EVDV: return MobileDataTechnology.Technology3G;
                case NetworkInterfaceSubType.Cellular_HSPA: return MobileDataTechnology.Technology3G;
                case NetworkInterfaceSubType.Cellular_EHRPD: return MobileDataTechnology.Technology4G;
                case NetworkInterfaceSubType.Cellular_LTE: return MobileDataTechnology.Technology4G;
                case NetworkInterfaceSubType.WiFi: return MobileDataTechnology.TechnologyWifi;
                case NetworkInterfaceSubType.Desktop_PassThru: return MobileDataTechnology.TechnologyUnknown;
                case NetworkInterfaceSubType.Unknown: return MobileDataTechnology.TechnologyUnknown;
                default: return MobileDataTechnology.TechnologyUnknown;
            }
        }
        internal string GetNetWorkOperator() { return DeviceNetworkInformation.CellularMobileOperator; }
        internal bool GetIsWifiEnabled() { return DeviceNetworkInformation.IsWiFiEnabled; }
        internal bool GetIsCellularDataEnabled() { return DeviceNetworkInformation.IsCellularDataEnabled; }
        internal bool GetIsCellularDataRoamingEnabled() { return DeviceNetworkInformation.IsCellularDataRoamingEnabled; }
        internal bool GetIsNetworkAvailable() { return DeviceNetworkInformation.IsNetworkAvailable; }
        #endregion
    }
}
