using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMobileNet.Utils
{
    public static class ConfigurationHelper
    {
        public static double MeZoomLevel = 15;
        public static double DefaultZoomLevel = 5;
        public static System.Device.Location.GeoCoordinate DefaultLocationCoordinate { get { return new System.Device.Location.GeoCoordinate(DefaultLocation[0], DefaultLocation[1]); } }
        public static double[] DefaultLocation = new double[2] { 40.4167, -003.7039 };
    }
}
