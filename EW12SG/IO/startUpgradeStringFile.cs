using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW12SG.IO {
    public class startUpgradeStringFile {

        public static bool readData() {
            string file = string.Format("{0}StartUpgradeString.cfg", AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(file)) {
                string[] lines = File.ReadAllLines(file);
                if (lines == null || lines.Length == 0) return false;

                GlobalData.startUpgradeStrings = new List<string>();
                foreach (var line in lines) {
                    if (line != null && line.Trim().Length > 0) GlobalData.startUpgradeStrings.Add(line);
                }
                return true;
            }
            else return false;
        }
    }
}
