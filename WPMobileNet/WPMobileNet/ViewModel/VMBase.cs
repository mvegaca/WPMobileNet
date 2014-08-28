using GalaSoft.MvvmLight.Command;
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
        #endregion
    }
}
