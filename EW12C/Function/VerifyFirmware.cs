using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UtilityPack.Validation;
using EW12C.Base;
using EW12C.Protocol;
using EW12C.Custom;

namespace EW12C.Function {
    public class VerifyFirmware : BaseFunction {

        private static int retry_max = 3;
        private static int connection_wifi_timeout = GlobalData.initSetting.WifiConnectionTimeOut * 1000; //ms

        public static bool Excute(testingInfomation testing) {
            lock (GlobalData.lockwifi) {
                SSH<testingInfomation> ssh = null;
                bool r = false;

                try {
                    //Connect wifi to AP MESH - retry 3 times
                    r = connect_wifi_to_ap(testing);
                    if (!r) goto NG;

                    //login SSH to AP MESH - retry 3 times
                    r = login_ssh_to_ap(testing, ref ssh);
                    if (!r) goto NG;

                    //ghi hay check mã manufacterer
                    if (GlobalData.initSetting.isWriteManufacturer) {
                        r = check_manufacturer(testing, ssh);
                        if (!r) goto NG;
                    }

                    //check mac ethernet
                    r = check_mac_ethernet(testing, ssh);
                    if (!r) goto NG;

                    //check mac wifi0-2.4GHz = mac ethernet + 1
                    r = check_mac_wlan_2g(testing, ssh);
                    if (!r) goto NG;

                    //check mac wifi1-5GHz = mac ethernet + 2
                    r = check_mac_wlan_5g(testing, ssh);
                    if (!r) goto NG;

                    //check hardware version
                    r = check_hardware_version(testing, ssh);
                    if (!r) goto NG;

                    //check model number
                    r = check_model_number(testing, ssh);
                    if (!r) goto NG;

                    //check serial number
                    r = check_factory_serial_number(testing, ssh);
                    if (!r) goto NG;

                    //check firmware build time stamp
                    r = check_firmware_build_time(testing, ssh);
                    if (!r) goto NG;

                    goto OK;

                }
                catch (Exception ex) {
                    testing.ErrorMessage = ex.Message;
                    testing.LOGSYSTEM += ex.Message + "\r\n";
                    goto NG;
                }

            OK:
                {
                    ssh = null;
                    return true;
                }

            NG:
                {
                    ssh = null;
                    return false;
                }
            }
        }

        static bool connect_wifi_to_ap(testingInfomation testing) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Connecting to AP MESH...\r\n");
            r = BaseFunction.Connect_Wifi(testing, connection_wifi_timeout, retry_max + 2);
            testing.LOGSYSTEM += string.Format("...{0}\r\n", testing.ErrorMessage);
            testing.LOGSYSTEM += string.Format("...Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            return r;
        }

        static bool login_ssh_to_ap(testingInfomation testing, ref SSH<testingInfomation> ssh) {
            bool r = false;
            int c = 0;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Login to AP MESH {0},{1},{2} ...\r\n", GlobalData.initSetting.APIP, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
            Thread.Sleep(3000);

            c = 0;
        RE_LOGIN:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            ssh = new SSH<testingInfomation>(testing);
            r = ssh.Login(GlobalData.initSetting.APIP, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
            testing.LOGSYSTEM += string.Format("...[{0}], Result = {1}\r\n", c, r == true ? "PASS" : "FAIL");
            if (c < retry_max + 2 && r == false) goto RE_LOGIN;

            if (!r) {
                testing.ErrorMessage = string.Format("Can't login SSH to AP.");
            }

            return r;
        }

        static bool check_mac_ethernet(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare mac ethernet with label on AP...\r\n");

            int c = 0;
            string data = "";
        RE_CHECKETHERNET:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get mac address of ethernet
            testing.LOGSYSTEM += string.Format("...[{0}], Get mac ethernet in AP firmware :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /sys/class/net/eth0/address");
            Thread.Sleep(100);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.MacEthernetValue = data;
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);

            //-------get mac address in label on AP
            testing.LOGSYSTEM += string.Format("...[{0}], Get mac ethernet in label on AP :\r\n", c);
            testing.LOGSYSTEM += string.Format("......{0}\r\n", testing.MAC_LABEL_ETHERNET);

            //-------compare ethernet mac
            testing.LOGSYSTEM += string.Format("...[0], Compare mac ethernet :\r\n", c);
            r = false;
            try {
                r = data.ToLower().Replace(":", "").Contains(testing.MAC_LABEL_ETHERNET.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKETHERNET;
            testing.MacEthernetResult = r == true ? "PASS" : "FAIL";

            if (!r) {
                testing.ErrorMessage = string.Format("Mac ethernet in AP firmware with AP label are not same.");
            }

            return r;
        }

        static bool check_mac_wlan_2g(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare mac wifi0-2.4GHz with label on AP...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKWIFI0:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get mac address of wifi0
            testing.LOGSYSTEM += string.Format("...[{0}], Get mac wifi0-2.4GHz in AP firmware :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /sys/class/net/wifi0/address");
            Thread.Sleep(100);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.MacWifi2GValue = data;
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);

            //-------get mac address in label on AP
            testing.LOGSYSTEM += string.Format("...[{0}], Gen mac wifi0-2.4GHz from label on AP :\r\n", c);
            //testing.MAC_LABEL_WIFI0 = BaseFunction.AddMacAddress(testing.MAC_LABEL_ETHERNET, 1);
            testing.LOGSYSTEM += string.Format("......{0}\r\n", testing.MAC_LABEL_WIFI0);

            //-------compare mac wifi0
            testing.LOGSYSTEM += string.Format("...[{0}], Compare mac wifi0 :\r\n", c);
            r = false;
            try {
                r = data.ToLower().Replace(":", "").Contains(testing.MAC_LABEL_WIFI0.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKWIFI0;
            testing.MacWifi2GResult = r == true ? "PASS" : "FAIL";

            if (!r) {
                testing.ErrorMessage = string.Format("Mac Wifi0-2.4GHz in AP firmware with AP label are not same.");
            }

            return r;
        }

        static bool check_mac_wlan_5g(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare mac wifi1-5GHz with label on AP...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKWIFI1:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get mac address of wifi1
            testing.LOGSYSTEM += string.Format("...[{0}], Get mac wifi1-5GHz in AP firmware :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /sys/class/net/wifi1/address");
            Thread.Sleep(100);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.MacWifi5GValue = data;
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);

            //-------get mac address in label on AP
            testing.LOGSYSTEM += string.Format("...[{0}], Gen mac Wifi1-5GHz from label on AP :\r\n", c);
            //testing.MAC_LABEL_WIFI1 = BaseFunction.AddMacAddress(testing.MAC_LABEL_ETHERNET, 2);
            testing.LOGSYSTEM += string.Format("......{0}\r\n", testing.MAC_LABEL_WIFI1);

            //-------compare mac wifi1
            testing.LOGSYSTEM += string.Format("...[{0}], Compare mac wifi1 :\r\n", c);
            r = false;
            try {
                r = data.ToLower().Replace(":", "").Contains(testing.MAC_LABEL_WIFI1.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKWIFI1;
            testing.MacWifi5GResult = r == true ? "PASS" : "FAIL";

            if (!r) {
                testing.ErrorMessage = string.Format("Mac wifi1-5GHz in AP firmware with AP label are not same.");
            }

            return r;
        }

        static bool check_hardware_version(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare hardware version with setting value...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKHW:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get hardware version
            testing.LOGSYSTEM += string.Format("...[{0}], Hardware version in AP:", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("fw_printenv");
            Thread.Sleep(500);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";

            string hw_ver = "";
            if (data == "null") hw_ver = data;
            else {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.ToLower().Contains("hardwareversion=")) {
                        hw_ver = b;
                        break;
                    }
                }
                hw_ver = hw_ver.Split(new string[] { "hardwareversion=" }, StringSplitOptions.None)[1].Replace("\r", "").Replace("\n", "").Trim();
            }

            testing.LOGSYSTEM += string.Format(" {0}\r\n", hw_ver);
            testing.LOGSYSTEM += string.Format("...[{0}], Hardware version in setting :", c);
            testing.LOGSYSTEM += string.Format(" {0}\r\n", GlobalData.initSetting.HardwareVersion);

            //-------compare with setting
            testing.LOGSYSTEM += string.Format("...[{0}], Compare hardware version :\r\n", c);
            r = false;
            try {
                r = hw_ver.ToLower().Equals(GlobalData.initSetting.HardwareVersion.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKHW;

            if (!r) {
                testing.ErrorMessage = string.Format("Hardware version not same setting.");
            }

            return r;
        }

        static bool check_model_number(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare model number with setting value...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKMODEL:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get model number
            testing.LOGSYSTEM += string.Format("...[{0}], Model number in AP:", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("fw_printenv");
            Thread.Sleep(500);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";

            string model_number = "";
            if (data == "null") model_number = data;
            else {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.ToLower().Contains("modelnumber=")) {
                        model_number = b;
                        break;
                    }
                }
                model_number = model_number.Split(new string[] { "modelnumber=" }, StringSplitOptions.None)[1].Replace("\r", "").Replace("\n", "").Trim();
            }

            testing.LOGSYSTEM += string.Format(" {0}\r\n", model_number);
            testing.LOGSYSTEM += string.Format("...[{0}], Model number in setting :", c);
            testing.LOGSYSTEM += string.Format(" {0}\r\n", GlobalData.initSetting.ModelNumber);

            //-------compare with setting
            testing.LOGSYSTEM += string.Format("...[{0}], Compare model number :\r\n", c);
            r = false;
            try {
                r = model_number.ToLower().Equals(GlobalData.initSetting.ModelNumber.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKMODEL;

            if (!r) {
                testing.ErrorMessage = string.Format("Model number not same setting.");
            }

            return r;
        }

        static bool check_factory_serial_number(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare serial number with mac ethernet...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKSERIAL:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get serial number
            testing.LOGSYSTEM += string.Format("...[{0}], Get serial number in AP :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("fw_printenv serialnumber | cut -d= -f2");
            Thread.Sleep(500);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);
            string[] buffer = data.Split('\n');
            string serial = buffer[2].Replace("\r", "").Replace("\n", "").ToUpper().Trim();
            testing.LOGSYSTEM += string.Format("......serial : {0}\r\n", serial);
            testing.LOGSYSTEM += string.Format("...[{0}], Compare serial number :\r\n", c);

            //-------compare mac value
            r = false;
            try {
                r = testing.MAC_LABEL_ETHERNET.ToLower().Substring(6, 6).Equals(serial.ToLower().Substring(9, 6));
            }
            catch { }
            if (c < retry_max && r == false) goto RE_CHECKSERIAL;

            //-------compare mac header
            r = false;
            try {
                r = Parse.IsMatchingMacCode(serial, testing.MAC_LABEL_ETHERNET, GlobalData.initSetting.VnptMacCode);
            }
            catch { }
            if (c < retry_max && r == false) goto RE_CHECKSERIAL;

            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (!r) {
                testing.ErrorMessage = string.Format("Serial number not same mac.");
            }

            return r;
        }

        static bool check_firmware_build_time(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare firmware build time stamp with setting value...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKFW:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get firmware build time stamp
            testing.LOGSYSTEM += string.Format("...[{0}], fw buildtime in AP:", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("cat /proc/version");
            Thread.Sleep(500);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";


            string fw_buildtime = "";
            if (data == "null") fw_buildtime = data;
            else {
                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.ToLower().Contains("gcc version")) {
                        fw_buildtime = b;
                        break;
                    }
                }
                fw_buildtime = fw_buildtime.Split(new string[] { ") ) " }, StringSplitOptions.None)[1].Replace("\r", "").Replace("\n", "").Trim();
            }

            testing.LOGSYSTEM += string.Format(" {0}\r\n", fw_buildtime);
            testing.LOGSYSTEM += string.Format("...[{0}], fw buildtime in setting :", c);
            testing.LOGSYSTEM += string.Format(" {0}\r\n", GlobalData.initSetting.BuildTimeStamp);

            //------compare build time stamp with setting value
            testing.LOGSYSTEM += string.Format("...[{0}], Compare build time stamp :\r\n", c);
            r = false;
            try {
                r = fw_buildtime.ToLower().Equals(GlobalData.initSetting.BuildTimeStamp.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKFW;
            //testing.VerifyFirmwareResult = r == true ? "PASS" : "FAIL";

            if (!r) {
                testing.ErrorMessage = string.Format("Firmware build time stamp with standard are not same.");
            }

            return r;
        }

        static bool check_manufacturer(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Compare manufacturer with setting value...\r\n");
            int c = 0;
            string data = "";

        RE_CHECKMENU:
            c++;
            testing.LOGSYSTEM += "\r\n\r\n";
            //-------get menufacturer
            testing.LOGSYSTEM += string.Format("...[{0}], Get manufacturer in AP :\r\n", c);
            ssh.Write("\n");
            Thread.Sleep(300);
            ssh.WriteLine("fw_printenv manufacturer");
            Thread.Sleep(500);
            data = ssh.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";

            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);
            if (data.ToLower().Contains("manufacturer=") == false) {
                if (c < retry_max && r == false) goto RE_CHECKMENU;
            }

            string[] bff = data.Split('\n');
            foreach (var b in bff) {
                if (b.ToLower().Contains("manufacturer=")) {
                    data = b;
                }
            }

            data = data.Split('=')[1].Replace("\r", "").Trim();

            //-------get menufacturer
            testing.LOGSYSTEM += string.Format("...[{0}], Get manufacturer value in setting :\r\n", c);
            testing.LOGSYSTEM += string.Format("......{0}\r\n", GlobalData.initSetting.Manufacturer);

            //------compare menufacturer value with setting value
            testing.LOGSYSTEM += string.Format("...[{0}], Compare manufacturer :\r\n", c);
            r = false;
            try {
                r = data.ToLower().Equals(GlobalData.initSetting.Manufacturer.ToLower());
            }
            catch { }
            testing.LOGSYSTEM += string.Format("......Result = {0}\r\n", r == true ? "PASS" : "FAIL");
            if (c < retry_max && r == false) goto RE_CHECKMENU;

            if (!r) {
                testing.ErrorMessage = string.Format("Manufacturer in AP not same in software setting.");
            }

            return r;
        }

        static bool write_manufacturer(testingInfomation testing, SSH<testingInfomation> ssh) {
            bool r = false;
            testing.LOGSSH = "";
            string manufact = GlobalData.initSetting.Manufacturer;

            //Transfer firmware from PC to IMAP ~~~~~~~//
            int max_count = 10;

            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("write manufacturer {0} to IMAP...\r\n", manufact);
            string temp = "";
            int count = 0;

        RE:
            count++;
            string cmd = $"fw_setenv manufacturer {manufact}";
            testing.LOGSYSTEM += string.Format("Gửi lệnh: {0} \r\n", cmd);
            ssh.WriteLine(cmd);
            Thread.Sleep(500);
            temp = ssh.Read();
            testing.LOGSYSTEM += temp == null || temp == "" ? "..." : temp;
            r = temp.Contains("root@VNPT:~#") || temp.Contains("root@VNPT:(unreachable)/root#") || temp.EndsWith("[x]");
            if (!r) {
                if (count < max_count) {
                    goto RE;
                }
            }
            bool kq = temp.EndsWith("[x]");
            kq = !kq;
            r = r && kq;

            if (!r) {
                testing.ErrorMessage = string.Format("Can't write manufacturer to IMAP.");
            }

            return r;
        }

        string getHardwareVersion(SSH<testingInfomation> ssh) {

            //get data
            string data = "";
            bool r = ssh.Query("fw_printenv", "hardwareversion=", 1000, 3, out data);
            if (!r) return null;

            //split hardware version
            string hardware_version = "";
            string[] buffer = data.Split('\n');
            int max = buffer.Length - 1;
            for (int i = max; i >= 0; i--) {
                string s = buffer[i];
                if (s.Contains("hardwareversion=")) {
                    hardware_version = s.Replace("hardwareversion=", "")
                                        .Replace("\r", "")
                                        .Replace("\n", "")
                                        .Replace(":", "")
                                        .Replace(",", "")
                                        .Replace(";", "")
                                        .Trim();
                    break;
                }
            }

            //return value
            return hardware_version == "" ? null : hardware_version;
        }



    }
}
