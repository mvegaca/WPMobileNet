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

namespace WPMobileNet.View
{
    public partial class VAccelerometer : PhoneApplicationPage
    {
        public VAccelerometer()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VMAccelerometer vm = (VMAccelerometer)DataContext;
            vm.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            VMAccelerometer vm = (VMAccelerometer)DataContext;
            vm.Stop();
        }
    }
}