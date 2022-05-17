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
    public partial class DoiMatKhau : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        private string idhv;
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        public DoiMatKhau(string id)
        {
            InitializeComponent();
            idhv = id; // Nhận giá trị từ Form trước
        }
        private void XacNhan_Click(object sender, EventArgs e)
        {
            // Tạo các ràng buộc không để trống của các ô nhập
            if (NLMKC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu cũ!!!");
                NLMKC.Focus();
            }
            else if (NMKM.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!!!");
                NMKM.Focus();
            }
            else if (NLMKM.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới!!!");
                NLMKM.Focus();
            }
            else if (NMKM.Text != NLMKM.Text) //Nhập đúng mật khẩu cũ
            {
                MessageBox.Show("Vui lòng nhập lại đúng mật khẩu mới");
                NLMKM.Focus();
            }
            else if (NLMKC.Text == NMKM.Text) // Đổi mật khẩu không được giống với mật khẩu cũ
            {
                MessageBox.Show("Vui lòng đổi mật khẩu không giống mật khẩu cũ!!!");
                NMKM.Focus();
            }
            else
            {
                conn.Open(); // Mở kết nối
                // Câu lệnh thực hiện truy vấn
                string sql = "select * from dbo.UserSV WHERE IDHV = '" + idhv + "' and MatKhauSV = '" + NLMKC.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                // Đọc kết quả truy vấn và in ra
                if (dta.Read() != true) // Kiểm tra mật khẩu cũ đã đúng chưa
                {
                    MessageBox.Show("Nhập lại mật khẩu cũ sai!!!");
                    NLMKC.Focus();
                    conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                }
                else
                {
                    conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                    if (MessageBox.Show("Bạn có muốn đổi mật khẩu?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận
                    {
                        conn.Open(); // Mở kết nối
                        MessageBox.Show("Đổi mật khẩu thành công");
                        // Cập nhật dữ liệu trong database
                        string cmd1 = @"Update UserSV set MatKhauSV = '" + NLMKM.Text + "' where IDHV = " + idhv;
                        SqlCommand thuchien = new SqlCommand(cmd1, conn);
                        thuchien.ExecuteNonQuery(); // Thi hành cập nhật
                        conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                        this.Close(); // Đóng form
                    }
                }
            }
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            NLMKC.UseSystemPasswordChar = false;
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            NLMKC.UseSystemPasswordChar = true;
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            NMKM.UseSystemPasswordChar = false;
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            NMKM.UseSystemPasswordChar = true;
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            NLMKM.UseSystemPasswordChar = false;
        }
        // Tạo các sự kiện hiện thị mật khẩu
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            NLMKM.UseSystemPasswordChar = true;
        }
    }
}
