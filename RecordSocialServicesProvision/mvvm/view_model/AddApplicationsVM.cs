using RecordSocialServicesProvision.res.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordSocialServicesProvision.mvvm.view_model
{
    internal class AddApplicationsVM : utilities.ViewModelBase
    {
        public string Login
        {
            get { return Login; }
            set
            {
                if (Login != value)
                {
                    Login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
    }
}
