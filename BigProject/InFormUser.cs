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
    public partial class InFormUser : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public InFormUser()
        {
            InitializeComponent();
        }
        public InFormUser(string idhv)
        {
            InitializeComponent();
            IDHV.Text = idhv.ToString(); // Nhận giá trị từ Form trước
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối
            // Câu lệnh thực hiện truy vấn
            string sql = @"SELECT * FROM InStudent WHERE IDHV = '" + IDHV.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr.Read())
            {
                string ht = (string)dr["HT"].ToString();
                HT.Text = ht;
                string sdt = (string)dr["SDT"].ToString();
                SDT.Text = sdt;
                string dc = (string)dr["DC"].ToString();
                DC.Text = dc;
                string email = (string)dr["EMAIL"].ToString();
                Email.Text = email;
                DateTime sdate = (DateTime)dr["NDKH"]; // Chuyển đổi kiểu dữ liệu
                string ndkh = sdate.ToString("dd-MM-yyyy");
                NDKH.Text = ndkh;
                string ttthp = (string)dr["TTHP"].ToString();
                if (ttthp == "False") // Kiểm tra nộp học phí hay chưa
                {
                    TTHP.Text = "Chưa đóng học phí";
                }
                else
                {
                    TTHP.Text = "Đã nộp học phí";
                }
                string lhvdk = (string)dr["MaLop"].ToString();
                LHVDK.Text = lhvdk;
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
        // Tạo sự kiện Click nút "Sửa thông tin"
        private void SuaTT_Click(object sender, EventArgs e)
        {
            if (HT.Enabled == false) // Kiểm tra xem đang sửa hay chưa sửa, nếu chưa sửa thì mở các controls lên để nhập dữ liệu và xác nhận
            {
                HT.Enabled = true;
                SDT.Enabled = true;
                DC.Enabled = true;
                Email.Enabled = true;
                LHVDK.Enabled = true;
                XacNhan.Enabled = true;
                SuaTT.Text = "Dừng sửa"; // Thay đổi Text nút
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn dừng cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận dừng cập nhật
                {
                    conn.Open(); // Mở kết nối
                    // Câu lệnh thực hiện truy vấn
                    string sql = @"SELECT * FROM InStudent WHERE IDHV = '" + IDHV.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    // Đọc kết quả truy vấn và in ra
                    while (dr.Read())
                    {
                        string ht = (string)dr["HT"].ToString();
                        HT.Text = ht;
                        string sdt = (string)dr["SDT"].ToString();
                        SDT.Text = sdt;
                        string dc = (string)dr["DC"].ToString();
                        DC.Text = dc;
                        string email = (string)dr["EMAIL"].ToString();
                        Email.Text = email;
                        DateTime sdate = (DateTime)dr["NDKH"];
                        string ndkh = sdate.ToString("dd-MM-yyyy");
                        NDKH.Text = ndkh;
                        string ttthp = (string)dr["TTHP"].ToString();
                        if (ttthp == "False")
                        {
                            TTHP.Text = "Chưa đóng học phí";
                        }
                        else
                        {
                            TTHP.Text = "Đã nộp học phí";
                        }
                        string lhvdk = (string)dr["MaLop"].ToString();
                        LHVDK.Text = lhvdk;
                    }
                    conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                    // Đóng các controls không cho nhập dữ liệu và xác nhận
                    HT.Enabled = false;
                    SDT.Enabled = false;
                    DC.Enabled = false;
                    Email.Enabled = false;
                    LHVDK.Enabled = false;
                    XacNhan.Enabled = false;
                    SuaTT.Text = "Sửa thông tin"; // Thay đổi Text nút
                }
            }
        }

        private void XacNhan_Click(object sender, EventArgs e)
        {
            // Tạo các ràng buộc không để trống của các ô nhập
            if (HT.Text =="")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!","Thông báo!!!");
                HT.Focus();
            }
            else if (SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!", "Thông báo!!!");
                SDT.Focus();
            }
            else if (SDT.Text.Length != 10) // Nhập đúng độ dài số điện thoại bằng 10
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại!!!");
                SDT.Focus();
            }
            else if (DC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!!!", "Thông báo!!!");
                DC.Focus();
            }
            else if (Email.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Email!!!", "Thông báo!!!");
                Email.Focus();
            }
            else if (!Email.Text.Contains("@gmail.com")) // Nhập phải có đuôi @gmail.com
            {
                MessageBox.Show("Vui lòng nhập đuôi @gmail.com");
                Email.Focus();
            }
            else if (LHVDK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lớp học!!!", "Thông báo!!!");
                LHVDK.Focus();
            }
            else if (MessageBox.Show("Bạn có muốn cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận cập nhật
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("Update InStudent set HT = @HTHV, DC = @DCHV, SDT = @SDTHV, Email = @EmailHV, MaLop = @MaLop where IDHV = @IDHV", conn);
                sql.Parameters.AddWithValue("@HTHV", HT.Text);
                sql.Parameters.AddWithValue("@DCHV", DC.Text);
                sql.Parameters.AddWithValue("@SDTHV", SDT.Text);
                sql.Parameters.AddWithValue("@EmailHV", Email.Text);
                sql.Parameters.AddWithValue("@IDHV", IDHV.Text);
                sql.Parameters.AddWithValue("@MaLop", LHVDK.Text);
                sql.ExecuteNonQuery();
                sql.Parameters.Clear();
                conn.Close();
                // Đóng các controls không cho nhập dữ liệu và xác nhận
                HT.Enabled = false;
                SDT.Enabled = false;
                DC.Enabled = false;
                Email.Enabled = false;
                LHVDK.Enabled = false;
                XacNhan.Enabled = false;
                SuaTT.Text = "Sửa thông tin"; // Thay đổi Text nút
            }
        }
        // Tạo sự kiện ở ô nhập Số điện thoại chỉ cho nhập số và xóa
        private void SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
