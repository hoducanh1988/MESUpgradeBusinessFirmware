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

using EW12SG.Custom;
using EW12SG.IO;
using EW12SG.Base;
using EW12SG.Function;
using System.Windows.Xps.Packaging;
using System.Reflection;

namespace EW12SG {
    public partial class MainWindow : Window {

        /// <summary>
        /// LOAD SETTING FROM XML FILE TO APP --------------------//
        /// </summary>
        private void loadSettingInfoFromXML() {
            if (File.Exists(String.Format("{0}Settings.xml", AppDomain.CurrentDomain.BaseDirectory)) == true) {
                GlobalData.initSetting = XmlHelper<initSetting>.FromXmlFile(String.Format("{0}Settings.xml", AppDomain.CurrentDomain.BaseDirectory));
            }
        }


        /// <summary>
        /// SAVE SETTING TO XML FILE -----------------------------//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e) {
            GlobalData.initSetting.Save();
            XmlHelper<initSetting>.ToXmlFile(GlobalData.initSetting, String.Format("{0}Settings.xml", AppDomain.CurrentDomain.BaseDirectory));
            this.grid_main.Children.Clear();
            if (GlobalData.initSetting.Station == "ASM-VerifyFirmware") this.grid_main.Children.Add(new UserControls.ucVerify());
            else this.grid_main.Children.Add(new UserControls.ucUpgrade());

            MessageBox.Show("Đã Lưu Cấu Hình!", "Save Setting", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// BUTTON BROWSER FILE FIRMWARE --------------------------//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowserFirmwareFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\TFTP-Root";

            if (openFile.ShowDialog() == true) {
                GlobalData.initSetting.FirmwareFile = openFile.SafeFileName;
            }
        }


        /// <summary>
        /// SELECT STATION TEST --------------------------------------//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbbStation_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string data = cbbStation.SelectedItem.ToString();
            switch (data) {
                case "ASM-UpGradeFirmware": {
                        this.label_AppTitle.Content = "Upgrade Firmware thương mại";
                        break;
                    }
                case "ASM-VerifyFirmware": {
                        //if (ckb_writemanufacterer.IsChecked == true) {
                        //    this.label_AppTitle.Content = "Verify Firmware thương mại - chỉ ghi manufacterer";
                        //}
                        //else {
                            this.label_AppTitle.Content = "Verify Firmware thương mại";
                        //}

                        
                        break;
                    }
            }
        }


        /// <summary>
        /// SELECT LOG DIRECTORY ----------------------------------------//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbbLogDir_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string data = cbbLogDir.SelectedItem.ToString();
            if (data.Equals("Other...")) {
                System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowser.SelectedPath = "C:\\";
                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    string dir = folderBrowser.SelectedPath;
                    myParams.Directorys.Add(dir);
                    GlobalData.initSetting.LogDirectory = folderBrowser.SelectedPath;
                }
            }
        }

    }
}
