/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WPMobileNet.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using WPMobileNet.Model;
using WPMobileNet.Service;

namespace WPMobileNet.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                //SimpleIoc.Default.Register<IDataService, DataService>();                
            }
            //Services
            SimpleIoc.Default.Register<PingService>();
            SimpleIoc.Default.Register<NetworkStatusService>();
            SimpleIoc.Default.Register<DeviceService>();
            SimpleIoc.Default.Register<MessageBoxService>();
            SimpleIoc.Default.Register<StateService>();
            SimpleIoc.Default.Register<LocationService>();
            SimpleIoc.Default.Register<AccelerometerService>();
            SimpleIoc.Default.Register<GyrometerService>();
            SimpleIoc.Default.Register<InclinometerService>();
            SimpleIoc.Default.Register<StringResourceService>();
            SimpleIoc.Default.Register<WeatherService>();
            SimpleIoc.Default.Register<NavigationService>(() => new NavigationService(App.RootFrame)
            {
                RoutingTable = new Dictionary<string, object>()
                {
                    {"VHome", new Uri("/View/VHome.xaml", UriKind.Relative)},
                    {"VAccelerometer", new Uri("/View/VAccelerometer.xaml", UriKind.Relative)},
                    {"VPedometer", new Uri("/View/VPedometer.xaml", UriKind.Relative)},
                    {"VInformation", new Uri("/View/VInformation.xaml", UriKind.Relative)},                    
                    {"VScreen", new Uri("/View/VScreen.xaml", UriKind.Relative)},
                    {"VGyrometer", new Uri("/View/VGyrometer.xaml", UriKind.Relative)},
                    {"VSettings", new Uri("/View/VSettings.xaml", UriKind.Relative)},
                    {"VInclinometer", new Uri("/View/VInclinometer.xaml", UriKind.Relative)},
                    {"VWeather", new Uri("/View/VWeather.xaml", UriKind.Relative)},
                    {"VMap", new Uri("/View/VMap.xaml", UriKind.Relative)}
                }
            });
            //ViewModels
            SimpleIoc.Default.Register<VMHome>();
            SimpleIoc.Default.Register<VMAccelerometer>();
            SimpleIoc.Default.Register<VMGyrometer>();
            SimpleIoc.Default.Register<VMInclinometer>();
            SimpleIoc.Default.Register<VMWeather>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public VMHome Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VMHome>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public VMAccelerometer Accelerometer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VMAccelerometer>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public VMGyrometer Gyrometer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VMGyrometer>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public VMInclinometer Inclinometer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VMInclinometer>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public VMWeather Weather
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VMWeather>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}