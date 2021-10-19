using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EW30SX.Base;
using EW30SX.Protocol;
using EW30SX.Custom;


namespace EW30SX {
    public class GlobalData {

        public static object lockwifi = new object();
        public static MainInfo myAppInfo = new MainInfo();
        public static initSetting initSetting = new initSetting();
        public static volatile List<string> completeStrings = null;
        public static volatile List<string> startUpgradeStrings = null;
        public static string FlagScanSSID = "1";

        public static volatile testingInfomation testingInfo1 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 0) };
        public static volatile testingInfomation testingInfo2 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 1) };
        public static volatile testingInfomation testingInfo3 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 2) };
        public static volatile testingInfomation testingInfo4 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 3) };
        public static volatile testingInfomation testingInfo5 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 4) };
        public static volatile testingInfomation testingInfo6 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 5) };
        public static volatile testingInfomation testingInfo7 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 6) };
        public static volatile testingInfomation testingInfo8 = new testingInfomation() { NEWIP = BaseFunction.addIPAddress(GlobalData.initSetting.StartNewAPIP, 7) };

        public static upgradeInformation upgradeInfo = new upgradeInformation();

        public static string dir_Path = AppDomain.CurrentDomain.BaseDirectory;
        public static string totalDirectory = string.Format("{0}Logtotal\\", dir_Path);
        public static string detailDirectory = string.Format("{0}Logdetail\\", dir_Path);
        public static string singleDirectory = string.Format("{0}Logsingle\\", dir_Path);
        public static string sshDirectory = string.Format("{0}Logssh\\", dir_Path);
        public static string pingPath = string.Format("{0}PingToImap\\pingNetwork.exe", dir_Path);
        public static string pingResult = string.Format("{0}PingToImap\\ping_pass.txt", dir_Path);
    }


    public class myParams {

        public static List<string> Directorys;
        public static List<string> Stations;
        public static List<string> StationIDs;
        public static List<string> Manufacturers;

        static myParams() {

            Directorys = new List<string>() {
            "Default",
            "D:\\LOGDATA",
            "Other..."
            };

            Stations = new List<string>() {
            "ASM-UpGradeFirmware",
            "ASM-VerifyFirmware"
            };

            StationIDs = new List<string>();
            for (int i = 1; i <= 8; i++) {
                StationIDs.Add(string.Format("0{0}", i));
            }

            Manufacturers = new List<string>() {
                "VNPTT",
                "VNPTT-VNPT",
            };
        }

    }

}
