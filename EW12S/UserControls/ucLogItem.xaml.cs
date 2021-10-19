﻿using EW12S.Custom;
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

namespace EW12S.UserControls
{
    /// <summary>
    /// Interaction logic for ucLogItem.xaml
    /// </summary>
    public partial class ucLogItem : UserControl
    {
        public ucLogItem(testingInfomation testinfo)
        {
            InitializeComponent();
            this.DataContext = testinfo;


        }
    }
}
