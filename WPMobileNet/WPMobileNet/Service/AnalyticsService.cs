using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMobileNet.Service
{
    public class AnalyticsService
    {
        public enum EventCategory
        {
            Application,
            Actions
        };

        public enum EventAction
        {
            ApplicationLaunching,            
            ApplicationClosing,
            ApplicationUnhandledException,            
            SendMail,
            OpenWebBrowser,
            AcceptAccessLocation,
            DenyAccessLocation            
        };

        #region Constructos
        public AnalyticsService()
        {
        }
        #endregion

        #region Methods
        public static void SendView(string viewName)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendView(viewName);
        }
        public static void SendEvent(EventCategory category, EventAction action, string label = "", int value = 0)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent(category.ToString(), action.ToString(), label, value);
        }
        #endregion
    }
}
