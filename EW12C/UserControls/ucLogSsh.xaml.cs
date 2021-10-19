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
using System.Windows.Threading;
using EW12C.Custom;

namespace EW12C.UserControls
{
    /// <summary>
    /// Interaction logic for ucLogSsh.xaml
    /// </summary>
    public partial class ucLogSsh : UserControl
    {
        DispatcherTimer timer = null;

        public ucLogSsh(testingInfomation testinfo)
        {
            InitializeComponent();
            this.DataContext = testinfo;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (sender, e) => {
                if (testinfo.scrollFlag == true) { Scr_LogSSH.ScrollToEnd(); }
            };
            timer.Start();
        }
    }
}
