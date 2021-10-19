using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace EW30SX.IO {

    public class SmbClient {


        public bool IsDirectoryExist(string folder_path) {
            return Directory.Exists(folder_path);
        }

        public bool IsFileExist(string file_full_name) {
            return File.Exists(file_full_name);
        }

        public string GetHash<T>(string filename) where T : HashAlgorithm {
            using (FileStream fStream = File.OpenRead(filename)) {
                return GetHash<T>(fStream);
            }
        }

        private string GetHash<T>(Stream stream) where T : HashAlgorithm {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null)) {
                byte[] hashBytes = crypt.ComputeHash(stream);
                foreach (byte bt in hashBytes) {
                    sb.Append(bt.ToString("x2"));
                }
            }
            return sb.ToString();
        }

    }
}
