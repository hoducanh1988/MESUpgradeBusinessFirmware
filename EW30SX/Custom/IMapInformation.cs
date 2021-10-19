using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW30SX.Custom {
    public class IMapInformation : INotifyPropertyChanged {

        //implement interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IMapInformation() {
            accessapstring = "";
            ip = "";
            isconnected = false;
        }


        //Property
        string _ip;
        public string ip {
            get { return _ip; }
            set {
                _ip = value;
                OnPropertyChanged(nameof(ip));
            }
        }
        string _access_ap_string;
        public string accessapstring {
            get { return _access_ap_string; }
            set {
                _access_ap_string = value;
                OnPropertyChanged(nameof(accessapstring));
            }
        }
        bool _isconnected;
        public bool isconnected {
            get { return _isconnected; }
            set {
                _isconnected = value;
                OnPropertyChanged(nameof(isconnected));
            }
        }
    }
}
