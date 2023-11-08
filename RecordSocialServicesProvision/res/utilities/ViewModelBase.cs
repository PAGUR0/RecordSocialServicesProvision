using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RecordSocialServicesProvision.res.utilities
{
    private string _valueToPass;
    public string ValueToPass
    {
        get { return _valueToPass; }
        set
        {
            if (_valueToPass != value)
            {
                _valueToPass = value;
                OnPropertyChanged(nameof(ValueToPass));
            }
        }
    }
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
