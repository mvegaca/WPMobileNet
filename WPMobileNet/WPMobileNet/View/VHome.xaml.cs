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
using WPMobileNet.ViewModel;

namespace WPMobileNet.View
{
    public partial class VHome : PhoneApplicationPage
    {
        public VHome()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (ApplicationBar == null)
            {
                ApplicationBar = new ApplicationBar();
                ApplicationBar.Mode = ApplicationBarMode.Minimized;
                ApplicationBarIconButton b1 = new ApplicationBarIconButton();
                b1.Text = StringResourceService.GetResource(WPMobileNet.Service.StringResourceService.ResourceKeys.VHomeAppBarIcon1);
                b1.IconUri = new Uri(StringResourceService.GetResource(WPMobileNet.Service.StringResourceService.ResourceKeys.VHomeAppBarIconUri1), UriKind.Relative);
                b1.Click += (sender, args) => { ((VMHome)DataContext).NavigateCommand.Execute("VInformation"); };
                ApplicationBar.Buttons.Add(b1);
                ApplicationBarIconButton b2 = new ApplicationBarIconButton();
                b2.Text = StringResourceService.GetResource(WPMobileNet.Service.StringResourceService.ResourceKeys.VHomeAppBarIcon2);
                b2.IconUri = new Uri(StringResourceService.GetResource(WPMobileNet.Service.StringResourceService.ResourceKeys.VHomeAppBarIconUri2), UriKind.Relative);
                b2.Click += (sender, args) => { ((VMHome)DataContext).NavigateCommand.Execute("VSettings"); };
                ApplicationBar.Buttons.Add(b2);
            }
        }
        private void Navigate_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            string parameter = sp.Tag as string;
            ServiceLocator.Current.GetInstance<NavigationService>().Navigate(parameter);
        }
    }
}