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
            SimpleIoc.Default.Register<NavigationService>(() => new NavigationService(App.RootFrame)
            {
                RoutingTable = new Dictionary<string, object>()
                {
                    {"VHome", new Uri("/View/VHome.xaml", UriKind.Relative)}
                }
            });
            //ViewModels
            SimpleIoc.Default.Register<VMHome>();
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
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}