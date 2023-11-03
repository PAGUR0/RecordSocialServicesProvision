using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordSocialServicesProvision.res.utilities;
using System.Windows.Input;
using RecordSocialServicesProvision.res.view;

namespace RecordSocialServicesProvision.res.view_model
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

        private void AddApplications(object obj) => CurrentView = new AddApplicationsVM();
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
