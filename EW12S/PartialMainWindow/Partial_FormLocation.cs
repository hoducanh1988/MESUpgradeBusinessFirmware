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

using EW12S.Custom;
using EW12S.IO;
using EW12S.Base;
using EW12S.Function;

namespace EW12S {


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private void setStartupLocation() {
            double h = SystemParameters.WorkArea.Height;
            double w = SystemParameters.WorkArea.Width;

            //if (w < 1300) {
            double scaleX = 1;
            double scaleY = 1;
            this.Height = SystemParameters.WorkArea.Height * scaleY - 10;
            this.Width = SystemParameters.WorkArea.Width * scaleX - 10;
            this.Top = (SystemParameters.WorkArea.Height * (1 - scaleY)) / 2;
            this.Left = (SystemParameters.WorkArea.Width * (1 - scaleX)) / 2;
            //canDragForm = false;
            //} else {
            //    this.Height = 850;
            //    this.Width = 1360;
            //    this.Top = (SystemParameters.WorkArea.Height - 850) / 2;
            //    this.Left = (SystemParameters.WorkArea.Width - 1360) / 2;
                canDragForm = true;
            //}
        }

    }
}
