using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using WPMobileNet.Service;

namespace WPMobileNet.View
{
    public partial class VHome : PhoneApplicationPage
    {
        public VHome()
        {
            InitializeComponent();
        }
        private void Navigate_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            string parameter = sp.Tag as string;
            ServiceLocator.Current.GetInstance<NavigationService>().Navigate(parameter);
        }
    }
}