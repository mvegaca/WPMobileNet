using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMobileNet.Service
{
    public class DeviceService : BaseService
    {
        #region Methods
        internal int GetRemainingChargePercent() { return Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent; }
        internal bool IsPluggedIn() { return Microsoft.Phone.Info.DeviceStatus.PowerSource.Equals(Microsoft.Phone.Info.PowerSource.External); }
        internal string GetDeviceName() { return Microsoft.Phone.Info.DeviceStatus.DeviceName; }
        internal long DeviceTotalMemory() { return Microsoft.Phone.Info.DeviceStatus.DeviceTotalMemory; }
        internal string GetDeviceManufacturer() { return Microsoft.Phone.Info.DeviceStatus.DeviceManufacturer; }
        internal string GetDeviceHardwareVersion() { return Microsoft.Phone.Info.DeviceStatus.DeviceHardwareVersion; }
        internal string GetDeviceFirmwareVersion() { return Microsoft.Phone.Info.DeviceStatus.DeviceFirmwareVersion; }
        #endregion
    }
}
