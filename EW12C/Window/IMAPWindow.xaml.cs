using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using EW12C.UserControls;
using EW12C.Custom;
using EW12C;
using EW12C.Base;
using System.Windows.Threading;

namespace EW12C {
    /// <summary>
    /// Interaction logic for IMAPWindow.xaml
    /// </summary>
    public partial class IMAPWindow : Window {
        class windowinfo : INotifyPropertyChanged {
            //implement interface
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName = null) {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public windowinfo() {
                ipstart = GlobalData.initSetting.StartNewAPIP;
                ipend = BaseFunction.addIPAddress(ipstart, 7);
                pingtimeout = 100;
                pingretry = 1;
                showallimap = true;

                progressmax = 1;
                progressvalue = 0;
                progresscontent = "--";
            }

            string _ipstart;
            public string ipstart {
                get { return _ipstart; }
                set {
                    _ipstart = value;
                    OnPropertyChanged(nameof(ipstart));
                }
            }
            string _ipend;
            public string ipend {
                get { return _ipend; }
                set {
                    _ipend = value;
                    OnPropertyChanged(nameof(ipend));
                }
            }
            int _pingtimeout;
            public int pingtimeout {
                get { return _pingtimeout; }
                set {
                    _pingtimeout = value;
                    OnPropertyChanged(nameof(pingtimeout));
                }
            }
            int _pingretry;
            public int pingretry {
                get { return _pingretry; }
                set {
                    _pingretry = value;
                    OnPropertyChanged(nameof(pingretry));
                }
            }
            bool _showallimap;
            public bool showallimap {
                get { return _showallimap; }
                set {
                    _showallimap = value;
                    OnPropertyChanged(nameof(showallimap));
                }
            }

            string _progresscontent;
            public string progresscontent {
                get { return _progresscontent; }
                set {
                    _progresscontent = value;
                    OnPropertyChanged(nameof(progresscontent));
                }
            }
            int _progressmax;
            public int progressmax {
                get { return _progressmax; }
                set {
                    _progressmax = value;
                    progresscontent = string.Format("{0} / {1}", progressvalue, progressmax);
                    OnPropertyChanged(nameof(progressmax));
                }
            }
            int _progressvalue;
            public int progressvalue {
                get { return _progressvalue; }
                set {
                    _progressvalue = value;
                    progresscontent = string.Format("{0} / {1}", progressvalue, progressmax);
                    OnPropertyChanged(nameof(progressvalue));
                }
            }

        }

        windowinfo wbinding = new windowinfo();
        bool scrollflag = false;

        public IMAPWindow() {
            InitializeComponent();
            this.DataContext = wbinding;

            viewIMAP.Items.Clear();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (sender, e) => {
                if (scrollflag == true) { _scrollview.ScrollToEnd(); }
            };
            timer.Start();

        }

        private void Close(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        private void Move(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string etag = b.Tag.ToString();
            b.IsEnabled = false;

            switch (etag) {
                case "buttonping": {
                        if (BaseFunction.isIPAddress(wbinding.ipstart) == true
                            && BaseFunction.isIPAddress(wbinding.ipend) == true
                            && BaseFunction.compare2IPAddress(wbinding.ipend, wbinding.ipstart) >= 0) {
                            int diff = BaseFunction.Subtract2IPAddress(wbinding.ipstart, wbinding.ipend);

                            wbinding.progressmax = diff + 1;
                            wbinding.progressvalue = 0;

                            viewIMAP.Items.Clear();
                            Thread t = new Thread(new ThreadStart(() => {
                                scrollflag = true;

                                //ping to ip
                                for (int i = 0; i <= diff; i++) {
                                    string ip_curr = BaseFunction.addIPAddress(wbinding.ipstart, i);
                                    bool r = BaseFunction.pingToIPAddress(ip_curr, wbinding.pingtimeout, wbinding.pingretry);
                                    if (r == true || (r == false && wbinding.showallimap == true)) {
                                        Dispatcher.Invoke(new Action(() => {
                                            var imapInfo = new IMapInformation() { ip = ip_curr, isconnected = r };
                                            var imap = new ImapControl(imapInfo);
                                            viewIMAP.Items.Add(imap);
                                        }));
                                    }
                                    wbinding.progressvalue++;
                                }
                                Dispatcher.Invoke(new Action(() => { b.IsEnabled = true; }));

                                scrollflag = false;
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        else {
                            b.IsEnabled = true;
                        }
                        break;
                    }
                default: break;
            }
        }
    }
}
