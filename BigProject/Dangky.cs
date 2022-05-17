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
using System.Configuration;

namespace BigProject
{
    public partial class Dangky : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public Dangky()
        {
            InitializeComponent();
        }
        private void Dangky_Load(object sender, EventArgs e)
        {
            // Đổ dữ liệu vào combobox Mã lớp
            SqlDataAdapter docdata = new SqlDataAdapter("Select * from InClass", conn); // Khởi tạo đối tượng đọc dữ liệu
            DataTable bangphu = new DataTable(); // Khai báo DataTable để chứa dữ liệu
            docdata.Fill(bangphu); // Điền dữ liệu vào DataTable
            docdata.Dispose(); // Giải phóng docdata
            MaLop.DataSource = bangphu; // Gán dữ liệu nguồn
            MaLop.DisplayMember = "MaLop"; // Chỉ ra cái bạn muốn in ra vì combobox chỉ nhận dữ liệu 1 cột
        }
        private void MaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load dữ liệu thông tin của Mã Lớp
            {
                conn.Open(); // Mở kết nối 
                // Câu lệnh thực hiện truy vấn
                string sql = "SELECT * FROM InClass WHERE MaLop = '" + MaLop.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                // Đọc kết quả truy vấn và in ra
                while (dr.Read())
                {
                    DateTime nbd = (DateTime)dr["NBD"]; // Chuyển đổi kiểu dữ liệu sang DATE 
                    NBD.Text = nbd.ToString("dd-MM-yyyy");
                    string kgh = (string)dr["KGH"].ToString();
                    KGH.Text = kgh;
                    string slhvtd = (string)dr["SLHVTD"].ToString();
                    SLHVTD.Text = slhvtd;
                    string slhvdk = (string)dr["SLHVDK"].ToString();
                    SLHVDDK.Text = slhvdk;
                    string sbh = (string)dr["SBH"].ToString();
                    SBH.Text = sbh;
                }
                conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            }
        }
        // Tạo sự kiện đăng ký
        private void DangKy1_Click(object sender, EventArgs e)
        {
            // Các ràng buộc không cho phép để trống cho Việc đăng ký
            if (HT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!");
                HT.Focus();
            }
            else if (SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!");
                SDT.Focus();
            }
            else if (SDT.Text.Length != 10) // Nhập đúng độ dài số điện thoại bằng 10
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại!!!");
                SDT.Focus();
            }
            else if (DC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!!!");
                DC.Focus();
            }
            else if (Email.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Email!!!");
                Email.Focus();
            }
            else if (!Email.Text.Contains("@gmail.com")) // Nhập phải có đuôi @gmail.com
            {
                MessageBox.Show("Vui lòng nhập đuôi @gmail.com");
                Email.Focus();
            }
            else if (TK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản!!!");
                TK.Focus();
            }
            else if (MK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!!!");
                MK.Focus();
            }
            else if (NLMK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!!!");
                NLMK.Focus();
            }
            else if (MK.Text.Length > 7) // Chỉ cho phép nhập mật khẩu dưới 7 ký tự
            {
                MessageBox.Show("Vui lòng nhập 7 ký tự");
                MK.Focus();
            }
            else if (MK.Text != NLMK.Text) // Kiểm tra đúng mật khẩu được nhập lại
            {
                MessageBox.Show("Vui lòng nhập đúng mật khẩu!!!");
                NLMK.Focus();
            }
            else if (CheckUserName(TK.Text) == false) // Kiểm tra xem tài khoản đã có trong database
            {
                MessageBox.Show("Tài khoản đã có vui lòng nhập tài khoản khác!!!");
                TK.Focus();
            }
            else if (Convert.ToInt32(SLHVTD.Text) < Convert.ToInt32(SLHVDDK.Text)) // Kiểm tra xem lớp đã đầy hay chưa
            {
                MessageBox.Show("Lớp bạn chọn đã đầy, vui lòng chọn lớp khác!!!");
                MaLop.Focus();
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn đăng ký?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Thông báo xác nhận đăng ký
                {
                    conn.Open(); // Mở kết nối 
                    // Câu lệnh truy vấn
                    SqlCommand cmd1 = new SqlCommand("Insert InStudent(HT,SDT,DC,EMAIL,MaLop) Values(@HT,@SDT,@DC,@EMAIL,@MaLop)", conn); // Tạo đối tượng
                    cmd1.Parameters.AddWithValue("@HT", HT.Text); // Thiết lập tham số 
                    cmd1.Parameters.AddWithValue("@SDT", SDT.Text);
                    cmd1.Parameters.AddWithValue("@DC", DC.Text);
                    cmd1.Parameters.AddWithValue("@EMAIL", Email.Text);
                    cmd1.Parameters.AddWithValue("@MaLop", MaLop.Text);
                    cmd1.ExecuteNonQuery(); // Thi hành truy vân
                    cmd1.Parameters.Clear();
                    SqlCommand cmd2 = new SqlCommand("Insert UserSV(TaiKhoanSV,MatKhauSV) Values(@TaiKhoanSV,@MatKhauSV)", conn);
                    cmd2.Parameters.AddWithValue("@TaiKhoanSV", TK.Text);
                    cmd2.Parameters.AddWithValue("@MatKhauSV", MK.Text);
                    cmd2.ExecuteNonQuery();
                    cmd2.Parameters.Clear();
                    // Lấy ra giá trị ID học viên vừa mới tạo, để đưa vào bản thông tin
                    string sql = @"select * from UserSV inner join InStudent on InStudent.IDHV = UserSV.IDHV and UserSV.TaiKhoanSV = '" + TK.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn); // Tạo đối tượng
                    SqlDataReader dr = cmd.ExecuteReader(); // Thi hành đọc dữ liệu
                    while (dr.Read())
                    {
                        string idhv = (string)dr["IDHV"].ToString();
                        new HomeUser(idhv,MaLop.Text).Show();// Đưa ID học viện vào bản HomeUser (idhv)
                        this.Hide();
                    }
                    conn.Close(); // Giải phóng
                }
            }
        }
        // Tạo sự kiện đóng ứng dụng (exit)
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
        // Tạo sự kiện quay lại (back)
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
            new DangNhap().Show();
        }
        // Tạo sự kiện ở ô nhập Số điện thoại chỉ cho nhập số và xóa
        private void SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)|| e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        // Tạo sự kiện kiểm tra tài khoản đã có trong Data, nếu đã có thì trả về False nếu chưa thì trả về True
        public static bool CheckUserName(string taikhoan)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True");
            bool trangthai = false;
            using (SqlCommand cmd = new SqlCommand("CheckUser", conn)) // Đã tạo sẵn 1 PROCEDURE trong data base với tên "CheckUser", để lấy ra thông tin 
            {
                cmd.CommandType = CommandType.StoredProcedure; // Thi hành truy vân
                cmd.Parameters.AddWithValue("@TaiKhoanSV", taikhoan.Trim());
                conn.Open();
                trangthai = Convert.ToBoolean(cmd.ExecuteScalar()); // Thì hành trả về một giá trị duy nhất - ở hàng đầu tiên
                conn.Close();
            }
            return trangthai; // Trạng thái trả về true hay false
        }
        // Tạo sự kiện đổi màu cho dấu * nếu CheckUserName trả về false thì có nghĩa đã có tài khoản tồn tại và đổi dấu * thành màu đỏ và ngược lại thành màu xanh lá
        private void TK_TextChanged(object sender, EventArgs e)
        {
            if (CheckUserName(TK.Text) == false)
            {
                checkTK.ForeColor = Color.Red;
            }
            else
            {
                checkTK.ForeColor = Color.Green;
            }
        }
        // Tạo sự kiện đổi màu cho dấu *  nếu mật khẩu nhập lại không giống mật khẩu thì đổi dấu * thành màu đỏ và ngược lại thành màu xanh lá 
        private void NLMK_TextChanged(object sender, EventArgs e)
        {
            if(MK.Text != NLMK.Text)
            {
                CheckNLMK.ForeColor = Color.Red;
            }
            else
            {
                CheckNLMK.ForeColor = Color.Green;
            }
        }
        // Tạo sự kiện đổi màu cho dấu * nếu mật khẩu quá 7 ký tự thì đổi dấu * thành màu đỏ và ngược lại thành màu xanh lá
        private void MK_TextChanged(object sender, EventArgs e)
        {
            if (MK.Text.Length > 7)
            {
                CheckMK.ForeColor = Color.Red;
            }
            else
            {
                CheckMK.ForeColor = Color.Green;
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            MK.UseSystemPasswordChar = true;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            MK.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            NLMK.UseSystemPasswordChar = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            NLMK.UseSystemPasswordChar = false;
        }
    }
}
