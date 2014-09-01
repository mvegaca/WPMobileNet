using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMobileNet.Service;

namespace WPMobileNet.ViewModel
{
    public class VMBase : SimpleViewModelBase
    {
        #region Services
        public NavigationService _navigationService;
        #endregion

        #region Properties
        private VMStatus _status;
        public VMStatus Status
        {
            get { return _status; }
            set { Set("Status", ref _status, value); }
        }
        #endregion

        #region Constructors
        public VMBase() { }
        public VMBase(NavigationService navigationService)
        {
            this._navigationService = navigationService;
            this.Status = new VMStatus();
        }
        #endregion

        #region Commands
        #region NavigateCommand
        private RelayCommand<string> _navigateCommand;
        public RelayCommand<string> NavigateCommand
        {
            get
            {
                return _navigateCommand ?? (_navigateCommand = new RelayCommand<string>(ExecuteNavigateCommand));
            }
        }
        private void ExecuteNavigateCommand(string parameter)
        {
            _navigationService.Navigate(parameter);
        }
        #endregion
        #region OpenWebBrowserCommand
        private RelayCommand<string> _openWebBrowserCommand;
        public RelayCommand<string> OpenWebBrowserCommand
        {
            get
            {
                return _openWebBrowserCommand ?? (_openWebBrowserCommand = new RelayCommand<string>(ExecuteOpenWebBrowserCommand));
            }
        }
        private void ExecuteOpenWebBrowserCommand(string parameter)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri(parameter, UriKind.Absolute);
            task.Show();
        }
        #endregion
        #region OpenMailCommand
        private RelayCommand<string> _openMailCommand;
        public RelayCommand<string> OpenMailCommand
        {
            get
            {
                return _openMailCommand ?? (_openMailCommand = new RelayCommand<string>(ExecuteOpenMailCommand));
            }
        }
        private void ExecuteOpenMailCommand(string parameter)
        {
            EmailComposeTask task = new EmailComposeTask();
            task.To = parameter;
            task.Show();
        }
        #endregion
        #endregion
    }
}
