using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using EW12CG.Base;
using EW12CG.Function;
using EW12CG.IO;
using EW12CG.Custom;

namespace EW12CG.UserControls {
    /// <summary>
    /// Interaction logic for ucVerify.xaml
    /// </summary>
    public partial class ucVerify : UserControl {

        volatile bool[] ScrollFlags = new bool[8] { false, false, false, false, false, false, false, false };
        DispatcherTimer timer = null;

        public ucVerify() {
            InitializeComponent();
            setBindingValueForControl();
            initControlScrollView();
        }

        /// <summary>
        /// SET BINDING VALUE FOR CONTROL -------------------//
        /// </summary>
        void setBindingValueForControl() {

            this.Group1.DataContext = GlobalData.testingInfo1;
            this.Group2.DataContext = GlobalData.testingInfo2;
            this.Group3.DataContext = GlobalData.testingInfo3;
            this.Group4.DataContext = GlobalData.testingInfo4;
            this.Group5.DataContext = GlobalData.testingInfo5;
            this.Group6.DataContext = GlobalData.testingInfo6;
            this.Group7.DataContext = GlobalData.testingInfo7;
            this.Group8.DataContext = GlobalData.testingInfo8;
            
        }


        /// <summary>
        /// INIT CONTROL FOR SCROLL VIEW --------------------//
        /// </summary>
        void initControlScrollView() {

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (sender, e) => {
                if (ScrollFlags[0] == true) { Scr_LogSYSTEM1.ScrollToEnd(); }
                if (ScrollFlags[1] == true) { Scr_LogSYSTEM2.ScrollToEnd(); }
                if (ScrollFlags[2] == true) { Scr_LogSYSTEM3.ScrollToEnd(); }
                if (ScrollFlags[3] == true) { Scr_LogSYSTEM4.ScrollToEnd(); }
                if (ScrollFlags[4] == true) { Scr_LogSYSTEM5.ScrollToEnd(); }
                if (ScrollFlags[5] == true) { Scr_LogSYSTEM6.ScrollToEnd(); }
                if (ScrollFlags[6] == true) { Scr_LogSYSTEM7.ScrollToEnd(); }
                if (ScrollFlags[7] == true) { Scr_LogSYSTEM8.ScrollToEnd(); }
            };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            int jig_id = int.Parse(b.Tag.ToString());

            testingInfomation testtmp = null;
            BaseFunction.get_Testing_Info_By_JIG(jig_id, ref testtmp);
            testtmp.InitValue();

            this.Opacity = 0.5;
            InputMacAddress inputMac = new InputMacAddress(jig_id, testtmp);
            inputMac.ShowDialog();
            this.Opacity = 1;

            if (testtmp.MAC_LABEL_ETHERNET == "--") return; //ko nhap mac

            Thread t = new Thread(new ThreadStart(() => {
                //start scroll
                ScrollFlags[jig_id - 1] = true;

                //start measure time
                Stopwatch st = new Stopwatch();
                st.Start();

                //display state
                testtmp.TotalResult = GlobalData.initSetting.Station == "ASM-VerifyFirmware" ? "Verifying..." : "Upgrading...";
                testtmp.LOGSYSTEM += "Waiting other DUTs...\r\n";

                //verify / upgrade firmware
                bool r = GlobalData.initSetting.Station == "ASM-VerifyFirmware" ? VerifyFirmware.Excute(testtmp) : new UpgradeFirmware().Excute(testtmp);

                testtmp.TotalResult = r == true ? "PASS" : "FAIL";

                //end measure time
                st.Stop();

                //save log
                bool s = false;
                testtmp.LOGSYSTEM += string.Format("------------------------------------------------\r\n");
                testtmp.LOGSYSTEM += string.Format("Save log data...\r\n");
                testtmp.LOGSYSTEM += string.Format("...Directory: {0}\r\n",
                                     GlobalData.initSetting.LogDirectory.Equals("Default") == true ? AppDomain.CurrentDomain.BaseDirectory : GlobalData.initSetting.LogDirectory);

                s = new LogTotal(jig_id).Save(testtmp); //total
                testtmp.LOGSYSTEM += string.Format("...Log total, result = {0}\r\n", s == true ? "PASS" : "FAIL");

                //show result test
                testtmp.LOGSYSTEM += string.Format("---------------------------------------------------[End]\r\n");
                testtmp.LOGSYSTEM += string.Format("\r\nTotal result: {0}\r\n", testtmp.TotalResult);
                testtmp.LOGSYSTEM += string.Format("Total time: {0} ms\r\n", st.ElapsedMilliseconds);
                testtmp.LOGSYSTEM += string.Format("Error message: {0}\r\n\r\n", testtmp.ErrorMessage);

                //stop scroll
                ScrollFlags[jig_id - 1] = false;

                //log detail
                s = new LogDetail().Save(testtmp);

                //log ssh
                s = new LogSsh().Save(testtmp);

            }));
            t.IsBackground = true;
            t.Start();
        }
    }


    /// <summary>
    /// CONVERT STRING TO STRING -------------------------//
    /// </summary>
    public class StringToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch (value.ToString().ToLower()) {
                case "verifying...":
                case "upgrading...":
                    return "STOP";
                default:
                    return "START";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }


    /// <summary>
    /// CONVERT STRING TO BOOL -----------------------------//
    /// </summary>
    public class StringToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch (value.ToString().ToLower()) {
                case "verifying...":
                case "upgrading...":
                    return false;
                default:
                    return true;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }

}
