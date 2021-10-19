using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.Threading;
using EW12SG.Custom;
using Renci.SshNet;
using Tamir.SharpSsh;
using SimpleWifi;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UtilityPack.Converter;

namespace EW12SG.Base {
    public class BaseFunction {

        public static bool get_Testing_Info_By_JIG(int jignumber, ref testingInfomation tf) {
            try {
                switch (jignumber) {
                    case 1: { tf = GlobalData.testingInfo1; break; }
                    case 2: { tf = GlobalData.testingInfo2; break; }
                    case 3: { tf = GlobalData.testingInfo3; break; }
                    case 4: { tf = GlobalData.testingInfo4; break; }
                    case 5: { tf = GlobalData.testingInfo5; break; }
                    case 6: { tf = GlobalData.testingInfo6; break; }
                    case 7: { tf = GlobalData.testingInfo7; break; }
                    case 8: { tf = GlobalData.testingInfo8; break; }
                    default: break;
                }

                return true;
            } catch { return false; }
        }

        //Ping to ip with timeout and retry
        public static bool pingToIPAddress(string ip, int timeout, int retry) {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            bool ret = false;
            int count = 0;
        REP:
            count++;
            try {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    ret = true;
                } else {
                    ret = false;
                }
            } catch {
                ret = false;
            }

            if (ret == false && count < retry) goto REP;
            return ret;
        }

        public static bool pingIPAutoTest(string nameOrAddress) {
            bool pingable = false;
            Ping pinger = null;

            try {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException) {
                // Discard PingExceptions and return false;
            }
            finally {
                if (pinger != null) {
                    pinger.Dispose();
                }
            }
            return pingable;
        }


        public static bool pingToHost(string hostAddress) {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try {
                PingReply reply = pingSender.Send(hostAddress, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// check format of string is ip address or not. Return true = valid, false = not valid
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool isIPAddress(string ip) {
            string pattern = "^[0-9]{3}.[0-9]{3}.[0-9]+.[0-9]+$";
            return Regex.IsMatch(ip, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// compare ip address. Return [-2, ip is not valid] // [-1, ipvalue smaller ipref] // [0, ipvalue equal ipref] // [1, ipvalue greater ipref]
        /// </summary>
        /// <param name="ipvalue"></param>
        /// <param name="ipref"></param>
        /// <returns></returns>
        public static int compare2IPAddress(string ipvalue, string ipref) {
            if (isIPAddress(ipvalue) && isIPAddress(ipref) == false) return -2;
            int value = int.Parse(ipvalue.Split('.')[3]);
            int refer = int.Parse(ipref.Split('.')[3]);

            if (value < refer) return -1;
            else if (value == refer) return 0;
            else return 1;
        }

        /// <summary>
        /// subtract 2 ip. Return [-1, ip is not valid] // diff > 0
        /// </summary>
        /// <param name="ip_upper"></param>
        /// <param name="ip_lower"></param>
        /// <returns></returns>
        public static int Subtract2IPAddress(string ip_lower, string ip_upper) {
            if (isIPAddress(ip_upper) && isIPAddress(ip_lower) == false) return -1;
            int upper = int.Parse(ip_upper.Split('.')[3]);
            int lower = int.Parse(ip_lower.Split('.')[3]);

            int diff = upper - lower;
            return diff >= 0 ? diff : -1;
        }


        /// <summary>
        /// Add mac address with number
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string AddMacAddress(string mac, int num) {
            string header = mac.Substring(0, 6);
            string value = mac.Substring(6, 6);

            int macInt = Convert.ToInt32(value, 16);
            int newmac = macInt + num;
            return string.Format("{0}{1}", header, newmac.ToString("X").PadLeft(6, '0'));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static bool isMacAddress(string mac) {
            return Regex.IsMatch(mac.ToUpper(), "^[A-F,0-9]{12}$");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="End_MAC"></param>
        /// <param name="timeout_miliseconds"></param>
        /// <param name="retry_time"></param>
        /// <returns></returns>
        public static bool Connect_Wifi(string End_MAC, int timeout_miliseconds, int retry_time) {
            int retry = 0;
        STA:
            retry++;
            bool kq = false;
            Thread t = new Thread(new ThreadStart(() => {
                kq = Connect_Wifi(End_MAC);
            }));
            t.IsBackground = true;
            t.Start();

            int count = 0;
            int max = timeout_miliseconds / 100;
        RE:
            count++;
            if (count < max) {
                if (t.IsAlive == true) {
                    Thread.Sleep(100);
                    goto RE;
                }
            } else {
                t.Abort();
                if (retry < retry_time) {
                    Thread.Sleep(1000);
                    goto STA;
                }
            }
            return kq;
        }


        public static bool Scan_SSID_Name(string End_MAC) {
            Wifi wf = new Wifi();
            bool ret = false;
            
            IEnumerable<AccessPoint> accessPoints = wf.GetAccessPoints();
            foreach (var ap in accessPoints) {
                if (ap.Name.Contains(End_MAC.ToLower())) {
                    ret = true;
                    break;
                }
            }

            return ret;
        }


        public static bool Connect_Wifi(string End_MAC) {
            //wifi object
            Wifi wifi = new Wifi();

            //disconnect wifi
            wifi.Disconnect();

            //delay
            Thread.Sleep(250);

            //get list of access points
            IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints();

            //for each access point from list
            foreach (var ap in accessPoints) {
                if (ap.Name.Contains(End_MAC.ToLower())) {
                    if (!ap.IsConnected) {

                        //connect if not connected
                        AuthRequest authRequest = new AuthRequest(ap);
                        if (authRequest.IsUsernameRequired == true) authRequest.Username = "user";
                        if (authRequest.IsPasswordRequired == true) authRequest.Password = "EW@" + End_MAC.ToLower();
                        ap.Connect(authRequest);
                    }
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="testing"></param>
        /// <param name="timeout_miliseconds"></param>
        /// <returns></returns>
        public static bool Connect_Wifi(testingInfomation testing, int timeout_miliseconds, int retry_time) {
            int retry = 0;
        STA:
            retry++;
            bool kq = false;
            Thread t = new Thread(new ThreadStart(() => {
                kq = Connect_Wifi(testing);
            }));
            t.IsBackground = true;
            t.Start();

            int count = 0;
            int max = timeout_miliseconds / 100;
        RE:
            count++;
            if (count < max) {
                if (t.IsAlive == true) {
                    Thread.Sleep(100);
                    testing.LOGSYSTEM += string.Format("...{0}/{1}\r\n", count, max);
                    goto RE;
                }
                else {
                    if (!kq) {
                        if (retry < retry_time) {
                            Thread.Sleep(1000);
                            testing.LOGSYSTEM += string.Format("...Delay 1000 ms\r\n");
                            testing.LOGSYSTEM += string.Format("...Retry {0}/{1}\r\n", retry, retry_time);
                            goto STA;
                        }
                    }
                }
            } else {
                testing.LOGSYSTEM += string.Format("...Status = Disconnected\r\n");
                testing.ErrorMessage = string.Format("Connection timeout {0} ms.", timeout_miliseconds * retry_time);
                t.Abort();
                if (retry < retry_time) {
                    Thread.Sleep(1000);
                    testing.LOGSYSTEM += string.Format("...Delay 1000 ms\r\n");
                    testing.LOGSYSTEM += string.Format("...Retry {0}/{1}\r\n", retry, retry_time);
                    goto STA;
                }
            }
            
            if (kq) testing.ErrorMessage = "--";
            return kq;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="testing"></param>
        /// <returns></returns>
        public static bool Connect_Wifi(testingInfomation testing) {
            try {
                //wifi object
                Wifi wifi = new Wifi();

                //disconnect wifi
                wifi.Disconnect();
                testing.LOGSYSTEM += string.Format("...Disconnect wifi\r\n");

                //delay
                Thread.Sleep(250);
                testing.LOGSYSTEM += string.Format("...Delay 250 ms\r\n");

                //get list of access points
                IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints();
                testing.LOGSYSTEM += string.Format("...Get list of access points\r\n");

                //show list access point
                foreach (var ap in accessPoints) {
                    testing.LOGSYSTEM += string.Format("...............{0}\r\n", ap.Name);
                }

                //for each access point from list
                foreach (var ap in accessPoints) {
                    if (ap.Name.Contains(testing.MAC_LABEL_ETHERNET.Substring(6, 6).ToLower())) {
                        if (!ap.IsConnected) {

                            //connect if not connected
                            AuthRequest authRequest = new AuthRequest(ap);
                            if (authRequest.IsUsernameRequired == true) authRequest.Username = "user";
                            if (authRequest.IsPasswordRequired == true) authRequest.Password = "EW@" + testing.MAC_LABEL_ETHERNET.Substring(6, 6).ToLower();
                            testing.LOGSYSTEM += string.Format("...Connecting to AP wifi\r\n");

                            ap.Connect(authRequest);
                            testing.LOGSYSTEM += string.Format("...Connected\r\n");
                        }
                        return true;
                    }
                }

                testing.ErrorMessage = string.Format("PC can't find out AP EW_{0}.", testing.MAC_LABEL_ETHERNET.Substring(6, 6).ToLower());
                return false;
            } catch (Exception ex) {
                testing.LOGSYSTEM += string.Format("...{0}\r\n", ex.ToString());
                return false;
            }
            
        }


        /// <summary>
        /// Lấy thông tin địa chỉ IP Address của Access Point. Return IP Address / NULL
        /// </summary>
        /// <returns></returns>
        public static string getAccessPointIPAddress() {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "ipconfig.exe";
            p.Start();
            p.WaitForExit();
            string data = p.StandardOutput.ReadToEnd();
            p.Close();

            if (!data.Contains("Wireless")) return "NULL";
            string[] buffer = data.Split(new string[] { "Wireless" }, StringSplitOptions.None);
            List<string> temps = new List<string>();
            for (int i = 0; i < buffer.Length; i++) { if (i > 0) temps.Add(buffer[i]); }

            List<string> wifis = new List<string>();
            for (int i = 0; i < temps.Count; i++) {
                if (temps[i].Contains("Ethernet")) {
                    data = temps[i].Split(new string[] { "Ethernet" }, StringSplitOptions.None)[0];
                    wifis.Add(data);

                } else {
                    wifis.Add(temps[i]);
                }
            }

            //Read Access Point IP Address
            string _gateway = "";
            foreach (var w in wifis) {
                if (w.Contains("Default Gateway")) {
                    _gateway += w.Split(new string[] { "Default Gateway" }, StringSplitOptions.None)[1].Split(':')[1].Replace("\r", "").Replace("\n", "").Trim();
                    break;
                }
            }
            
            return _gateway == "" ? "NULL" : _gateway;
        }


        public static string addIPAddress(string ip, int num) {
            string[] buffer = ip.Split('.');
            return string.Format("{0}.{1}.{2}.{3}", buffer[0], buffer[1], buffer[2], int.Parse(buffer[3]) + num);
        }

    }
}
