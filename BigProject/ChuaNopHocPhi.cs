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
    public partial class ChuaNopHocPhi : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server

        public ChuaNopHocPhi()
        {
            InitializeComponent();
        }
        private void ChuaNopHocPhi_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and TTHP = 'false' Order by InStudent.IDHV", conn); // Tạo đối tượng
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
        }
        private void HTHV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql1 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and TTHP = 'false' and HT like N'%" + HTHV.Text + "%' Order by InStudent.IDHV", conn);
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
            SqlCommand sql2 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and TTHP = 'false' and InStudent.IDHV like '%" + IDHV.Text + "%' Order by InStudent.IDHV", conn);
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
            SqlCommand sql3 = new SqlCommand("Select InStudent.IDHV as 'ID học viên', HT as 'Họ và tên', SDT as 'Số điện thoại', DC as 'Địa chỉ',Email as 'Email', NDKH as 'Ngày đăng ký học', TTHP as ' Tình trạng học phí' , MaLop as 'Học lớp', TaiKhoanSV as 'Tài khoản', MatKhauSV as 'Mật khẩu' from InStudent,UserSV where InStudent.IDHV = UserSV.IDHV and TTHP = 'false' and MaLop like '%" + TenLop.Text + "%' Order by MaLop", conn);
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql3);
            DataTable bangphu3 = new DataTable();
            chuyenkieu1.Fill(bangphu3);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu3;
        }
    }
}
