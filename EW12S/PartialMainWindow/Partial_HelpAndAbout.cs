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

using EW12S.Custom;
using EW12S.IO;
using EW12S.Base;
using EW12S.Function;
using System.Windows.Xps.Packaging;
using System.Reflection;

namespace EW12S {
    public partial class MainWindow : Window {

        void loadHistory() {
            listHist = new List<history>();
            listHist.Add(new history() {
                ID = "1",
                VERSION = "1.0.0.0",
                CONTENT = "- Phát hành lần đầu",
                DATE = "15/01/2019",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh, Trần Đức Hòa"
            });

            listHist.Add(new history() {
                ID = "2",
                VERSION = "1.0.0.1",
                CONTENT = "- Thay đổi thời gian timeout khi truyền dữ liệu file firmware từ PC xuống IMAP: cũ 20s => mới 60s.",
                DATE = "17/05/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "3",
                VERSION = "1.0.0.2",
                CONTENT = "- Cho phép cấu hình giá trị thời gian timeout chuyển file firmware từ PC xuống sản phẩm trên phần mềm.",
                DATE = "21/05/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "4",
                VERSION = "1.0.0.3",
                CONTENT = "- Gửi thêm file script upfw.sh xuống AP để fix lỗi đèn cam (file upfw.sh copy vào folder C:\\TFTP-Root).\n" + 
                          "- Thay đổi thời gian timeout khi chờ ping tới  cổng LAN AP: cũ 20s => mới 60s.",
                DATE = "30/05/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "5",
                VERSION = "1.0.0.4",
                CONTENT = "- Sửa tham số cài đặt wifi connection timeout từ ms => sec.\n" +
                          "- Sửa giá trị cài đặt mặc định TimeoutTransFWFile từ 180s => 360s.",
                DATE = "12/08/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "6",
                VERSION = "1.0.0.5",
                CONTENT = "- Update cho phép cài đặt timeout : check md5, transfer script, upgrade firmware ở trạm upgrade firmware.\n" + 
                          "- Update chức năng check serial number, hardware version, model number trạm verify firmware (đang phát triển).",
                DATE = "21/08/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "7",
                VERSION = "1.0.0.6",
                CONTENT = "- Chỉnh sửa thay đổi bắt chuỗi lệnh kết thúc nạp firmware thương mại từ file CompleteString.cfg,StartUpgradeString.cfg => fix lỗi firmware mekong net.\n" + 
                          "- Chỉnh sửa giới hạn số kí tự log detail của mỗi sản phẩm là 1000 kí tự => fix lỗi đơ phần mềm khi chạy nhiều sản phẩm cùng 1 lúc.\n" + 
                          "- Thêm biến LOGDETAIL để hiển thị lên giao diện, LOGSYSTEM để lưu log => fix lỗi lưu log detail ko đầy đủ.",
                DATE = "18/10/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "8",
                VERSION = "1.0.0.7",
                CONTENT = "- Cập nhật tool cho định dạng số serial number mới thay đổi mã màu sang mã phân biệt dải mac.",
                DATE = "25/12/2019",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "9",
                VERSION = "1.0.0.8",
                CONTENT = "- Cập nhật tool retry nạp lại firmware để fix lỗi đèn cam (chỉ áp dụng nạp FW Basic mới -> FW TM).\r\n" + 
                          "- Thay đổi flow verify firmware TM sau upgrade (chuyển lệnh chmod script test lên trước khi nạp FW)",
                DATE = "17/03/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "10",
                VERSION = "1.0.0.9",
                CONTENT = "- Cập nhật tool thay đổi đường dẫn file upfw.sh từ /sbin/ sang /tmp/.\r\n" + 
                          "- Cập nhật tool check md5 sum của file script fix lỗi đèn cam sau khi tải xuống sản phẩm.\r\n" +
                          "- Cập nhật tool lưu log SSH cho từng sản phẩm.",
                DATE = "24/03/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "11",
                VERSION = "1.0.1.0",
                CONTENT = "- Cập nhật tool check lỗi đèn cam sau khi sản phẩm khởi động lại bằng phương pháp scan SSID Name kết hợp với phần mềm NetSpot.\r\n" +
                          "- Cập nhật thêm chức năng check địa chỉ mac sản phẩm trước khi upgrade FW TM.\r\n" +
                          "- Cập nhật thêm chức năng check firmware build time đúng firmware basic mới trước khi upgrade.",
                DATE = "31/03/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });


            listHist.Add(new history() {
                ID = "12",
                VERSION = "1.0.1.1",
                CONTENT = "- Cập nhật tool sử dụng giải pháp đổi IP qua cổng LAN.\r\n" +
                          "- Cập nhật tool giảm số lượng multi nạp FW từ 8 về 4.\r\n" +
                          "- Cập nhật tool thêm chức năng check ping sản phẩm sau upgrade FW bằng AP DHCP Server và kết hợp scan SSID.\r\n" +
                          "- Cập nhật tool check run phần mềm tftpd64/tftpd32.\r\n" +
                          "- Cập nhật tool check file tồn tại và md5 file script upfw.sh, file script verify_flash_and_upgrade.sh và file firmware thương mại.",
                DATE = "06/04/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "13",
                VERSION = "1.0.1.2",
                CONTENT = "- Cập nhật tool nạp firmware basic lên thương mại từ 4 => lên thành 8 sản phẩm cùng lúc.\r\n" +
                          "- Cập nhật tool nạp firmware không theo mẻ mà theo từng sản phẩm.",
                DATE = "20/05/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "14",
                VERSION = "1.0.1.3",
                CONTENT = "- Tối ưu thời gian nạp firmware\n." +
                          "- Cập nhật chức năng nạp firmware từ thương mại lên thương mại.",
                DATE = "03/09/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "15",
                VERSION = "1.0.1.4",
                CONTENT = "- Bỏ chức năng check firmware build time trước khi nạp firmware thương mại.",
                DATE = "23/09/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "16",
                VERSION = "1.0.1.5",
                CONTENT = "- Update thêm chức năng ghi mã manufacturer (VNPTT hoặc VNPTT-VNPT) tại trạm nạp FW TM.\n" + 
                          "- Update thêm chức năng ghi hoặc check mã manufacturer tại trạm verify FW TM.",
                DATE = "15/10/2020",
                CHANGETYPE = "Chỉnh Sửa",
                PERSON = "Hồ Đức Anh"
            });
            listHist.Add(new history() {
                ID = "17",
                VERSION = "1.0.1.6",
                CONTENT = "- Update tool tương thích với mật khẩu firmware thương mại \"VNPT@88Tech\".\n" + 
                          "- Cho phép chọn ghi hoặc không ghi mã khách hàng.",
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
