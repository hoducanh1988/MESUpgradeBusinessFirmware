using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityPack.IO;

using EW12S.Custom;
using System.Diagnostics;
using System.Reflection;

namespace EW12S.IO {
    public class LogDetail {

        string dir_log = "";

        public LogDetail() {
            dir_log = GlobalData.detailDirectory;
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.Station);
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.StationID);
            dir_log = string.Format("{0}{1}\\", dir_log, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(dir_log)) Directory.CreateDirectory(dir_log);
        }

        public bool Save(testingInfomation testing) {
            try {
                string ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                string f = String.Format("EW12S_{0}_{1}_{2}.txt", testing.MAC_LABEL_ETHERNET, DateTime.Now.ToString("HHmmss"), testing.TotalResult);

                using (StreamWriter st = new StreamWriter(Path.Combine(dir_log, f), false, Encoding.Unicode)) {
                    st.WriteLine(ver);
                    st.WriteLine("******************************************************************************");
                    st.WriteLine("Setting Information:");
                    st.WriteLine(string.Format("Mac header: {0}", GlobalData.initSetting.MACHeader));
                    st.WriteLine(string.Format("Mã phân biệt dải mac: {0}", GlobalData.initSetting.VnptMacCode));
                    st.WriteLine(string.Format("IP mặc định: {0}", GlobalData.initSetting.APIP));
                    st.WriteLine(string.Format("SSH user: {0}", GlobalData.initSetting.SSHUser));
                    st.WriteLine(string.Format("SSH password: {0}", GlobalData.initSetting.SSHPassword));
                    st.WriteLine(string.Format("Hardware version: {0}", GlobalData.initSetting.HardwareVersion));
                    st.WriteLine(string.Format("Model number: {0}", GlobalData.initSetting.ModelNumber));
                    st.WriteLine(string.Format("Tên firmware thương mại: {0}", GlobalData.initSetting.FirmwareFile));
                    st.WriteLine(string.Format("Build time firmware thương mại: {0}", GlobalData.initSetting.BuildTimeStamp));
                    st.WriteLine(string.Format("MD5 file firmware thương mại: {0}", GlobalData.initSetting.Md5Sum));
                    st.WriteLine(string.Format("IP máy tính: {0}", GlobalData.initSetting.LocalIP));
                    st.WriteLine("******************************************************************************");
                    st.WriteLine(testing.LOGSYSTEM);
                }
                
                return true;
            } catch (Exception ex) {
                testing.LOGSYSTEM += ex.Message + "\r\n";
                return false;
            }
        }

    }
}
