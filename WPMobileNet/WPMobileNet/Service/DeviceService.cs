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
        internal bool IsPowerSavingModeEnabled() { return Windows.Phone.System.Power.PowerManager.PowerSavingMode.Equals(Windows.Phone.System.Power.PowerSavingMode.On); }
        internal string GetDeviceName() { return Microsoft.Phone.Info.DeviceStatus.DeviceName; }
        internal long DeviceTotalMemory() { return Microsoft.Phone.Info.DeviceStatus.DeviceTotalMemory; }
        internal string GetDeviceManufacturer() { return Microsoft.Phone.Info.DeviceStatus.DeviceManufacturer; }
        internal string GetDeviceHardwareVersion() { return Microsoft.Phone.Info.DeviceStatus.DeviceHardwareVersion; }
        internal string GetDeviceFirmwareVersion() { return Microsoft.Phone.Info.DeviceStatus.DeviceFirmwareVersion; }
        internal string GetDeviceID() { return Convert.ToBase64String((byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId")); }
        internal string GetPublisherHostId() { return Windows.Phone.System.Analytics.HostInformation.PublisherHostId; }
        //Desde aqui en adelante solamente está aqui.
        internal bool GetIsDeviceRetailModeEnabled() { return Windows.Phone.System.Profile.RetailMode.RetailModeEnabled; }
        internal string GetLockScreenImageUri() { return Windows.Phone.System.UserProfile.LockScreen.GetImageUri().ToString(); }
        internal bool GetIsScreenLocked() { return Windows.Phone.System.SystemProtection.ScreenLocked; }
        internal string GetGetAudioEndpoint() { return Windows.Phone.Media.Devices.AudioRoutingManager.GetDefault().GetAudioEndpoint().ToString(); }
        internal string GetSupportedAudioEncodingFormats()
        {
            string formats = string.Empty;
            foreach (var format in Windows.Phone.Media.Capture.AudioVideoCaptureDevice.SupportedAudioEncodingFormats) formats += format + ";";
            return formats;
        }
        internal string GetSupportedVideoEncodingFormats()
        {
            string formats = string.Empty;
            foreach (var format in Windows.Phone.Media.Capture.AudioVideoCaptureDevice.SupportedVideoEncodingFormats) formats += format + ";";
            return formats;
        }
        internal string GetAvailableSensorLocations()
        {
            string formats = string.Empty;
            foreach (var format in Windows.Phone.Media.Capture.AudioVideoCaptureDevice.AvailableSensorLocations) formats += format + ";";
            return formats;
        }
        internal string GetAvailableSensorLocations()
        {
            string formats = string.Empty;
            foreach (var format in Windows.Phone.Media.Capture.AudioVideoCaptureDevice.AvailableSensorLocations) formats += format + ";";
            return formats;
        }
        internal string GetInstallationPackages()
        {
            string formats = string.Empty;
            foreach (var format in Windows.Phone.Management.Deployment.InstallationManager.FindPackages()) formats += format.Id + ";";
            return formats;
        }
        internal float GetDeviceDisplayDpi() { return Windows.Graphics.Display.DisplayProperties.LogicalDpi; }
        internal string GetDeviceDisplayDpi() { return Windows.Graphics.Display.DisplayProperties.ResolutionScale.ToString(); }
        internal bool IsDeviceStereoEnabled() { return Windows.Graphics.Display.DisplayProperties.StereoEnabled; }
        internal string GetDisplayCurrentOrientation() { return Windows.Graphics.Display.DisplayProperties.CurrentOrientation.ToString(); }       

        //TODO Work with all Windows.Devices.Sensors
        #endregion
    }
}
