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
    public partial class DangNhap : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-FO8QLMB;Initial Catalog=QuanLyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public DangNhap()
        {
            InitializeComponent();
        }
        // Tạo sự kiện cho nút Login
        private void DangNhap_Click(object sender, EventArgs e)
        {
            if (txtUser.Text =="") // Điều kiện không để trống nhập ô tài khoản
            {
                MessageBox.Show("Vui lòng nhập tài khoản!!!", "Thông báo!!!");
                txtUser.Focus();
            }
            else if (txtPW.Text =="") // Điều kiện không để trống nhập ô mật khẩu
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!!!", "Thông báo");
                txtPW.Focus();
            }
            else if (comboBox1.SelectedItem != null) //Điệu kiện nếu chưa chọn kiểu đăng nhập
            {
                if (comboBox1.Text == "User") // Nếu chọn User 
                {
                    conn.Open(); // Mở kết nối 
                    string tk = txtUser.Text;
                    string mk = txtPW.Text;
                    string sql = "select * from dbo.UserSV,dbo.InStudent WHERE InStudent.IDHV = UserSV.IDHV and TaiKhoanSV = '" + tk + "' and MatKhauSV = '" + mk + "'"; // Tạo câu lệnh truy vấn
                    // Câu lệnh thực hiện truy vấn
                    SqlCommand cmd = new SqlCommand(sql, conn); 
                    SqlDataReader dta = cmd.ExecuteReader(); 
                    // Đọc kết quả truy vấn
                    if (dta.Read() == true) // Nếu tài khoản, mật khẩu đúng thì làm tiếp còn sai thì trả ra else
                    {
                        string malop = (string)dta["MaLop"].ToString();
                        string idhv = (string)dta["IDHV"].ToString();
                        new HomeUser(idhv,malop).Show();
                        conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                        this.Hide();
                    }
                    else 
                    {
                        MessageBox.Show("Sai mật khẩu hoặc tài khoản!!!","Thông báo!!!");
                    }
                }
                if (comboBox1.Text == "Admin") // Nếu chọn Admin
                {
                    conn.Open(); // Mở kết nối 
                    string tk = txtUser.Text;
                    string mk = txtPW.Text;
                    string sql = "select * from dbo.UserAD WHERE TaiKhoanAD = '" + tk + "' and MatKhauAD = '" + mk + "'"; // Tạo câu lệnh truy vấn
                    // Câu lệnh thực hiện truy vấn
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dta = cmd.ExecuteReader();
                    // Đọc kết quả truy vấn
                    if (dta.Read() == true) // Nếu tài khoản, mật khẩu đúng thì làm tiếp còn sai thì trả ra else
                    {
                        new HomeAdmin().Show();
                        conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai mật khẩu hoặc tài khoản!!!","Thông báo");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu đăng nhập!!!");
            }
            conn.Close();
        }
        // Tạo sự kiện cho label 'Exit' để đóng ứng dụng
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Tạo sự kiện hiện và đóng mật khẩu
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            txtPW.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            txtPW.UseSystemPasswordChar = true;
        }
        // Tạo sự kiện đăng ký
        private void DangKyUser_Click(object sender, EventArgs e)
        {
            new Dangky().Show();
            this.Hide();
        }
    }
}
