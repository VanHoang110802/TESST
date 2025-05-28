using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEST.DAL;
using TEST.DTO;

namespace TEST.BLL
{
    internal class NhanVien_BLL
    {
        private NhanVien_DAL nv_DAL = new NhanVien_DAL();

        public DataTable LayRaNhanVien()
        {
            return nv_DAL.LoadCSDL();
        }




        public (bool CheckVal, string Message) XacThucThemNhanVien(NhanVien_DTO nv)
        {
            // Kiểm tra dữ liệu trước khi thêm
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                return (false, "Họ tên nhân viên không được để trống!");

            if (string.IsNullOrWhiteSpace(nv.GioiTinh))
                return (false, "Giới tính không được để trống!");

            if (nv.NgaySinh == default(DateTime))
                return (false, "Ngày sinh không được để trống hoặc không hợp lệ!");

            DateTime today = DateTime.Today;
            if (nv.NgaySinh > today)
                return (false, "Ngày sinh không thể lớn hơn ngày hiện tại!");

            if (nv.Luong <= 0)
                return (false, "Lương không hợp lê hoặc lương bắt buộc phải là số dương!");

            return (true, "Dữ liệu hợp lệ.");
        }

        public (bool isSuccess, string message) XuLyThemNhanVien(NhanVien_DTO nv)
        {
            var validation = XacThucThemNhanVien(nv);
            if (!validation.CheckVal)
                return (false, validation.Message);

            // Gọi DAL thêm nhân viên vào database
            bool success = nv_DAL.ThemNhanVien(nv);

            if (success)
                return (true, "Thêm nhân viên thành công!");
            else
                return (false, "Lỗi khi thêm nhân viên vào database.");
        }


        public bool XuLyXoaNhanVien(int employeeId)
        {
            // Có thể thêm kiểm tra nghiệp vụ trước khi gọi DAL (vd: kiểm tra quyền xóa)
            return nv_DAL.XoaNhanVien(employeeId);
        }

    }
}
