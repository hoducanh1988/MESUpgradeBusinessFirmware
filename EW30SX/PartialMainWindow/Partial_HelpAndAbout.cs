using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

using UtilityPack.IO;

using EW30SX.Custom;
using EW30SX.IO;
using EW30SX.Base;
using EW30SX.Function;
using System.Windows.Xps.Packaging;
using System.Reflection;

namespace EW30SX {
    public partial class MainWindow : Window {

        void loadHistory() {
            listHist = new List<history>();
            listHist.Add(new history() {
                ID = "1",
                VERSION = "EW30SXVN0U0001",
                CONTENT = "- Phát hành lần đầu",
                DATE = "30/09/2021",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });
            this.GridAbout.ItemsSource = listHist;
        }

        void loadUserGuide() {
            XpsDocument xpsDocument = new XpsDocument(string.Format("{0}UserGuide.xps", System.AppDomain.CurrentDomain.BaseDirectory), System.IO.FileAccess.Read);
            FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
            docViewer.Document = fds;
        }

    }
}
