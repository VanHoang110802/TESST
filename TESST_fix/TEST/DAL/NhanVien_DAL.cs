using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEST.DTO;

namespace TEST.DAL
{
    internal class NhanVien_DAL
    {
        private string connectionString = @"Server=MAY-14\SQLEXPRESS;Database=QLNhanVien;Trusted_Connection=True;";

        public DataTable LoadCSDL()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM NhanVien";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }



        public bool ThemNhanVien(NhanVien_DTO nv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, Luong) VALUES (@HoTen, @NgaySinh, @GioiTinh, @Luong)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@HoTen", nv.HoTen);
                command.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                command.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                command.Parameters.AddWithValue("@Luong", nv.Luong);

                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0) return true; // Trả về true nếu thêm thành công
                return false;
            }
        }



        public bool XoaNhanVien(int employeeId)
        {
            string query = "DELETE FROM NhanVien WHERE MaNV = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }



        public List<NhanVien_DTO> GetAllNhanVien()
        {
            List<NhanVien_DTO> list = new List<NhanVien_DTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT HoTen, NgaySinh, GioiTinh, Luong FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var nv = new NhanVien_DTO
                    {
                        MaNV = (int)dr["MaNV"],
                        HoTen = dr["HoTen"].ToString(),
                        NgaySinh = Convert.ToDateTime(dr["NgaySinh"]),
                        GioiTinh = dr["GioiTinh"].ToString(),
                        Luong = Convert.ToDecimal(dr["Luong"])
                    };
                    list.Add(nv);
                }
            }
            return list;
        }

        public bool UpdateNhanVien(NhanVien_DTO nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NhanVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, Luong=@Luong WHERE MaNV=@MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@Luong", nv.Luong);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

    }
}
