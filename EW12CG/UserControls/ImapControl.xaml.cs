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

using EW12CG.Custom;
using EW12CG;
using EW12CG.Protocol;
using EW12CG.Base;

namespace EW12CG.UserControls
{
    /// <summary>
    /// Interaction logic for ImapControl.xaml
    /// </summary>
    public partial class ImapControl : UserControl
    {
        IMapInformation imapinfo = null;
        public ImapControl(IMapInformation _imapinfo)
        {
            InitializeComponent();
            this.imapinfo = _imapinfo;
            this.DataContext = this.imapinfo;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem item = sender as MenuItem;
            SSH<testingInfomation> ssh = null;
            bool r = false;
            int count = 0;
            Thread v = null;
            bool _flag = false;

            switch (item.Header.ToString()) {
                case "get mac address": {
                        Thread t = new Thread(new ThreadStart(() => {
                            string data = "";
                            count = 0;
                            _flag = false;
                        RE:
                            count++;
                            testingInfomation ttt = new testingInfomation();
                            ssh = new SSH<testingInfomation>(ttt);
                            r = ssh.Login(imapinfo.ip, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
                            if (r == false && count < 3) goto RE;
                            
                            if (r) {
                                ssh.WriteLine(""); Thread.Sleep(100); data = ssh.Read();
                                //get mac ethernet
                                ssh.WriteLine("cat /sys/class/net/eth0/address"); Thread.Sleep(100); data = ssh.Read();
                                //get mac wifi0
                                ssh.WriteLine("cat /sys/class/net/wifi0/address"); Thread.Sleep(100); data += ssh.Read();
                                //get mac wifi1
                                ssh.WriteLine("cat /sys/class/net/wifi1/address"); Thread.Sleep(100); data += ssh.Read();

                                ssh.CloseShellStream();
                                ssh.Close();
                            }

                            ssh = null;
                            _flag = true; Thread.Sleep(600);

                            Dispatcher.Invoke(new Action(() => {
                                MessageWindow message = new MessageWindow(string.Format("MAC AP {0}", this.imapinfo.ip), data);
                                message.ShowDialog();
                            }));

                            //MessageBox.Show(data, string.Format("MAC AP {0}", this.imapinfo.ip), MessageBoxButton.OK, MessageBoxImage.Information);
                        }));
                        t.IsBackground = true;
                        t.Start();

                        Thread.Sleep(10);
                        v = new Thread(new ThreadStart(() => {
                            string temp = "";
                            while (!_flag) {
                                temp += ".";
                                this.imapinfo.accessapstring = "Wait" + temp;
                                Thread.Sleep(500);
                            }
                            this.imapinfo.accessapstring = "";
                        }));
                        v.IsBackground = true;
                        v.Start();
                        
                        break;
                    }
                case "change ip address to 192.168.88.1": {
                        Thread t = new Thread(new ThreadStart(() => {
                            string data = "";
                            count = 0;
                            _flag = false;
                        RE:
                            count++;
                            testingInfomation ttt = new testingInfomation();
                            ssh = new SSH<testingInfomation>(ttt);
                            r = ssh.Login(imapinfo.ip, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
                            if (r == false && count < 3) goto RE;

                            if (r) {
                                ssh.WriteLine(""); Thread.Sleep(100); data = ssh.Read();
                                //change ip
                                ssh.WriteLine("uci set network.lan.ifname=eth0");
                                Thread.Sleep(500);
                                ssh.WriteLine("uci set network.lan.ipaddr=192.168.88.1");
                                Thread.Sleep(500);
                                ssh.WriteLine("uci delete network.wan");
                                Thread.Sleep(500);
                                ssh.WriteLine("uci commit network");
                                Thread.Sleep(500);
                                ssh.WriteLine("/etc/init.d/network restart && /etc/init.d/dnsmasq restart");
                                Thread.Sleep(1000);

                                count = 0;
                                r = false;
                                while(count < 20) {
                                    count++;
                                    if (BaseFunction.pingIPAutoTest("192.168.88.1")) {
                                        r = true;
                                        break;
                                    }
                                    else Thread.Sleep(500);
                                }

                                ssh.CloseShellStream();
                                ssh.Close();
                            }

                            ssh = null;
                            _flag = true; Thread.Sleep(1100);
                            this.imapinfo.isconnected = !r;
                            MessageBox.Show(string.Format("Change ip to 192.168.88.1 is {0}.", r == true ? "passed" : "failed"), string.Format("MAC AP {0}", this.imapinfo.ip), MessageBoxButton.OK, MessageBoxImage.Information);
                        }));
                        t.IsBackground = true;
                        t.Start();

                        Thread.Sleep(10);
                        v = new Thread(new ThreadStart(() => {
                            string temp = "";
                            while (!_flag) {
                                temp += ".";
                                this.imapinfo.accessapstring = "Wait" + temp;
                                Thread.Sleep(1000);
                            }
                            this.imapinfo.accessapstring = "";
                        }));
                        v.IsBackground = true;
                        v.Start();
                        break;
                    }
                case "get firmware build time stamp": {
                        Thread t = new Thread(new ThreadStart(() => {
                            string data = "";
                            count = 0;
                            _flag = false;
                        RE:
                            count++;
                            testingInfomation ttt = new testingInfomation();
                            ssh = new SSH<testingInfomation>(ttt);
                            r = ssh.Login(imapinfo.ip, GlobalData.initSetting.SSHUser, GlobalData.initSetting.SSHPassword);
                            if (r == false && count < 3) goto RE;

                            if (r) {
                                ssh.WriteLine(""); Thread.Sleep(100); data = ssh.Read();
                                //get firmware build time stamp
                                ssh.WriteLine("cat /proc/version");
                                Thread.Sleep(500);
                                data = ssh.Read();
                                
                                ssh.CloseShellStream();
                                ssh.Close();
                            }

                            ssh = null;
                            _flag = true; Thread.Sleep(600);

                            Dispatcher.Invoke(new Action(() => {
                                MessageWindow message = new MessageWindow(string.Format("MAC AP {0}", this.imapinfo.ip), data);
                                message.ShowDialog();
                            }));
                            
                            //MessageBox.Show(data, string.Format("MAC AP {0}", this.imapinfo.ip), MessageBoxButton.OK, MessageBoxImage.Information);
                        }));
                        t.IsBackground = true;
                        t.Start();

                        Thread.Sleep(10);
                        v = new Thread(new ThreadStart(() => {
                            string temp = "";
                            while (!_flag) {
                                temp += ".";
                                this.imapinfo.accessapstring = "Wait" + temp;
                                Thread.Sleep(500);
                            }
                            this.imapinfo.accessapstring = "";
                        }));
                        v.IsBackground = true;
                        v.Start();
                        break;
                    }
                default: break;
            }
        }
    }
}
