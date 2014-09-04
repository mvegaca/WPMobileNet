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
    public partial class VGyrometer : PhoneApplicationPage
    {
        public VGyrometer()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            VMGyrometer vm = (VMGyrometer)DataContext;
            vm.Start();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            VMGyrometer vm = (VMGyrometer)DataContext;
            vm.Stop();
        }
    }
}