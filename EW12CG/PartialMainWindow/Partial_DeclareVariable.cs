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

using EW12CG.Custom;
using EW12CG.IO;
using EW12CG.Base;
using EW12CG.Function;
using System.Windows.Xps.Packaging;
using System.Reflection;

namespace EW12CG {
    public partial class MainWindow : Window {

        //DECLARE VARIABLE -----------------------------------------------------//
        bool canDragForm = false;
        List<history> listHist = null;


        //DECLARE CLASS --------------------------------------------------------//
        public class history {
            public string ID { get; set; }
            public string VERSION { get; set; }
            public string CONTENT { get; set; }
            public string DATE { get; set; }
            public string CHANGETYPE { get; set; }
            public string PERSON { get; set; }
        }

    }
}
