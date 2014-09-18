using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WPMobileNet.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Device.Location;
using WPMobileNet.Utils;
using Microsoft.Phone.Maps;

namespace WPMobileNet.View
{
    public partial class VMap : PhoneApplicationPage
    {
        private MapLayer userLayer { get { return map.Layers[0]; } }
        public GeoCoordinate Me { get { return ((VMHome)DataContext).Me; } }
        public VMap()
        {
            InitializeComponent();
            MapsSettings.ApplicationContext.ApplicationId = "f4624420-8022-4f7e-b933-f3fa954467b6";
            MapsSettings.ApplicationContext.AuthenticationToken = "PmwEHUC4ESNMchaUJxuV6A";
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (map.Layers.Count == 0) map.Layers.Add(new MapLayer());
            if (this.Me != null && this.userLayer != null && this.userLayer.Count == 0)
            {
                UpdateUserPosition(this.Me);
                map.SetView(this.Me, ConfigurationHelper.MeZoomLevel);
            }
        }
        private void UpdateUserPosition(GeoCoordinate position)
        {
            this.UpdateUserPosition(position.Latitude, position.Longitude);
        }
        private void UpdateUserPosition(double latitude, double longitude)
        {
            try
            {
                if (userLayer == null) return;
                var userOverlay = GetUserOverlay(latitude, longitude);
                if (userOverlay != null)
                {
                    userLayer.Clear();
                    userLayer.Add(userOverlay);
                }
            }
            catch (System.Exception)
            {
            }
        }
        public MapOverlay GetUserOverlay(double latitude, double longitude)
        {
            try
            {
                Grid grid = new Grid();
                Ellipse circle = new Ellipse();
                //circle.Fill = new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);
                circle.Fill = new SolidColorBrush(Color.FromArgb(255, 51, 200, 255));
                circle.Height = 30;
                circle.Width = 30;
                circle.Opacity = 1;
                Ellipse outCircle = new Ellipse();
                //outCircle.Fill = new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);
                outCircle.Fill = new SolidColorBrush(Color.FromArgb(255, 2, 170, 231));
                outCircle.Height = 35;
                outCircle.Width = 35;
                outCircle.Opacity = 1;
                grid.Children.Add(outCircle);
                grid.Children.Add(circle);

                MapOverlay locationOverlay = new MapOverlay();
                locationOverlay.Content = grid;
                locationOverlay.PositionOrigin = new System.Windows.Point(0.5, 0.5);
                locationOverlay.GeoCoordinate = new GeoCoordinate(latitude, longitude);
                return locationOverlay;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private void Me_Click(object sender, EventArgs e)
        {
            if (this.Me != null)
            {
                map.SetView(this.Me, ConfigurationHelper.MeZoomLevel);
            }
        }        
    }
}