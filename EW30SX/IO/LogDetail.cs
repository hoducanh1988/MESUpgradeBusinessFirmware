using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityPack.IO;

using EW30SX.Custom;
using System.Diagnostics;
using System.Reflection;
using EW30SX.Base;

namespace EW30SX.IO {
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
                string log_data = "";
                BaseFunction.getAppInfo(ref log_data);
                BaseFunction.getSettingInfo(ref log_data);

                string f = String.Format("EW30SX_{0}_{1}_{2}.txt", testing.MAC_LABEL_ETHERNET, DateTime.Now.ToString("HHmmss"), testing.TotalResult);

                using (StreamWriter st = new StreamWriter(Path.Combine(dir_log, f), false, Encoding.Unicode)) {
                    st.WriteLine("Product: EW30SX");
                    st.WriteLine(log_data);
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
