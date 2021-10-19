using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW12CG.IO {

    public class ScanSSIDFile {

        public static bool readData() {
            string file = string.Format("{0}ScanSSID.cfg", AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(file)) {
                string[] lines = File.ReadAllLines(file);
                if (lines == null || lines.Length == 0) return false;

                GlobalData.FlagScanSSID = lines[0];
                return true;
            }
            else return false;
        }

    }

}
