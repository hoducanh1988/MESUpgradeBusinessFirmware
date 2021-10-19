using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using UtilityPack.IO;

using EW30SX.Custom;
using EW30SX.IO;
using EW30SX.Base;
using EW30SX.Function;

namespace EW30SX {


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        /// <summary>
        /// CONSTRUCTOR OF MAINWINDOW --------------------------------------------//
        /// </summary>
        public MainWindow() {
            //Init view
            InitializeComponent();

            //set window location
            this.setStartupLocation();

            //set value for control
            this.loadResourceForControl();

            //Load setting from xml file to software
            this.loadSettingInfoFromXML();

            //set binding value 
            this.tabItem_Setting.DataContext = GlobalData.initSetting;
            this.DataContext = GlobalData.myAppInfo;

            //load user guide
            this.loadUserGuide();

            //load about
            this.loadHistory();

            //load end string
            completeStringFile.readData();
            startUpgradeStringFile.readData();
            ScanSSIDFile.readData();

            //load user control
            this.grid_main.Children.Clear();
            if (GlobalData.initSetting.Station.Contains("ASM-VerifyFirmware")) this.grid_main.Children.Add(new UserControls.ucVerify());
            else this.grid_main.Children.Add(new UserControls.ucUpgrade());
        }



        /// <summary>
        /// CONTROL MOUSEDOWN EVENT OF ALL CONTROL -------------------------------//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameworkElement_MouseDown(object sender, MouseButtonEventArgs e) {
            FrameworkElement element = sender as FrameworkElement;
            string etag = element.Tag.ToString();

            if (e.LeftButton == MouseButtonState.Pressed) {
                switch (etag) {
                    case "viewlog": {
                            string _dir = GlobalData.dir_Path;
                            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
                            Process.Start(_dir);
                            break;
                        }
                    case "dragform": {
                            if (canDragForm == true) this.DragMove();
                            break;
                        }
                    case "minimizeform": {
                            this.WindowState = WindowState.Minimized;
                            break;
                        }
                    case "closeform": {
                            this.Close();
                            break;
                        }
                    case "imapmanagement": {
                            this.Opacity = 0.5;
                            IMAPWindow iwindow = new IMAPWindow();
                            iwindow.ShowDialog();
                            this.Opacity = 1;
                            break;
                        }
                    default: break;
                }
            }
        }

        private void BtnBrowserProductFile_Click(object sender, RoutedEventArgs e) {

        }

        private void Ckb_writemanufacterer_Checked(object sender, RoutedEventArgs e) {
            string data = cbbStation.Text.ToString();
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

        private void Ckb_writemanufacterer_Unchecked(object sender, RoutedEventArgs e) {
            string data = cbbStation.Text.ToString();
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
    }
}
