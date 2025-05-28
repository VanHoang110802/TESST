using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEST.BLL;
using TEST.DTO;

namespace TEST.GUI
{
    public partial class NhanVien_GUI : Form
    {
        private NhanVien_BLL nv_BLL = new NhanVien_BLL();

        public NhanVien_GUI()
        {
            InitializeComponent();
        }



        private void NhanVien_GUI_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView_ThongTin.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chỉ chọn dòng
            dataGridView_ThongTin.ReadOnly = true;  // Vô hiệu hóa chỉnh sửa
            dataGridView_ThongTin.MultiSelect = false; // Không cho phép chọn nhiều dòng
            dataGridView_ThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Tự động căn chỉnh kích thước

            try
            {
                DataTable dataTable = nv_BLL.LayRaNhanVien();
                dataGridView_ThongTin.DataSource = dataTable;  // Dùng dataGridView thay cho listView vì dataGridView sẽ có hỗ trợ nhiều thứ hơn về CSDL

                dataGridView_ThongTin.Columns["MaNV"].HeaderText = "ID";
                dataGridView_ThongTin.Columns["HoTen"].HeaderText = "Họ tên nhân viên";
                dataGridView_ThongTin.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dataGridView_ThongTin.Columns["GioiTinh"].HeaderText = "Giới tính";
                dataGridView_ThongTin.Columns["Luong"].HeaderText = "Lương";

                dataGridView_ThongTin.Columns["MaNV"].Width = 60;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }



        private void ClearForm()
        {
            textBox_NhapHoTen.Clear();
            radioButton_ChonNam.Checked = false;
            radioButton_ChonNu.Checked = false;
            dateTimePicker_ChonNamSinh.Value = DateTime.Today;
            textBox_NhapLuong.Clear();
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ giao diện
                NhanVien_DTO nv_dto = new NhanVien_DTO
                {
                    HoTen = textBox_NhapHoTen.Text.Trim(),
                    NgaySinh = dateTimePicker_ChonNamSinh.Value,
                    GioiTinh = radioButton_ChonNam.Checked ? "Nam" : radioButton_ChonNu.Checked ? "Nữ" : "",
                    Luong = decimal.TryParse(textBox_NhapLuong.Text, out decimal luong) ? luong : -1
                };

                // Hiển thị thông báo
                NhanVien_BLL bll = new NhanVien_BLL();
                var result = bll.XuLyThemNhanVien(nv_dto);
                MessageBox.Show(result.message);

                // Tải lại danh sách nếu thành công
                if (result.isSuccess)
                {
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


  
        private void button_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView_ThongTin.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            int employeeId = Convert.ToInt32(dataGridView_ThongTin.SelectedRows[0].Cells["MaNV"].Value);

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool success = nv_BLL.XuLyXoaNhanVien(employeeId);
                if (success)
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }




        private void button_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button_Sua_Click(object sender, EventArgs e)
        {

        }

        private void button_TimKiem_Click(object sender, EventArgs e)
        {
            TimKiem_GUI formSearch = new TimKiem_GUI();
            formSearch.ShowDialog(); // Mở form tìm kiếm dưới dạng dialog
        }
    }
}
