using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UtilityPack.IO;
using System.Reflection;
using System.Diagnostics;
using EW12S.Custom;


namespace EW12S.IO {
    public class LogTotal {

        private static object obj = new object();

        class verify_totalfield {

            public verify_totalfield() {
                DATETime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                PCName = Environment.MachineName.ToString();
                MacAddress = "NULL";
                JigNumber = "-";
                Operator = "-";
                SoftwareVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                VerifyMacEthernet = "-";
                VerifyMacWifi2G = "-";
                VerifyMacWifi5G = "-";
                VerifyFirmwareResult = "-";
                TotalResult = "-";
            }

            public string DATETime { get; set; }
            public string PCName { get; set; }
            public string MacAddress { get; set; }
            public string JigNumber { get; set; }
            public string Operator { get; set; }
            public string SoftwareVersion { get; set; }
            public string VerifyMacEthernet { get; set; }
            public string VerifyMacWifi2G { get; set; }
            public string VerifyMacWifi5G { get; set; }
            public string VerifyFirmwareResult { get; set; }
            public string TotalResult { get; set; }
        }

        class upgrade_totalfield {

            public upgrade_totalfield() {
                DATETime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                PCName = Environment.MachineName.ToString();
                MacAddress = "NULL";
                JigNumber = "-";
                Operator = "-";
                SoftwareVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                UpgradeFirmwareResult = "-";
                TotalResult = "-";
            }

            public string DATETime { get; set; }
            public string PCName { get; set; }
            public string MacAddress { get; set; }
            public string JigNumber { get; set; }
            public string Operator { get; set; }
            public string SoftwareVersion { get; set; }
            public string UpgradeFirmwareResult { get; set; }
            public string TotalResult { get; set; }

        }


        string dir_log = "";
        int _jignumber;


        public LogTotal(int jignumber) {
            this._jignumber = jignumber;

            dir_log = GlobalData.totalDirectory;
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.Station);
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.StationID);
            
            if (!Directory.Exists(dir_log)) Directory.CreateDirectory(dir_log);
        }


        /// <summary>
        /// SAVE LOG TOTAL ------------------------//
        /// </summary>
        /// <param name="testing"></param>
        /// <returns></returns>
        public bool Save(testingInfomation testing) {
            lock (obj) {
                try {
                    string f = String.Format("EW12S_{0}.csv", DateTime.Now.ToString("yyyyMMdd"));

                    switch (GlobalData.initSetting.Station) {
                        case "ASM-VerifyFirmware": {
                                var field = new verify_totalfield();
                                field.MacAddress = testing.MAC_LABEL_ETHERNET;
                                field.JigNumber = this._jignumber.ToString();
                                field.VerifyMacEthernet = testing.MacEthernetResult;
                                field.VerifyMacWifi2G = testing.MacWifi2GResult;
                                field.VerifyMacWifi5G = testing.MacWifi5GResult;
                                field.VerifyFirmwareResult = testing.VerifyFirmwareResult;
                                field.TotalResult = testing.TotalResult;

                                List<verify_totalfield> fields = new List<verify_totalfield>();
                                fields.Add(field);

                                CsvHelper<verify_totalfield>.ToCsvFile(fields, Path.Combine(dir_log, f), Encoding.Unicode, true);
                                break;
                            }
                        case "ASM-UpGradeFirmware": {
                                var field = new upgrade_totalfield();
                                field.MacAddress = testing.MAC_LABEL_ETHERNET;
                                field.JigNumber = this._jignumber.ToString();
                                field.UpgradeFirmwareResult = testing.TotalResult;
                                field.TotalResult = testing.TotalResult;

                                List<upgrade_totalfield> fields = new List<upgrade_totalfield>();
                                fields.Add(field);

                                CsvHelper<upgrade_totalfield>.ToCsvFile(fields, Path.Combine(dir_log, f), Encoding.Unicode, true);
                                break;
                            }
                        default: break;
                    }
                    return true;
                }
                catch (Exception ex) {
                    testing.LOGSYSTEM += ex.Message + "\r\n";
                    return false;
                }
            }
        }

    }
}
