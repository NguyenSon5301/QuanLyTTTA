using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BigProject
{
    public partial class ThemGiaoVien : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public ThemGiaoVien()
        {
            InitializeComponent();
        }
        // Tạo sự kiện Load dữ liệu Giáo Viên
        private void ThemGiaoVien_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            string sql = "select top 1 (IDGV) + 1 as 'IDGV' from GiaoVien order by IDGV DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr.Read())
            {
                string idgv = (string)dr["IDGV"].ToString();
                IDGV.Text = idgv;
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
        // Tạo sự kiện Click  nút Thêm giáo viên
        private void ThemGV_Click(object sender, EventArgs e)
        {
            // Các ràng buộc không cho phép để trống
            if (HTGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!");
                HTGV.Focus();
            }
            else if (NSGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày sinh!!!");
                NSGV.Focus();
            }
            else if (SDTGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!");
                SDTGV.Focus();
            }
            else if (SDTGV.Text.Length != 10) // Nhập đúng độ dài số điện thoại bằng 10
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại!!!");
                SDTGV.Focus();
            }
            else if (DCGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!!!");
                DCGV.Focus();
            }
            else if (EmailGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Email!!!");
                EmailGV.Focus();
            }
            else if (!EmailGV.Text.Contains("@gmail.com")) // Nhập phải có đuôi @gmail.com
            {
                MessageBox.Show("Vui lòng nhập đuôi @gmail.com");
                EmailGV.Focus();
            }

            else
            {
                if (MessageBox.Show("Bạn có muốn thêm giáo viên?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Thông báo xác nhận thêm
                {
                    conn.Open();
                    // Câu lệnh truy vấn
                    SqlCommand cmd1 = new SqlCommand("Insert GiaoVien(HTGV,NgaySinh,SDTGV,DCGV,EMAILGV) Values(@HTGV,@NgaySinh,@SDTGV,@DCGV,@EMAILGV)", conn); // Tạo đối tượng
                    cmd1.Parameters.AddWithValue("@HTGV", HTGV.Text); // Thiết lập tham số 
                    DateTime ngaysinh = DateTime.ParseExact(NSGV.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); // Sử dụng chuyển đổi kiểu
                    cmd1.Parameters.AddWithValue("@NgaySinh", ngaysinh);  
                    cmd1.Parameters.AddWithValue("@SDTGV", SDTGV.Text);
                    cmd1.Parameters.AddWithValue("@DCGV", DCGV.Text);
                    cmd1.Parameters.AddWithValue("@EMAILGV", EmailGV.Text);
                    cmd1.ExecuteNonQuery(); // Thi hành truy vân
                    cmd1.Parameters.Clear();
                    conn.Close();
                    this.Close();
                }
            }
        }
    }
}
