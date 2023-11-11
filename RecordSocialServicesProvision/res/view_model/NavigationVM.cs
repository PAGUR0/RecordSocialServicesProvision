using RecordSocialServicesProvision.res.utilities;
using System.Windows.Input;


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
        public ICommand AllApplicationsCommand { get; set; }

        private void AddApplications(object obj) => CurrentView = new AddApplicationsVM();
        private void WaitingList(object obj) => CurrentView = new WaitingListVM();
        private void AllApplications(object obj) => CurrentView = new AllApplicationsVM();

        public NavigationVM()
        {
            AddApplicationsCommand = new RelayCommand(AddApplications);
            WaitingListCommand = new RelayCommand(WaitingList);
            AllApplicationsCommand = new RelayCommand(AllApplications);
            CurrentView = new WaitingListVM();
        }
    }
}
