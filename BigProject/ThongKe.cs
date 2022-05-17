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
    public partial class ThongKe : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public ThongKe()
        {
            InitializeComponent();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select Sum(SLHVDK) as 'Tổng số học viên hiện có', count(MaLop) as 'Tổng số lớp hiện có' from InClass where InClass.NBD between @Ngaydau and @Ngaycuoi", conn); // Tạo đối tượng
            sql.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql2 = new SqlCommand("select Count(IDHV)as 'Số sinh viên viên đăng ký',InStudent.MaLop as 'Mã Lớp' from InStudent,InClass where NDKH between @Ngaydau and @NgayCuoi and NBD between @Ngaydau and @Ngaycuoi and InStudent.MaLop = InClass.MaLop group by InStudent.MaLop", conn); // Tạo đối tượng
            sql2.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql2.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql2.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql2);
            DataTable bangphu1 = new DataTable();
            chuyenkieu1.Fill(bangphu1);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql3 = new SqlCommand("select Count(IDHV) as 'Tổng số học viên đang theo học',InStudent.Malop as 'Mã lớp' from InClass,InStudent where Datediff(day,InStudent.NDKH,InClass.NBD) <= datediff(day,InStudent.NDKH,getdate()) and InClass.MaLop = InStudent.MaLop and NBD between @Ngaydau and @Ngaycuoi group by InStudent.MaLop", conn); // Tạo đối tượng
            sql3.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql3.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu2 = new SqlDataAdapter(sql3);
            DataTable bangphu2 = new DataTable();
            chuyenkieu2.Fill(bangphu2);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql4 = new SqlCommand("select Count(IDHV) as 'Tổng số học viên chưa theo học',InStudent.Malop as 'Mã lớp' from InClass,InStudent where Datediff(day,InStudent.NDKH,InClass.NBD) >= datediff(day,InStudent.NDKH,getdate()) and InClass.MaLop = InStudent.MaLop and NBD between @NgayDau and @Ngaycuoi group by InStudent.MaLop", conn); // Tạo đối tượng
            sql4.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql4.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql4.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu3 = new SqlDataAdapter(sql4);
            DataTable bangphu3 = new DataTable();
            chuyenkieu3.Fill(bangphu3);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
            dataGridView2.DataSource = bangphu1;
            dataGridView4.DataSource = bangphu2;
            dataGridView3.DataSource = bangphu3;
        }

        private void Ngaycuoi_ValueChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select Sum(SLHVDK) as 'Tổng số học viên hiện có', count(MaLop) as 'Tổng số lớp hiện có' from InClass where InClass.NBD between @Ngaydau and @Ngaycuoi", conn); // Tạo đối tượng
            sql.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql2 = new SqlCommand("select Count(IDHV)as 'Số sinh viên viên đăng ký',InStudent.MaLop as 'Mã Lớp' from InStudent,InClass where NDKH between @Ngaydau and @NgayCuoi and NBD between @Ngaydau and @Ngaycuoi and InStudent.MaLop = InClass.MaLop group by InStudent.MaLop", conn); // Tạo đối tượng
            sql2.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql2.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql2.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql2);
            DataTable bangphu1 = new DataTable();
            chuyenkieu1.Fill(bangphu1);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql3 = new SqlCommand("select Count(IDHV) as 'Tổng số học viên đang theo học',InStudent.Malop as 'Mã lớp' from InClass,InStudent where Datediff(day,InStudent.NDKH,InClass.NBD) <= datediff(day,InStudent.NDKH,getdate()) and InClass.MaLop = InStudent.MaLop and NBD between @Ngaydau and @Ngaycuoi group by InStudent.MaLop", conn); // Tạo đối tượng
            sql3.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql3.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu2 = new SqlDataAdapter(sql3);
            DataTable bangphu2 = new DataTable();
            chuyenkieu2.Fill(bangphu2);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql4 = new SqlCommand("select Count(IDHV) as 'Tổng số học viên chưa theo học',InStudent.Malop as 'Mã lớp' from InClass,InStudent where Datediff(day,InStudent.NDKH,InClass.NBD) >= datediff(day,InStudent.NDKH,getdate()) and InClass.MaLop = InStudent.MaLop and NBD between @NgayDau and @Ngaycuoi group by InStudent.MaLop", conn); // Tạo đối tượng
            sql4.Parameters.AddWithValue("@Ngaydau", Ngaydau.Value.ToString("yyyy-MM-dd")); // Thiết lập tham số 
            sql4.Parameters.AddWithValue("@Ngaycuoi", Ngaycuoi.Value.ToString("yyyy-MM-dd"));
            sql4.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu3 = new SqlDataAdapter(sql4);
            DataTable bangphu3 = new DataTable();
            chuyenkieu3.Fill(bangphu3);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
            dataGridView2.DataSource = bangphu1;
            dataGridView4.DataSource = bangphu2;
            dataGridView3.DataSource = bangphu3;
        }
    }
}
