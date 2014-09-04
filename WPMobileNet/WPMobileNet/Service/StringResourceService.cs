using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Resources;

namespace WPMobileNet.Service
{
    public class StringResourceService : BaseService
    {
        public enum ResourceKeys
        {
            VHomeAppBarIcon1,
            VHomeAppBarIconUri1,
            VHomeAppBarIcon2,
            VHomeAppBarIconUri2
        };

        public string GetResource(ResourceKeys resourceKey)
        {
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIcon1)) return AppResources.VHomeAppBarIcon1;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIconUri1)) return AppResources.VHomeAppBarIconUri1;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIcon2)) return AppResources.VHomeAppBarIcon2;
            if (resourceKey.Equals(ResourceKeys.VHomeAppBarIconUri2)) return AppResources.VHomeAppBarIconUri2;
            return string.Empty;
        }
    }
}
