using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW12C.Custom {

    public class initSetting : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Save() {
            //Properties.Settings.Default.Save();
        }

        public initSetting() {
            APIP = "192.168.88.1";
            SSHUser = "root";
            SSHPassword = "vnpt";

            MACHeader = "A06518:A4F4C2:D49AA0";
            VnptMacCode = "A06518=2,A4F4C2=3,D49AA0=4";
            Manufacturer = "VNPTT";
            isWriteManufacturer = false;

            BuildTimeStamp = "Sat Oct 5 13:49:05 +07 2019";
            LogDirectory = "Default";

            Station = "ASM-UpGradeFirmware";
            HardwareVersion = "EW12HCv1.0";
            ModelNumber = "EW12C";
            ProductNumber = "";

            TimeoutTransFWFile = "360";
            TimeoutCheckMD5 = "20";
            TimeoutUpgradeFirmware = "360";
            TimeoutFindSSID = "30";
            WifiConnectionTimeOut = 20;

            FirmwareFile = "EW12_02RTM_RC02.tar.gz";

            Md5Sum = "AD530D9CB49951ACB0ECE905F47E3AC1";
            Md5Verify = "6d12d0631aa3046dbf10580ef9ee71f3";
            StartNewAPIP = "192.168.88.11";
            LocalIP = "192.168.88.100";
            StationID = "01";
            Operator = "";
        }

        bool _is_write_manufacturer;
        public bool isWriteManufacturer {
            get { return _is_write_manufacturer; }
            set {
                _is_write_manufacturer = value;
                OnPropertyChanged(nameof(isWriteManufacturer));
            }
        }
        string _apip;
        public string APIP {
            get { return _apip; }
            set {
                _apip = value;
                OnPropertyChanged(nameof(APIP));
            }
        }
        string _ssh_user;
        public string SSHUser {
            get { return _ssh_user; }
            set {
                _ssh_user = value;
                OnPropertyChanged(nameof(SSHUser));
            }
        }
        string _ssh_pass;
        public string SSHPassword {
            get { return _ssh_pass; }
            set {
                _ssh_pass = value;
                OnPropertyChanged(nameof(SSHPassword));
            }
        }
        string _mac_header;
        public string MACHeader {
            get { return _mac_header; }
            set {
                _mac_header = value;
                OnPropertyChanged(nameof(MACHeader));
            }
        }
        string _manufacturer;
        public string Manufacturer {
            get { return _manufacturer; }
            set {
                _manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
            }
        }
        string _vnpt_mac_code;
        public string VnptMacCode {
            get { return _vnpt_mac_code; }
            set {
                _vnpt_mac_code = value;
                OnPropertyChanged(nameof(VnptMacCode));
            }
        }
        string _build_time_stamp;
        public string BuildTimeStamp {
            get { return _build_time_stamp; }
            set {
                _build_time_stamp = value;
                OnPropertyChanged(nameof(BuildTimeStamp));
            }
        }
        string _log_directory;
        public string LogDirectory {
            get { return _log_directory; }
            set {
                _log_directory = value;
                OnPropertyChanged(nameof(LogDirectory));
            }
        }
        string _station;
        public string Station {
            get { return _station; }
            set {
                _station = value;
                OnPropertyChanged(nameof(Station));
            }
        }
        string _hardware_version;
        public string HardwareVersion {
            get { return _hardware_version; }
            set {
                _hardware_version = value;
                OnPropertyChanged(nameof(HardwareVersion));
            }
        }
        string _model_number;
        public string ModelNumber {
            get { return _model_number; }
            set {
                _model_number = value;
                OnPropertyChanged(nameof(ModelNumber));
            }
        }
        string _product_number;
        public string ProductNumber {
            get { return _product_number; }
            set {
                _product_number = value;
                OnPropertyChanged(nameof(ProductNumber));
            }
        }


        ////TIMEOUT ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~////
        string _tout_tran_fw_file;
        public string TimeoutTransFWFile {
            get { return _tout_tran_fw_file; }
            set {
                _tout_tran_fw_file = value;
                OnPropertyChanged(nameof(TimeoutTransFWFile));
            }
        }
        string _tout_check_md5;
        public string TimeoutCheckMD5 {
            get { return _tout_check_md5; }
            set {
                _tout_check_md5 = value;
                OnPropertyChanged(nameof(TimeoutCheckMD5));
            }
        }
        string _tout_up_fw;
        public string TimeoutUpgradeFirmware {
            get { return _tout_up_fw; }
            set {
                _tout_up_fw = value;
                OnPropertyChanged(nameof(TimeoutUpgradeFirmware));
            }
        }
        string _tout_find_ssid;
        public string TimeoutFindSSID {
            get { return _tout_find_ssid; }
            set {
                _tout_find_ssid = value;
                OnPropertyChanged(nameof(TimeoutFindSSID));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~////
        string _file_fw;
        public string FirmwareFile {
            get { return _file_fw; }
            set {
                _file_fw = value;
                OnPropertyChanged(nameof(FirmwareFile));
            }
        }
        string _md5_sum;
        public string Md5Sum {
            get { return _md5_sum; }
            set {
                _md5_sum = value;
                OnPropertyChanged(nameof(Md5Sum));
            }
        }
        string _start_new_apip;
        public string StartNewAPIP {
            get { return _start_new_apip; }
            set {
                _start_new_apip = value;
                OnPropertyChanged(nameof(StartNewAPIP));
            }
        }
        string _local_ip;
        public string LocalIP {
            get { return _local_ip; }
            set {
                _local_ip = value;
                OnPropertyChanged(nameof(LocalIP));
            }
        }
        string _station_id;
        public string StationID {
            get { return _station_id; }
            set {
                _station_id = value;
                OnPropertyChanged(nameof(StationID));
            }
        }
        string _operator;
        public string Operator {
            get { return _operator; }
            set {
                _operator = value;
                OnPropertyChanged(nameof(Operator));
            }
        }
        int _wifi_timeout;
        public int WifiConnectionTimeOut {
            get { return _wifi_timeout; }
            set {
                _wifi_timeout = value;
                OnPropertyChanged(nameof(WifiConnectionTimeOut));
            }
        }
        string _md5_verify;
        public string Md5Verify {
            get { return _md5_verify; }
            set {
                _md5_verify = value;
                OnPropertyChanged(nameof(Md5Verify));
            }
        }

    }
}
