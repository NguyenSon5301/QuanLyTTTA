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
    public partial class ThongTinCacHocVien : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True");
        public ThongTinCacHocVien()
        {
            InitializeComponent();
            ThongTinCacHocVien_Load();
            AddBinding();
        }
        private void ThongTinCacHocVien_Load()
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV Order by InStudent.IDHV", conn); // Tạo đối tượng
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
        }
        void AddBinding()
        {
            HTHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Họ và tên"));
            IDHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "ID học viên"));
            SDTHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Số điện thoại"));
            DCHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Địa chỉ"));
            EmailHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Email"));
            MaLopHVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Học lớp"));

        }

        private void HTHV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql1 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and HT like N'%"+HTHV.Text+"%' Order by InStudent.IDHV", conn);
            sql1.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql1);
            DataTable bangphu1 = new DataTable();
            chuyenkieu.Fill(bangphu1);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu1;
        }

        private void IDHV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql2 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and InStudent.IDHV like '%" + IDHV.Text + "%' Order by InStudent.IDHV", conn);
            sql2.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql2);
            DataTable bangphu2 = new DataTable();
            chuyenkieu.Fill(bangphu2);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu2;
        }

        private void TenLop_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql3 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and MaLop like '%" + TenLop.Text + "%' Order by MaLop", conn);
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql3);
            DataTable bangphu3 = new DataTable();
            chuyenkieu1.Fill(bangphu3);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu3;
        }

        private void SuaTT_Click(object sender, EventArgs e)
        {
            if (HTHVCS.Enabled == false) // Kiểm tra xem đang sửa hay chưa sửa, nếu chưa sửa thì mở các controls lên để nhập dữ liệu và xác nhận
            {
                HTHVCS.Enabled = true;
                SDTHVCS.Enabled = true;
                DCHVCS.Enabled = true;
                EmailHVCS.Enabled = true;
                MaLopHVCS.Enabled = true;
                XacNhan.Enabled = true;
                SuaTT.Text = "Dừng sửa"; // Thay đổi Text nút
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn dừng cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận dừng cập nhật
                {
                    // Đóng các controls không cho nhập dữ liệu và xác nhận
                    HTHVCS.Enabled = false;
                    SDTHVCS.Enabled = false;
                    DCHVCS.Enabled = false;
                    EmailHVCS.Enabled = false;
                    MaLopHVCS.Enabled = false;
                    XacNhan.Enabled = false;
                    SuaTT.Text = "Sửa thông tin"; // Thay đổi Text nút
                }
            }
        }

        private void XacNhan_Click(object sender, EventArgs e)
        {
            // Tạo các ràng buộc không để trống của các ô nhập
            if (HTHVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!", "Thông báo!!!");
                HTHVCS.Focus();
            }
            else if (SDTHVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!", "Thông báo!!!");
                SDTHVCS.Focus();
            }
            else if (SDTHVCS.Text.Length != 10) // Nhập đúng độ dài số điện thoại bằng 10
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại!!!");
                SDTHVCS.Focus();
            }
            else if (DCHVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!!!", "Thông báo!!!");
                DCHVCS.Focus();
            }
            else if (EmailHVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Email!!!", "Thông báo!!!");
                EmailHVCS.Focus();
            }
            else if (!EmailHVCS.Text.Contains("@gmail.com")) // Nhập phải có đuôi @gmail.com
            {
                MessageBox.Show("Vui lòng nhập đuôi @gmail.com");
                EmailHVCS.Focus();
            }
            else if (MaLopHVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lớp học!!!", "Thông báo!!!");
                MaLopHVCS.Focus();
            }
            else if (MessageBox.Show("Bạn có muốn cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận cập nhật
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("Update InStudent set HT = @HTHV, DC = @DCHV, SDT = @SDTHV, Email = @EmailHV, MaLop = @MaLop where IDHV = @IDHV", conn);
                sql.Parameters.AddWithValue("@HTHV", HTHVCS.Text);
                sql.Parameters.AddWithValue("@DCHV", DCHVCS.Text);
                sql.Parameters.AddWithValue("@SDTHV", SDTHVCS.Text);
                sql.Parameters.AddWithValue("@EmailHV", EmailHVCS.Text);
                sql.Parameters.AddWithValue("@IDHV", IDHVCS.Text);
                sql.Parameters.AddWithValue("@MaLop", MaLopHVCS.Text);
                sql.ExecuteNonQuery();
                sql.Parameters.Clear();
                conn.Close();
                // Đóng các controls không cho nhập dữ liệu và xác nhận
                HTHVCS.Enabled = false;
                SDTHVCS.Enabled = false;
                DCHVCS.Enabled = false;
                EmailHVCS.Enabled = false;
                MaLopHVCS.Enabled = false;
                XacNhan.Enabled = false;
                SuaTT.Text = "Sửa thông tin"; // Thay đổi Text nút
            }
        }
    }
}
