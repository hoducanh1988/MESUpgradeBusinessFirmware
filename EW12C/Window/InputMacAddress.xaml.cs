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
using System.Windows.Shapes;

using System.Text.RegularExpressions;
using EW12C.Custom;

namespace EW12C {
    /// <summary>
    /// Interaction logic for InputMacAddress.xaml
    /// </summary>
    public partial class InputMacAddress : Window {

        testingInfomation test = null;

        public InputMacAddress(int id, testingInfomation _test) {
            InitializeComponent();

            //this.Title = string.Format("#DUT {0}", id);
            this.lblDut.Content = string.Format("#IMAP-0{0}", id);
            this.test = _test;

            txtMac.Clear();
            txtMac.Focus();
        }

        private void Close(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        private void Move(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void TxtMac_KeyDown(object sender, KeyEventArgs e) {
            if (txtMac.Text.Length > 1) tbMessage.Text = "";
            if (e.Key == Key.Enter) {
                //check mac address
                if (txtMac.Text.Trim() == "") {
                    tbMessage.Text = "Chưa nhập địa chỉ MAC.\nVui lòng nhập lại.";
                    goto FAIL;
                }
                if (txtMac.Text.Trim().Length != 12) {
                    tbMessage.Text = string.Format("Địa chỉ MAC '{0}' không đủ 12 kí tự.\nVui lòng nhập lại.", txtMac.Text);
                    goto FAIL;
                }
                if (Regex.IsMatch(txtMac.Text.Trim().ToUpper(), "^[A-F,0-9]{12}$") == false) {
                    tbMessage.Text = string.Format("Địa chỉ MAC '{0}' không đúng định dạng A-F,0-9.\nVui lòng nhập lại.", txtMac.Text);
                    goto FAIL;
                }
                if (GlobalData.initSetting.MACHeader.ToUpper().Contains(txtMac.Text.Trim().Substring(0, 6).ToUpper()) == false) {
                    tbMessage.Text = string.Format("MAC header '{0}' không đúng tiêu chuẩn VNPT '{1}'.\nVui lòng nhập lại.", txtMac.Text, GlobalData.initSetting.MACHeader);
                    goto FAIL;
                }

                goto PASS;

            FAIL: {
                    txtMac.Clear();
                    txtMac.Focus();
                    return;
                }
            PASS: {
                    this.test.MAC_LABEL_ETHERNET = txtMac.Text.Trim().ToUpper();
                    this.Close();
                    return;
                }
            }
        }
    }
}
