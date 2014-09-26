using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Resources;

namespace WPMobileNet.Service
{
    public static class StringResourceService
    {
        public enum ResourceKeys
        {
            VHomeAppBarIcon1,
            VHomeAppBarIconUri1,
            VHomeAppBarIcon2,
            VHomeAppBarIconUri2,
            SensorNotFoundTitle,
            SensorNotFoundMessage
        };

        public static string GetResource(ResourceKeys resourceKey)
        {
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIcon1)) return AppResources.VHomeAppBarIcon1;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIconUri1)) return AppResources.VHomeAppBarIconUri1;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIcon2)) return AppResources.VHomeAppBarIcon2;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIconUri2)) return AppResources.VHomeAppBarIconUri2;
            if (resourceKey.Equals(ResourceKeys.SensorNotFoundTitle)) return AppResources.SensorNotFoundTitle;
            if (resourceKey.Equals(ResourceKeys.SensorNotFoundMessage)) return AppResources.SensorNotFoundMessage;
            return string.Empty;
        }
    }
}
