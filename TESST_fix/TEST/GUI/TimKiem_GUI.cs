using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST.GUI
{
    public partial class TimKiem_GUI : Form
    {
        public TimKiem_GUI()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void button_QuayLai_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form tìm kiếm
        }

        private void button_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
