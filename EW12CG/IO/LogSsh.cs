using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EW12CG.Custom;
using System.Diagnostics;
using System.Reflection;

namespace EW12CG.IO {
    public class LogSsh {

        string dir_log = "";

        public LogSsh() {
            dir_log = GlobalData.sshDirectory;
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.Station);
            dir_log = string.Format("{0}{1}\\", dir_log, GlobalData.initSetting.StationID);
            dir_log = string.Format("{0}{1}\\", dir_log, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(dir_log)) Directory.CreateDirectory(dir_log);
        }


        /// <summary>
        /// SAVE LOG SSH -----------------------//
        /// </summary>
        /// <param name="testing"></param>
        /// <returns></returns>
        public bool Save(testingInfomation testing) {
            try {
                string ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                string f = String.Format("EW12CG_{0}_{1}_{2}.txt", testing.MAC_LABEL_ETHERNET, DateTime.Now.ToString("HHmmss"), testing.TotalResult);
                using (StreamWriter st = new StreamWriter(Path.Combine(dir_log, f), false, Encoding.Unicode)) {
                    st.WriteLine(ver);
                    st.WriteLine(testing.LOGSSH_FOR_SAVE);
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
