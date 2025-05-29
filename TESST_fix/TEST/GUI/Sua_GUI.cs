using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEST.DAL;
using TEST.DTO;

namespace TEST.GUI
{
    public partial class Sua_GUI : Form
    {
        private NhanVien_DTO nhanVien;
        private NhanVien_DAL dal = new NhanVien_DAL();

        public Sua_GUI(NhanVien_DTO nv)
        {
            InitializeComponent();
            nhanVien = nv;
            LoadNhanVien();
        }

        private void LoadNhanVien()
        {
            textBox_SuaNhapHoTen.Text = nhanVien.HoTen;
            dateTimePicker_SuaChonNamSinh.Value = nhanVien.NgaySinh;
            if (nhanVien.GioiTinh == "Nam")
            {
                radioButton_SuaChonNam.Checked = true;
            }
            else if (nhanVien.GioiTinh == "Nữ")
            {
                radioButton_SuaChonNu.Checked = true;
            }

            textBox_SuaNhapLuong.Text = nhanVien.Luong.ToString();
        }

        private void Sua_GUI_Load(object sender, EventArgs e)
        {
            //LoadNhanVien();    // Hiển thị thông tin nhân viên
        }

        private void button_SuaThongTin_Click(object sender, EventArgs e)
        {
            string hoTenMoi = textBox_SuaNhapHoTen.Text;
            DateTime ngaySinhMoi = dateTimePicker_SuaChonNamSinh.Value;
            string gioiTinhMoi = radioButton_SuaChonNam.Checked ? "Nam" : "Nữ";
            decimal luongMoi;

            if (!decimal.TryParse(textBox_SuaNhapLuong.Text, out luongMoi))
            {
                MessageBox.Show("Lương không hợp lệ. Vui lòng nhập lại!");
                return;
            }

            // Kiểm tra xem có thay đổi nào không
            if (nhanVien.HoTen == hoTenMoi && nhanVien.NgaySinh == ngaySinhMoi && nhanVien.GioiTinh == gioiTinhMoi && nhanVien.Luong == luongMoi)
            {
                MessageBox.Show("Không có thay đổi nào được thực hiện!");
                this.Close();
                return;
            }

            // Cập nhật thông tin vào DTO
            nhanVien.HoTen = hoTenMoi;
            nhanVien.NgaySinh = ngaySinhMoi;
            nhanVien.GioiTinh = gioiTinhMoi;
            nhanVien.Luong = luongMoi;

            // Gọi phương thức cập nhật trong DAL
            bool kq = dal.UpdateNhanVien(nhanVien);
            if (kq)
            {
                MessageBox.Show("Cập nhật thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void button_QuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
