using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW12SG.Custom {

    public class upgradeInformation : INotifyPropertyChanged {
        //implement interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public upgradeInformation() {
            InitValue();
        }

        public void InitValue() {
            LOGDETAIL = "";
            NetSpotStatus = "--";
            DHCPStatus = "--";
            ClientCount = "--";
        }

        string _logdetail;
        public string LOGDETAIL {
            get { return _logdetail; }
            set {
                if (value.Length > 5000) value = "";
                _logdetail = value;
                OnPropertyChanged(nameof(LOGDETAIL));
            }
        }
        string _netspot_status;
        public string NetSpotStatus {
            get { return _netspot_status; }
            set {
                _netspot_status = value;
                OnPropertyChanged(nameof(NetSpotStatus));
            }
        }
        string _dhcp_status;
        public string DHCPStatus {
            get { return _dhcp_status; }
            set {
                _dhcp_status = value;
                OnPropertyChanged(nameof(DHCPStatus));
            }
        }
        string _client_count;
        public string ClientCount {
            get { return _client_count; }
            set {
                _client_count = value;
                OnPropertyChanged(nameof(ClientCount));
            }
        }

    }
}
