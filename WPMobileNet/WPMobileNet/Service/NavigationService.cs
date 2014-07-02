using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WPMobileNet.Service
{
    public class NavigationService : BaseService
    {
        #region Properties
        private readonly PhoneApplicationFrame frame;

        public Dictionary<string, object> RoutingTable { get; set; }

        public event NavigatedEventHandler Navigated;
        public event NavigatingCancelEventHandler Navigating;
        public event EventHandler<ObscuredEventArgs> Obscured;
        #endregion

        #region Constructors
        public NavigationService(PhoneApplicationFrame frame)
        {
            this.frame = frame;
            this.frame.Navigated += frame_Navigated;
            this.frame.Navigating += frame_Navigating;
            this.frame.Obscured += frame_Obscured;
        }
        #endregion

        #region Methods
        public bool Navigate(string route)
        {
            Uri uri = (Uri)RoutingTable[route];
            return this.frame.Navigate(uri);
        }
        public bool Navigate(string route, string parameter)
        {
            string originalString = ((Uri)RoutingTable[route]).OriginalString;
            Uri uri = new Uri(String.Concat(originalString, "?id=", parameter), UriKind.Relative);
            return this.frame.Navigate(uri);
        }

        //public bool RemovingHistoricalNavigate(string route)
        //{ 
        //    Uri uri = (Uri)RoutingTable[route];            
        //    return this.frame.Navigate(uri);
        //    //while (this.frame.BackStack.Count() > 0) { this.frame.RemoveBackEntry(); }
        //}

        public void GoBack() { this.frame.GoBack(); }
        public void GoForward() { this.frame.GoForward(); }
        public bool CanGoBack { get { return this.frame.CanGoBack; } }
        #endregion

        #region Events
        void frame_Navigated(object sender, NavigationEventArgs e)
        {
            var handler = this.Navigated;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var handler = this.Navigating;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        void frame_Obscured(object sender, ObscuredEventArgs e)
        {
            var handler = this.Obscured;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        #endregion
    }
}
