using EW12C.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW12C.Custom {

    public class testingInfomation : INotifyPropertyChanged {
        //implement interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public testingInfomation() {
            InitValue();
        }

        public void InitValue() {
            MAC_LABEL_ETHERNET = "--";
            MAC_LABEL_WIFI0 = "--";
            MAC_LABEL_WIFI1 = "--";
            APIP = "--";
            LOGDETAIL = "";
            LOGSYSTEM = "";
            LOGSSH = "";
            LOGSSH_FOR_SAVE = "";
            TotalResult = "--";
            ErrorMessage = "--";
            ErrorStep = "--";
            VerifyFirmwareValue = "";
            UpgradeFirmwareValue = "";
            MacEthernetValue = "";
            MacWifi2GValue = "";
            MacWifi5GValue = "";
            MacEthernetResult = "";
            MacWifi2GResult = "";
            MacWifi5GResult = "";
            VerifyFirmwareResult = "";

            scrollFlag = false;

            checkMacAndFwBasic = "--";
            tranFileAndCheckMd5Sum = "--";
            upgradeBusinessFw = "--";
        }

        #region Item result upgrade FW

        string _check_mac_and_fwbasic;
        public string checkMacAndFwBasic {
            get { return _check_mac_and_fwbasic; }
            set {
                _check_mac_and_fwbasic = value;
                OnPropertyChanged(nameof(checkMacAndFwBasic));
            }
        }
        string _tran_file_and_check_md5;
        public string tranFileAndCheckMd5Sum {
            get { return _tran_file_and_check_md5; }
            set {
                _tran_file_and_check_md5 = value;
                OnPropertyChanged(nameof(tranFileAndCheckMd5Sum));
            }
        }
        string _upgrade_business_fw;
        public string upgradeBusinessFw {
            get { return _upgrade_business_fw; }
            set {
                _upgrade_business_fw = value;
                OnPropertyChanged(nameof(upgradeBusinessFw));
            }
        }

        #endregion


        bool _scroll_flag;
        public bool scrollFlag {
            get { return _scroll_flag; }
            set {
                _scroll_flag = value;
                OnPropertyChanged(nameof(scrollFlag));
            }
        }

        public string NEWIP { get; set; }

        public string LocalIP {
            get { return GlobalData.initSetting.LocalIP; }
        }

        string _mac_label_ethernet;
        public string MAC_LABEL_ETHERNET {
            get { return _mac_label_ethernet; }
            set {
                _mac_label_ethernet = value;
                OnPropertyChanged(nameof(MAC_LABEL_ETHERNET));

                if (BaseFunction.isMacAddress(value)) {
                    MAC_LABEL_WIFI0 = BaseFunction.AddMacAddress(value, 1);
                    MAC_LABEL_WIFI1 = BaseFunction.AddMacAddress(value, 2);
                }
            }
        }

        string _mac_label_wifi0;
        public string MAC_LABEL_WIFI0 {
            get { return _mac_label_wifi0; }
            set {
                _mac_label_wifi0 = value;
                OnPropertyChanged(nameof(MAC_LABEL_WIFI0));
            }
        }

        string _mac_label_wifi1;
        public string MAC_LABEL_WIFI1 {
            get { return _mac_label_wifi1; }
            set {
                _mac_label_wifi1 = value;
                OnPropertyChanged(nameof(MAC_LABEL_WIFI1));
            }
        }

        string _ip;
        public string APIP {
            get { return _ip; }
            set {
                _ip = value;
                OnPropertyChanged(nameof(APIP));
            }
        }

        string _logdetail;
        public string LOGDETAIL {
            get { return _logdetail; }
            set {
                _logdetail = value;
                OnPropertyChanged(nameof(LOGDETAIL));
            }
        }

        string _logSystem;
        public string LOGSYSTEM {
            get { return _logSystem; }
            set {
                _logSystem = value;
                OnPropertyChanged(nameof(LOGSYSTEM));
                LOGDETAIL = value;
            }
        }

        string _logSSH;
        public string LOGSSH {
            get { return _logSSH; }
            set {
                _logSSH = value;
                OnPropertyChanged(nameof(LOGSSH));
            }
        }

        string _logssh_for_save;
        public string LOGSSH_FOR_SAVE {
            get { return _logssh_for_save; }
            set {
                _logssh_for_save = value;
                OnPropertyChanged(nameof(LOGSSH_FOR_SAVE));
            }
        }

        public string MacEthernetValue { get; set; }
        public string MacEthernetResult { get; set; }
        public string MacWifi2GValue { get; set; }
        public string MacWifi2GResult { get; set; }
        public string MacWifi5GValue { get; set; }
        public string MacWifi5GResult { get; set; }
        public string VerifyFirmwareValue { get; set; }
        public string VerifyFirmwareResult { get; set; }
        public string UpgradeFirmwareValue { get; set; }

        string _result;
        public string TotalResult {
            get { return _result; }
            set {
                _result = value;
                OnPropertyChanged(nameof(TotalResult));
            }
        }

        string _errormessage;
        public string ErrorMessage {
            get { return _errormessage; }
            set {
                _errormessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        string _errorstep;
        public string ErrorStep {
            get { return _errorstep; }
            set {
                _errorstep = value;
                OnPropertyChanged(nameof(ErrorStep));
            }
        }


    }
}
