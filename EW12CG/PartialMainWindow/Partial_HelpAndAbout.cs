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

        void loadHistory() {
            listHist = new List<history>();
            listHist.Add(new history() {
                ID = "1",
                VERSION = "1.0.0.0",
                CONTENT = "- Phát hành lần đầu",
                DATE = "28/04/2020",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "2",
                VERSION = "1.0.0.1",
                CONTENT = "- Cập nhật tool nạp firmware basic lên thương mại từ 4 => lên thành 8 sản phẩm cùng lúc.\r\n" +
                          "- Cập nhật tool nạp firmware không theo mẻ mà theo từng sản phẩm.",
                DATE = "20/05/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "3",
                VERSION = "1.0.0.2",
                CONTENT = "- Tích hợp thêm chức năng nạp firmware thương mại lên thương mại.\n" +
                          "- Bỏ chức năng check thông tin firmware build time trước khi nạp firmware thương mại.\n" +
                          "- Tối ưu giảm thời gian nạp firmware thương mại.",
                DATE = "23/09/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "4",
                VERSION = "1.0.0.3",
                CONTENT = "- Update thêm chức năng ghi mã manufacturer (VNPTT hoặc VNPTT-VNPT) tại trạm nạp FW TM.\n" +
                          "- Update thêm chức năng ghi hoặc check mã manufacturer tại trạm verify FW TM.",
                DATE = "18/08/2021",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            this.GridAbout.ItemsSource = listHist;
        }

        void loadUserGuide() {
            XpsDocument xpsDocument = new XpsDocument(string.Format("{0}UserGuide.xps", System.AppDomain.CurrentDomain.BaseDirectory), System.IO.FileAccess.Read);
            FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
            docViewer.Document = fds;
        }

    }
}
