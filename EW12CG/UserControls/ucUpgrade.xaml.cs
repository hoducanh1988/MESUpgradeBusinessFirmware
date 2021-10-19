using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using EW12CG.Protocol;
using EW12CG.Base;
using EW12CG.Function;
using EW12CG.Custom;
using System.Windows.Threading;
using EW12CG.IO;
using System.Net.NetworkInformation;

namespace EW12CG.UserControls {
    /// <summary>
    /// Interaction logic for ucUpgrade.xaml
    /// </summary>
    public partial class ucUpgrade : UserControl {

        volatile bool scrollFlag = false;
        DispatcherTimer timer = null;

        public ucUpgrade() {
            InitializeComponent();
            //binding data
            setBindingValueForControl();
            //init control
            initControlScrollView();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_tag = (string)b.Tag;

            switch (b_tag) {
                case "start": {
                        b.Content = "Stop";
                        //thread change ip
                        Thread t = new Thread(new ThreadStart(() => {

                            init_testing_item();
                            scrollFlag = true;

                            //check run phan mem tftpd64/tftpd32, check exist va md5 file script, file firmware
                            if ((check_tftpd() & check_file_firmware()) == false) {
                                Application.Current.Dispatcher.Invoke(new Action(() => { b.Content = "Start"; }));
                                scrollFlag = false;
                                return;
                            }

                            //điều khiển sản phẩm upgrade firmware
                            loop_upgrade_firmware();

                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "clean1": { GlobalData.testingInfo1.InitValue(); break; }
                case "clean2": { GlobalData.testingInfo2.InitValue(); break; }
                case "clean3": { GlobalData.testingInfo3.InitValue(); break; }
                case "clean4": { GlobalData.testingInfo4.InitValue(); break; }
                case "clean5": { GlobalData.testingInfo5.InitValue(); break; }
                case "clean6": { GlobalData.testingInfo6.InitValue(); break; }
                case "clean7": { GlobalData.testingInfo7.InitValue(); break; }
                case "clean8": { GlobalData.testingInfo8.InitValue(); break; }
                case "clean_all": {
                        if (GlobalData.testingInfo1.TotalResult == "PASS" || GlobalData.testingInfo1.TotalResult == "FAIL") GlobalData.testingInfo1.InitValue();
                        if (GlobalData.testingInfo2.TotalResult == "PASS" || GlobalData.testingInfo2.TotalResult == "FAIL") GlobalData.testingInfo2.InitValue();
                        if (GlobalData.testingInfo3.TotalResult == "PASS" || GlobalData.testingInfo3.TotalResult == "FAIL") GlobalData.testingInfo3.InitValue();
                        if (GlobalData.testingInfo4.TotalResult == "PASS" || GlobalData.testingInfo4.TotalResult == "FAIL") GlobalData.testingInfo4.InitValue();
                        if (GlobalData.testingInfo5.TotalResult == "PASS" || GlobalData.testingInfo5.TotalResult == "FAIL") GlobalData.testingInfo5.InitValue();
                        if (GlobalData.testingInfo6.TotalResult == "PASS" || GlobalData.testingInfo6.TotalResult == "FAIL") GlobalData.testingInfo6.InitValue();
                        if (GlobalData.testingInfo7.TotalResult == "PASS" || GlobalData.testingInfo7.TotalResult == "FAIL") GlobalData.testingInfo7.InitValue();
                        if (GlobalData.testingInfo8.TotalResult == "PASS" || GlobalData.testingInfo8.TotalResult == "FAIL") GlobalData.testingInfo8.InitValue();
                        break;
                    }
            }
        }

        #region event handle

        private void RadioButton_Click(object sender, RoutedEventArgs e) {
            RadioButton rb = sender as RadioButton;
            string rb_tag = (string)rb.Tag;

            switch (rb_tag) {
                //DUT-1
                case "logitem_dut1": {
                        this.grid_dut1.Children.Clear();
                        this.grid_dut1.Children.Add(new ucLogItem(GlobalData.testingInfo1));
                        break;
                    }
                case "logsystem_dut1": {
                        this.grid_dut1.Children.Clear();
                        this.grid_dut1.Children.Add(new ucLogSystem(GlobalData.testingInfo1));
                        break;
                    }
                case "logssh_dut1": {
                        this.grid_dut1.Children.Clear();
                        this.grid_dut1.Children.Add(new ucLogSsh(GlobalData.testingInfo1));
                        break;
                    }

                //DUT-2
                case "logitem_dut2": {
                        this.grid_dut2.Children.Clear();
                        this.grid_dut2.Children.Add(new ucLogItem(GlobalData.testingInfo2));
                        break;
                    }
                case "logsystem_dut2": {
                        this.grid_dut2.Children.Clear();
                        this.grid_dut2.Children.Add(new ucLogSystem(GlobalData.testingInfo2));
                        break;
                    }
                case "logssh_dut2": {
                        this.grid_dut2.Children.Clear();
                        this.grid_dut2.Children.Add(new ucLogSsh(GlobalData.testingInfo2));
                        break;
                    }

                //DUT-3
                case "logitem_dut3": {
                        this.grid_dut3.Children.Clear();
                        this.grid_dut3.Children.Add(new ucLogItem(GlobalData.testingInfo3));
                        break;
                    }
                case "logsystem_dut3": {
                        this.grid_dut3.Children.Clear();
                        this.grid_dut3.Children.Add(new ucLogSystem(GlobalData.testingInfo3));
                        break;
                    }
                case "logssh_dut3": {
                        this.grid_dut3.Children.Clear();
                        this.grid_dut3.Children.Add(new ucLogSsh(GlobalData.testingInfo3));
                        break;
                    }

                //DUT-4
                case "logitem_dut4": {
                        this.grid_dut4.Children.Clear();
                        this.grid_dut4.Children.Add(new ucLogItem(GlobalData.testingInfo4));
                        break;
                    }
                case "logsystem_dut4": {
                        this.grid_dut4.Children.Clear();
                        this.grid_dut4.Children.Add(new ucLogSystem(GlobalData.testingInfo4));
                        break;
                    }
                case "logssh_dut4": {
                        this.grid_dut4.Children.Clear();
                        this.grid_dut4.Children.Add(new ucLogSsh(GlobalData.testingInfo4));
                        break;
                    }

                //DUT-5
                case "logitem_dut5": {
                        this.grid_dut5.Children.Clear();
                        this.grid_dut5.Children.Add(new ucLogItem(GlobalData.testingInfo5));
                        break;
                    }
                case "logsystem_dut5": {
                        this.grid_dut5.Children.Clear();
                        this.grid_dut5.Children.Add(new ucLogSystem(GlobalData.testingInfo5));
                        break;
                    }
                case "logssh_dut5": {
                        this.grid_dut5.Children.Clear();
                        this.grid_dut5.Children.Add(new ucLogSsh(GlobalData.testingInfo5));
                        break;
                    }

                //DUT-6
                case "logitem_dut6": {
                        this.grid_dut6.Children.Clear();
                        this.grid_dut6.Children.Add(new ucLogItem(GlobalData.testingInfo6));
                        break;
                    }
                case "logsystem_dut6": {
                        this.grid_dut6.Children.Clear();
                        this.grid_dut6.Children.Add(new ucLogSystem(GlobalData.testingInfo6));
                        break;
                    }
                case "logssh_dut6": {
                        this.grid_dut6.Children.Clear();
                        this.grid_dut6.Children.Add(new ucLogSsh(GlobalData.testingInfo6));
                        break;
                    }

                //DUT-7
                case "logitem_dut7": {
                        this.grid_dut7.Children.Clear();
                        this.grid_dut7.Children.Add(new ucLogItem(GlobalData.testingInfo7));
                        break;
                    }
                case "logsystem_dut7": {
                        this.grid_dut7.Children.Clear();
                        this.grid_dut7.Children.Add(new ucLogSystem(GlobalData.testingInfo7));
                        break;
                    }
                case "logssh_dut7": {
                        this.grid_dut7.Children.Clear();
                        this.grid_dut7.Children.Add(new ucLogSsh(GlobalData.testingInfo7));
                        break;
                    }

                //DUT-8
                case "logitem_dut8": {
                        this.grid_dut8.Children.Clear();
                        this.grid_dut8.Children.Add(new ucLogItem(GlobalData.testingInfo8));
                        break;
                    }
                case "logsystem_dut8": {
                        this.grid_dut8.Children.Clear();
                        this.grid_dut8.Children.Add(new ucLogSystem(GlobalData.testingInfo8));
                        break;
                    }
                case "logssh_dut8": {
                        this.grid_dut8.Children.Clear();
                        this.grid_dut8.Children.Add(new ucLogSsh(GlobalData.testingInfo8));
                        break;
                    }
            }


        }

        #endregion

        #region Sub Function

        void setBindingValueForControl() {
            //DUT-1
            this.Group1.DataContext = GlobalData.testingInfo1;
            this.grid_dut1.Children.Add(new ucLogSystem(GlobalData.testingInfo1));

            this.Group2.DataContext = GlobalData.testingInfo2;
            this.grid_dut2.Children.Add(new ucLogSystem(GlobalData.testingInfo2));

            this.Group3.DataContext = GlobalData.testingInfo3;
            this.grid_dut3.Children.Add(new ucLogSystem(GlobalData.testingInfo3));

            this.Group4.DataContext = GlobalData.testingInfo4;
            this.grid_dut4.Children.Add(new ucLogSystem(GlobalData.testingInfo4));

            this.Group5.DataContext = GlobalData.testingInfo5;
            this.grid_dut5.Children.Add(new ucLogSystem(GlobalData.testingInfo5));

            this.Group6.DataContext = GlobalData.testingInfo6;
            this.grid_dut6.Children.Add(new ucLogSystem(GlobalData.testingInfo6));

            this.Group7.DataContext = GlobalData.testingInfo7;
            this.grid_dut7.Children.Add(new ucLogSystem(GlobalData.testingInfo7));

            this.Group8.DataContext = GlobalData.testingInfo8;
            this.grid_dut8.Children.Add(new ucLogSystem(GlobalData.testingInfo8));

            this.DataContext = GlobalData.upgradeInfo;
        }

        void initControlScrollView() {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (sender, e) => {
                if (scrollFlag == true) { Scr_LogSYSTEM.ScrollToEnd(); }
            };
            timer.Start();
        }

        bool check_tftpd() {
            bool r = false;

            GlobalData.upgradeInfo.LOGDETAIL += "\r\nKiểm tra trạng thái phần mềm tftpd64/tftpd32 : ";
            r = UtilityPack.IO.WindowProcess.isProcessRunning("tftpd");

            if (!r) {
                GlobalData.upgradeInfo.LOGDETAIL += "[FAIL] chưa chạy\r\n";
                MessageBox.Show("Vui lòng bật phần mềm tftpd64/tftpd32 trước khi upgrade firmware thương mại.", "Lỗi phần mềm tftpd64/tftpd32", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                GlobalData.upgradeInfo.LOGDETAIL += "[PASS] đã chạy\r\n";
            }

            return r;
        }

        bool check_file_firmware() {
            bool r = false;

            SmbClient smb = new SmbClient();
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("\r\nKiểm tra file firmware  {0} : \r\n", GlobalData.initSetting.FirmwareFile);
            r = smb.IsFileExist(string.Format(@"C:\TFTP-Root\{0}", GlobalData.initSetting.FirmwareFile));

            if (!r) {
                GlobalData.upgradeInfo.LOGDETAIL += "[FAIL] file không tồn tại\r\n";
                MessageBox.Show(string.Format(@"File firmware {0} không tồn tại trong folder C:\TFTP-Root.", GlobalData.initSetting.FirmwareFile), "Lỗi file firmware " + GlobalData.initSetting.FirmwareFile, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                string md5 = smb.GetHash<System.Security.Cryptography.MD5>(string.Format(@"C:\TFTP-Root\{0}", GlobalData.initSetting.FirmwareFile)).Trim();
                r = md5.ToLower().Equals(GlobalData.initSetting.Md5Sum.ToLower());
                if (!r) {
                    GlobalData.upgradeInfo.LOGDETAIL += "[FAIL] file sai thông tin md5\r\n";
                    MessageBox.Show(string.Format(@"File firmware {0} trong folder C:\TFTP-Root sai thông tin md5.", GlobalData.initSetting.FirmwareFile), "Lỗi file firmware " + GlobalData.initSetting.FirmwareFile, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else {
                    GlobalData.upgradeInfo.LOGDETAIL += "[PASS] file hợp lệ\r\n\r\n";
                }
            }

            return r;
        }

        void init_testing_item() {
            GlobalData.testingInfo1.InitValue();
            GlobalData.testingInfo2.InitValue();
            GlobalData.testingInfo3.InitValue();
            GlobalData.testingInfo4.InitValue();
            GlobalData.testingInfo5.InitValue();
            GlobalData.testingInfo6.InitValue();
            GlobalData.testingInfo7.InitValue();
            GlobalData.testingInfo8.InitValue();
            GlobalData.upgradeInfo.InitValue();
        }

        void get_tab_ready(out int t_index) {
            t_index = -1;
            if (GlobalData.testingInfo1.TotalResult.Equals("--")) { t_index = 1; return; }
            if (GlobalData.testingInfo2.TotalResult.Equals("--")) { t_index = 2; return; }
            if (GlobalData.testingInfo3.TotalResult.Equals("--")) { t_index = 3; return; }
            if (GlobalData.testingInfo4.TotalResult.Equals("--")) { t_index = 4; return; }
            if (GlobalData.testingInfo5.TotalResult.Equals("--")) { t_index = 5; return; }
            if (GlobalData.testingInfo6.TotalResult.Equals("--")) { t_index = 6; return; }
            if (GlobalData.testingInfo7.TotalResult.Equals("--")) { t_index = 7; return; }
            if (GlobalData.testingInfo8.TotalResult.Equals("--")) { t_index = 8; return; }
        }

        void ping_to_mesh() {
        RE:
            bool r = BaseFunction.pingToHost(GlobalData.initSetting.APIP);
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("Ping to mesh {0} - {1}\r\n", GlobalData.initSetting.APIP, r ? "Passed" : "Failed");
            if (!r) {
                Thread.Sleep(1000);
                goto RE;
            }

            //    if (UtilityPack.IO.WindowProcess.isProcessRunning("pingNetwork")) {
            //        UtilityPack.IO.WindowProcess.killAllProcessByName("pingNetwork");
            //        Thread.Sleep(100);
            //    }

            //    Process.Start(GlobalData.pingPath);
            //    Thread.Sleep(1000);
            //    bool r = false;

            //RE:
            //    r = System.IO.File.Exists(GlobalData.pingResult);
            //    GlobalData.upgradeInfo.LOGDETAIL += string.Format("Ping to mesh {0} - {1}\r\n", GlobalData.initSetting.APIP, r ? "Passed" : "Failed");
            //    if (!r) {
            //        Thread.Sleep(1000);
            //        goto RE;
            //    }
        }

        void get_mac_ethernet(SSH sh, ref string mac_ethernet) {
        RE:
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("Get mac ethernet: \r\n");
            sh.WriteLine("cat /sys/class/net/eth0/address");
            Thread.Sleep(500);
            string mac = sh.Read().ToLower();
            mac = mac.Split(new string[] { "root@vnpt:~#" }, StringSplitOptions.None)[1];
            mac = mac.Replace("cat /sys/class/net/eth0/address", "").Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("{0}\r\n", mac);
            bool r = mac.Replace(":", "").Replace("-", "").Trim().Length == 12;

            if (!r) goto RE;
            else mac_ethernet = mac;
        }

        void login_to_mesh(ref SSH sh) {
        RE:
            sh = new SSH();
            bool r = sh.Login(GlobalData.initSetting.APIP, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("Login SSH to {0} - {1}\r\n", GlobalData.initSetting.APIP, r ? "Passed" : "Failed");
            if (!r) {
                Thread.Sleep(1000);
                goto RE;
            }
        }

        void change_ip_mesh(SSH sh, string ip_address) {
            //change ip
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("Change to new ip {0} :\r\n", ip_address);
            sh.WriteLine("ifconfig br-lan " + ip_address);
            Thread.Sleep(300);

            try {
                sh.Close();
                sh = null;
            }
            catch { }
        }

        bool ping_to_new_ip(string new_ip, int timeout_sec) {
            int count = 0;
        RE:
            count++;
            bool r = BaseFunction.pingToHost(new_ip);
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("Ping to new ip {0} - {1}\r\n", new_ip, r ? "Passed" : "Failed");
            if (!r) {
                if (count < timeout_sec) {
                    Thread.Sleep(1000);
                    goto RE;
                }
            }

            return r;
        }

        void upgrade_business_firmware(string ip, string mac) {
            Thread t = new Thread(new ThreadStart(() => {
                switch (ip) {
                    case "192.168.88.11": { upgrade_FW(GlobalData.testingInfo1, mac, 1); break; }
                    case "192.168.88.12": { upgrade_FW(GlobalData.testingInfo2, mac, 2); break; }
                    case "192.168.88.13": { upgrade_FW(GlobalData.testingInfo3, mac, 3); break; }
                    case "192.168.88.14": { upgrade_FW(GlobalData.testingInfo4, mac, 4); break; }
                    case "192.168.88.15": { upgrade_FW(GlobalData.testingInfo5, mac, 5); break; }
                    case "192.168.88.16": { upgrade_FW(GlobalData.testingInfo6, mac, 6); break; }
                    case "192.168.88.17": { upgrade_FW(GlobalData.testingInfo7, mac, 7); break; }
                    case "192.168.88.18": { upgrade_FW(GlobalData.testingInfo8, mac, 8); break; }
                }
            }));
            t.IsBackground = true;
            t.Start();
        }

        void loop_upgrade_firmware() {

            int tab_index = -1;
            bool r = true;

        LOOP:
            //check ping to mesh
            r = true;
            try {
                ping_to_mesh();
            } catch { r = false; }
            if (!r) goto LOOP;

            //get index of tab free
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("TabItem free: ");
            try {
                get_tab_ready(out tab_index);
            } catch { tab_index = -1; }
            GlobalData.upgradeInfo.LOGDETAIL += string.Format("...{0}\r\n", tab_index);
            if (tab_index == -1) {
                Thread.Sleep(3000);
                goto LOOP;
            }

            //login ssh
            SSH ssh = null;
            r = true;
            try {
                login_to_mesh(ref ssh);
            } catch { r = false; }
            if (!r) goto LOOP;
            
            //delay
            Thread.Sleep(1000);
            
            //get mac ethernet
            string mac = "";
            try {
                get_mac_ethernet(ssh, ref mac);
            } catch { mac = ""; }
            if (mac == "") { ssh.Close(); goto LOOP; }

            //change ip
            r = true;
            string new_ip = "";
            try {
                new_ip = string.Format("192.168.88.{0}", 10 + tab_index);
                change_ip_mesh(ssh, new_ip);
            } catch { r = false; }
            if (r == false || new_ip == "") goto LOOP;

            //ping to new ip
            r = true;
            try {
                if (ping_to_new_ip(new_ip, 15) == false) goto LOOP;
            } catch { r = false; }
            if (!r) goto LOOP;

            //upgrade firmware
            upgrade_business_firmware(new_ip, mac);

            //return loop
            Thread.Sleep(1000);
            goto LOOP;
        }

        void upgrade_FW(testingInfomation testing, string mac, int idx) {
            Stopwatch st = new Stopwatch();
            st.Start();
            bool ret = false;
            testing.scrollFlag = true;

            testing.MAC_LABEL_ETHERNET = mac.Trim().Replace("\r", "").Replace("\n", "").ToUpper();
            testing.TotalResult = "Upgrading...";
            ret = new UpgradeFirmware().Excute(testing);
            testing.TotalResult = ret == true ? "PASS" : "FAIL";

            //save log
            st.Stop();
            bool s = false;
            testing.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
            testing.LOGSYSTEM += string.Format("Save log data...\r\n");
            testing.LOGSYSTEM += string.Format("...Directory: {0}\r\n",
            GlobalData.initSetting.LogDirectory.Equals("Default") == true ? AppDomain.CurrentDomain.BaseDirectory : GlobalData.initSetting.LogDirectory);

            s = new LogTotal(idx).Save(testing); //total
            testing.LOGSYSTEM += string.Format("...Log total, result = {0}\r\n", s == true ? "PASS" : "FAIL");

            //show result test
            testing.LOGSYSTEM += string.Format("---------------------------------------------------[End]\r\n");
            testing.LOGSYSTEM += string.Format("\r\nTotal result: {0}\r\n", testing.TotalResult);
            testing.LOGSYSTEM += string.Format("Total time: {0} ms\r\n", st.ElapsedMilliseconds);
            testing.LOGSYSTEM += string.Format("Error message: {0}\r\n\r\n", testing.ErrorMessage);

            //log detail
            s = new LogDetail().Save(testing);

            //log ssh
            s = new LogSsh().Save(testing);

            testing.scrollFlag = false;
        }


        #endregion

    }
}
