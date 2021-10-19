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

        /// <summary>
        /// LOAD RESOURCE FOR CONTROL -----------------------//
        /// </summary>
        void loadResourceForControl() {
            this.cbbLogDir.ItemsSource = myParams.Directorys;
            this.cbbStation.ItemsSource = myParams.Stations;
            this.cbbStationID.ItemsSource = myParams.StationIDs;
            this.cbb_manufacturer.ItemsSource = myParams.Manufacturers;
        }

    }
}
