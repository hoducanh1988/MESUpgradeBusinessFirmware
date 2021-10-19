using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EW12CG;

namespace EW12CG.IO
{
    public class completeStringFile
    {

        public static bool readData() {
            string file = string.Format("{0}CompleteString.cfg", AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(file)) {
                string[] lines = File.ReadAllLines(file);
                if (lines == null || lines.Length == 0) return false;

                GlobalData.completeStrings = new List<string>();
                foreach (var line in lines) {
                    if (line != null && line.Trim().Length > 0) GlobalData.completeStrings.Add(line);
                }
                return true;
            }
            else return false;
        }

    }
}
