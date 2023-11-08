using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordSocialServicesProvision.res.utilities;
using System.Windows.Input;
using RecordSocialServicesProvision.res.view;
using RecordSocialServicesProvision.res.view_model;

namespace RecordSocialServicesProvision.mvvm.view_model
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand AddApplicationsCommand { get; set; }
        public ICommand WaitingListCommand { get; set; }

        private void AddApplications(object obj)
        {
            var addApplicationsVM = new AddApplicationsVM();
            addApplicationsVM.Login = "1";
            CurrentView = addApplicationsVM;
        }
        private void WaitingList(object obj) => CurrentView = new WaitingListVM();


        public NavigationVM()
        {
            AddApplicationsCommand = new RelayCommand(AddApplications);
            WaitingListCommand = new RelayCommand(WaitingList);

            // Startup Page
            CurrentView = new WaitingListVM();
        }
    }
}
