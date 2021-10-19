using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Windows;
using System.Diagnostics;
using EW12SG.Base;
using EW12SG.Function;
using EW12SG.Custom;
using Renci.SshNet;
using Tamir.SharpSsh;
using EW12SG.Protocol;
using EW12SG.IO;
using SimpleWifi;
using System.Text.RegularExpressions;


namespace EW12SG.Function {
    public class UpgradeFirmware : BaseFunction {

        private static int retry_max = 3;
        private static int connection_wifi_timeout = GlobalData.initSetting.WifiConnectionTimeOut * 1000; //ms


        public bool Excute(testingInfomation testing) {
            var sSH = new SSH<testingInfomation>(testing);
            bool r = false;
            Stopwatch st = new Stopwatch();
            try {
                testing.LOGSYSTEM = "";

                //login SSH to AP qua LAN - retry 5 times
                st.Start();
                r = login_to_mesh(sSH, testing);

                st.Stop();
                testing.LOGSYSTEM += string.Format("... [{0}] login to mesh {1}. \r\n", r ? "PASS" : "FAIL", testing.NEWIP);
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) goto NG;


                //check mac && fw basic build time
                st.Reset(); st.Restart();
                testing.checkMacAndFwBasic = "Waiting...";
                r = check_mac_ethernet(sSH, testing) && check_fw_basic(sSH, testing);
                testing.checkMacAndFwBasic = r ? "Passed" : "Failed";

                st.Stop();
                testing.LOGSYSTEM += string.Format("... [{0}] check mac and firmware basic build time. \r\n", r ? "PASS" : "FAIL");
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) goto NG;

                //write menufacturer
                if (GlobalData.initSetting.isWriteManufacturer) {
                    st.Reset(); st.Restart();
                    r = write_manufacturer(sSH, testing);
                    st.Stop();
                    testing.LOGSYSTEM += string.Format("... [{0}] write menufacturer to AP. \r\n", r ? "PASS" : "FAIL");
                    testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                    if (!r) goto NG;
                }
                
                //Upgrade firmware -----------------------------------------//
                st.Reset(); st.Restart();
                r = upgradeFirmware(sSH, testing);

                st.Stop();
                testing.LOGSYSTEM += string.Format("... [{0}] upgrade firmware business. \r\n", r ? "PASS" : "FAIL");
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) goto NG;


                goto OK;
            }
            catch (Exception ex) {
                testing.LOGSYSTEM += ex.Message + "\r\n";
                goto NG;
            }

        OK:
            {
                sSH = null;
                return true;
            }
        NG:
            {
                sSH = null;
                return false;
            }
        }

        #region other

        bool login_to_mesh(SSH<testingInfomation> sSH, testingInfomation testing) {
            bool ret = false;
            int count = 0;
            testing.LOGSYSTEM += "> Chờ Login SSH qua LAN... \r\n";

        RE:
            count++;
            ret = sSH.Login(testing.NEWIP, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
            if (count < retry_max + 2 && ret == false) goto RE;

            return ret;
        }

        bool check_mac_ethernet(SSH<testingInfomation> sSH, testingInfomation testing) {
        ReGET_MAC:
            testing.LOGSYSTEM += string.Format("> Đọc địa chỉ MAC Ethernet của sản phẩm\r\n");
            sSH.WriteLine("cat /sys/class/net/eth0/address");
            Thread.Sleep(300);
            string data = sSH.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);
            if (data == "null") goto ReGET_MAC;
            data = data.ToLower();
            data = data.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[1];
            data = data.Replace("cat /sys/class/net/eth0/address", "").Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
            if (data.ToLower().Replace(":", "").ToLower().Contains(testing.MAC_LABEL_ETHERNET.ToLower()) == false) {
                testing.LOGSYSTEM += "[FAIL] địa chỉ MAC không khớp FAIL. \r\n";
                return false;
            }

            return true;
        }

        bool check_fw_basic(SSH<testingInfomation> sSH, testingInfomation testing) {
        ReFW_VER:
            testing.LOGSYSTEM += string.Format("> Đọc phiên bản firmware cũ của sản phẩm\r\n");
            sSH.WriteLine("cat /proc/version");
            Thread.Sleep(500);
            string data = sSH.Read();
            if (data == null || data == "" || data == string.Empty) data = "null";
            testing.LOGSYSTEM += string.Format("......{0}\r\n", data);
            if (data == "null") goto ReFW_VER;
            return true;
        }

        #endregion

        #region Upgrade firmware


        bool upgradeFirmware(SSH<testingInfomation> sSH, testingInfomation testing) {
            try {
                Stopwatch st = new Stopwatch();
                bool r = false;
                testing.upgradeBusinessFw = "--";

                st.Start();
                testing.tranFileAndCheckMd5Sum = "Waiting...";
                r = tran_fw_from_pc_to_imap(sSH, testing);
                testing.tranFileAndCheckMd5Sum = r ? "Waiting..." : "Failed";

                st.Stop();
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) return false;


                st.Reset(); st.Restart();
                r = check_md5_sum_fw(sSH, testing);
                testing.tranFileAndCheckMd5Sum = r ? "Passed" : "Failed";

                st.Stop();
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) return false;

                st.Reset(); st.Restart();
                testing.upgradeBusinessFw = "Waiting...";
                r = upgrade_fw(sSH, testing);
                testing.upgradeBusinessFw = r ? "Passed" : "Failed";

                st.Stop();
                testing.LOGSYSTEM += string.Format("... Time elapsed: {0} ms\n\n", st.ElapsedMilliseconds);
                if (!r) return false;

                return true;

            }
            catch (Exception ex) {
                testing.LOGSYSTEM += ex.Message + "\r\n";
                testing.LOGSYSTEM += string.Format("\r\n KẾT QUẢ UPGRADE FIRMWARE: FAIL\r\n");
                return false;
            }
        }

        bool write_manufacturer(SSH<testingInfomation> sSH, testingInfomation testing) {
            bool ret = false;
            testing.LOGSSH = "";
            string manufact = GlobalData.initSetting.Manufacturer;
            
            //Transfer firmware from PC to IMAP ~~~~~~~//
            int max_count = 10;
            
            testing.LOGSYSTEM += string.Format("\r\n Write manufacturer {0} to IMAP...\r\n", manufact);
            string temp = "";
            int count = 0;

        RE:
            count++;
            string cmd = $"fw_setenv manufacturer {manufact}";
            testing.LOGSYSTEM += string.Format("Gửi lệnh: {0} \r\n", cmd);
            sSH.WriteLine(cmd);
            Thread.Sleep(500);
            temp = sSH.Read();
            testing.LOGSYSTEM += temp == null || temp == "" ? "..." : temp;
            ret = temp.Contains("root@VNPT:~#") || temp.Contains("root@VNPT:(unreachable)/root#") || temp.EndsWith("[x]");
            if (!ret) {
                if (count < max_count) {
                    goto RE;
                }
            }
            bool kq = temp.EndsWith("[x]");
            kq = !kq;
            ret = ret && kq;

            if (!ret) {
                testing.LOGSYSTEM += temp.EndsWith("[x]") ? "\r\n [FAIL] Mất kết nối SSH => dây LAN lỏng hoặc sản phẩm bị mất nguồn.\r\n" : "\r\n [FAIL] Quá TimeOut gửi lệnh. \r\n";
            }
            return ret;
        }


        //ok
        bool tran_fw_from_pc_to_imap(SSH<testingInfomation> sSH, testingInfomation testing) {
            bool ret = false;
            testing.LOGSSH = "";
            string FW_name_temp = GlobalData.initSetting.FirmwareFile;
            string IP_server_temp = testing.LocalIP;

            //Transfer firmware from PC to IMAP ~~~~~~~//
            int MAX_TIME_TRANSFER_FW = 360;
            try {
                MAX_TIME_TRANSFER_FW = int.Parse(GlobalData.initSetting.TimeoutTransFWFile);
            }
            catch { MAX_TIME_TRANSFER_FW = 360; }

            testing.LOGSYSTEM += string.Format("\r\n Transfer firmware {0} from PC to IMAP...\r\n", FW_name_temp);
            testing.LOGSYSTEM += string.Format("Gửi lệnh: {0} \r\n", string.Format("tftp -l /tmp/{0} -r {1} -g {2} 69", FW_name_temp, FW_name_temp, IP_server_temp));
            sSH.WriteLine(string.Format("tftp -l /tmp/{0} -r {1} -g {2} 69", FW_name_temp, FW_name_temp, IP_server_temp));
            Thread.Sleep(500);
            sSH.Read();
            string temp = "";
            int count = 0;
        RE_TF_FW:
            count++;
            temp = sSH.Read();
            testing.LOGSYSTEM += temp == null || temp == "" ? "..." : temp;
            ret = temp.Contains("root@VNPT:~#") || temp.Contains("root@VNPT:(unreachable)/root#") || temp.EndsWith("[x]");
            if (!ret) {
                if (count < MAX_TIME_TRANSFER_FW * 5) {
                    Thread.Sleep(200);
                    goto RE_TF_FW;
                }
            }
            bool kq = temp.ToLower().Contains("tftp") || temp.ToLower().Contains("timeout") || temp.ToLower().Contains("error") || temp.ToLower().Contains("not") || temp.EndsWith("[x]");
            kq = !kq;
            ret = ret && kq;

            if (!ret) {
                testing.LOGSYSTEM += temp.EndsWith("[x]") ? "\r\n [FAIL] Mất kết nối SSH => dây LAN lỏng hoặc sản phẩm bị mất nguồn.\r\n" : "\r\n [FAIL] Quá TimeOut gửi lệnh. \r\n";
            }

            return ret;
        }


        //ok
        bool check_md5_sum_fw(SSH<testingInfomation> sSH, testingInfomation testing) {
            bool ret = false;
            testing.LOGSSH = "";
            string FW_name_temp = GlobalData.initSetting.FirmwareFile;
            string IP_server_temp = testing.LocalIP;

            //Check MD5 Sum ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            int MAX_TIME_CHECK_MD5 = 20;
            try {
                MAX_TIME_CHECK_MD5 = int.Parse(GlobalData.initSetting.TimeoutCheckMD5);
            }
            catch { MAX_TIME_CHECK_MD5 = 20; }

            testing.LOGSYSTEM += "\r\n Gửi lệnh check Md5sum file firmware: \r\n";
            sSH.WriteLine(string.Format("md5sum /tmp/{0}", FW_name_temp));
            Thread.Sleep(500);
            int count = 0;
            string temp = "";
        RE_CK_MD5:
            count++;
            temp += sSH.Read();
            testing.LOGSYSTEM += temp;

            ret = temp.Contains(GlobalData.initSetting.Md5Sum.ToLower());

            if (!ret) {
                if (count < MAX_TIME_CHECK_MD5 * 5) {
                    Thread.Sleep(200); //timeout = 20s
                    goto RE_CK_MD5;
                }
            }
            testing.LOGSYSTEM += "Thông tin md5sum file firmware đọc được: " + temp + "\r\n";

            if (!ret) {
                testing.LOGSYSTEM += "[FAIL] Mã Md5sum file firmware không trùng khớp. \r\n";
            }
            else {
                testing.LOGSYSTEM += "[PASS] Mã Md5sum file firmware trùng khớp.\n";
            }

            return ret;
        }


        //ok
        bool upgrade_fw(SSH<testingInfomation> sSH, testingInfomation testing) {
            string FW_name_temp = GlobalData.initSetting.FirmwareFile;
            string IP_server_temp = testing.LocalIP;

            //Upgrade firmware ~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            int MAX_TIME_UPGRADE_FW = 360;
            try {
                MAX_TIME_UPGRADE_FW = int.Parse(GlobalData.initSetting.TimeoutUpgradeFirmware);
            }
            catch { MAX_TIME_UPGRADE_FW = 360; }

            testing.LOGSYSTEM += "Upgrade firmware for IMAP......\r\n";
            testing.LOGSYSTEM += string.Format("Gửi lệnh: /tmp/upfw.sh /tmp/{0} 0 0 \r\n", FW_name_temp);
            sSH.WriteLine(string.Format("/sbin/upfw.sh /tmp/{0} 0 0", FW_name_temp));
            Thread.Sleep(500);
            sSH.Read();

            int count = 0;
            bool ss = false;
            bool condition = false;
            bool ret = false;

            string log_temp = "";
            string data = "";

        RE_UP_FW:
            count++;
            data = sSH.Read();
            log_temp += data;
            testing.LOGSYSTEM += data;

            if (data.EndsWith("[x]")) { ret = false; goto END; }

            if (condition == false) {
                //Writing from <stdin> to firmware
                foreach (var str in GlobalData.startUpgradeStrings) {
                    if (str != null && str.Length > 0) {
                        ss = log_temp.ToLower().Contains(str.ToLower());
                        if (ss) break;
                    }
                }

                if (!ss) {
                    if (count < MAX_TIME_UPGRADE_FW * 5) {
                        Thread.Sleep(200);
                        goto RE_UP_FW;
                    }
                }
                else {
                    condition = true;
                    Thread.Sleep(200);
                    goto RE_UP_FW;

                }
            }
            else {
                foreach (var str in GlobalData.completeStrings) {
                    if (str != null && str.Length > 0) {
                        ret = log_temp.ToLower().Contains(str.ToLower());
                        if (ret) break;
                    }
                }
                if (!ret) {
                    if (count < MAX_TIME_UPGRADE_FW * 5) {
                        Thread.Sleep(200);
                        goto RE_UP_FW;
                    }
                }
            }

            sSH.WriteLine("");
            Thread.Sleep(100);

        END:
            testing.LOGSYSTEM += string.Format("\r\n KẾT QUẢ UPGRADE FIRMWARE: {0}\r\n", ret == true ? "PASS" : "FAIL");
            if (data.EndsWith("[x]"))
                testing.LOGSYSTEM += "Error: Mất kết nối SSH với sản phẩm => dây LAN bị lỏng hoặc sản phẩm bị mất nguồn.\n";
            return ret;
        }


        #endregion

    }
}
