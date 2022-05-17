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
    public partial class ThongTinCacGiaoVien : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public ThongTinCacGiaoVien()
        {
            InitializeComponent();
            ThongTinCacGiaoVien_Load();
            AddBinding();
        }
        private void ThongTinCacGiaoVien_Load()
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select GiaoVien.IDGV as 'ID giáo viên', HTGV as 'Họ và tên', NgaySinh as 'Ngày sinh', DCGV as 'Địa chỉ', SDTGV as 'Số điện thoại', EmailGV as 'Email',MaLop as 'Tên lớp' from GiaoVien,InClass where GiaoVien.IDGV = InClass.IDGV order by GiaoVien.IDGV", conn); // Tạo đối tượng
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
        }
      
        private void HTGV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql1 = new SqlCommand("Select GiaoVien.IDGV as 'ID giáo viên', HTGV as 'Họ và tên', NgaySinh as 'Ngày sinh', DCGV as 'Địa chỉ', SDTGV as 'Số điện thoại', EmailGV as 'Email',MaLop as 'Tên lớp' from GiaoVien,InClass where GiaoVien.IDGV = InClass.IDGV and HTGV like N'%" + HTGV.Text + "%' order by GiaoVien.IDGV", conn); // Tạo đối tượng
            sql1.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql1); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu1 = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu1);// Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu1; // In dữ liệu lên bằng DataGridView
        }

        private void IDGV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql2 = new SqlCommand("Select GiaoVien.IDGV as 'ID giáo viên', HTGV as 'Họ và tên', NgaySinh as 'Ngày sinh', DCGV as 'Địa chỉ', SDTGV as 'Số điện thoại', EmailGV as 'Email',MaLop as 'Tên lớp' from GiaoVien,InClass where GiaoVien.IDGV = InClass.IDGV and GiaoVien.IDGV like '%" + IDGV.Text + "%' order by GiaoVien.IDGV", conn); // Tạo đối tượng
            sql2.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql2); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu2 = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu2); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu2; // In dữ liệu lên bằng DataGridView
        }

        private void TenLop_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql3 = new SqlCommand("Select GiaoVien.IDGV as 'ID giáo viên', HTGV as 'Họ và tên', NgaySinh as 'Ngày sinh', DCGV as 'Địa chỉ', SDTGV as 'Số điện thoại', EmailGV as 'Email',MaLop as 'Tên lớp' from GiaoVien,InClass where GiaoVien.IDGV = InClass.IDGV and MaLop like '%" + TenLop.Text + "%' order by GiaoVien.IDGV", conn); // Tạo đối tượng
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql3); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu3 = new DataTable(); // Tạo một bảng giả
            chuyenkieu1.Fill(bangphu3); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu3; // In dữ liệu lên bằng DataGridView
        }
        void AddBinding()
        {
            HTGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Họ và tên"));
            NSGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Ngày sinh", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty)); // Chuyển sang kiểu 
            DCGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Địa chỉ"));
            SDTGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Số điện thoại"));
            EmailGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Email"));
            IDGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "ID giáo viên"));
        }

        private void SuaTT_Click(object sender, EventArgs e)
        {
            if (HTGVCS.Enabled == false)
            {
                HTGVCS.Enabled = true;
                NSGVCS.Enabled = true;
                DCGVCS.Enabled = true;
                SDTGVCS.Enabled = true;
                EmailGVCS.Enabled = true;
                XacNhan.Enabled = true;
                SuaTT.Text = "Dừng sửa";
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn dừng cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận dừng cập nhật
                {
                    HTGVCS.Enabled = false;
                    NSGVCS.Enabled = false;
                    DCGVCS.Enabled = false;
                    SDTGVCS.Enabled = false;
                    EmailGVCS.Enabled = false;
                    XacNhan.Enabled = false;
                    SuaTT.Text = "Sửa thông tin";
                }
            }
        }

        private void XacNhan_Click(object sender, EventArgs e)
        {
            // Tạo các ràng buộc không để trống của các ô nhập
            if (HTGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên!!!", "Thông báo!!!");
                HTGVCS.Focus();
            }
            else if (NSGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày sinh!!!", "Thông báo!!!");
                NSGVCS.Focus();
            }
            else if (SDTGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!!!", "Thông báo!!!");
                SDTGVCS.Focus();
            }
            else if (SDTGVCS.Text.Length != 10) // Nhập đúng độ dài số điện thoại bằng 10
            {
                MessageBox.Show("Vui lòng nhập đúng số điện thoại!!!");
                SDTGVCS.Focus();
            }
            else if (DCGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!!!", "Thông báo!!!");
                DCGVCS.Focus();
            }
            else if (EmailGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Email!!!", "Thông báo!!!");
                EmailGVCS.Focus();
            }
            else if (!EmailGVCS.Text.Contains("@gmail.com")) // Nhập phải có đuôi @gmail.com
            {
                MessageBox.Show("Vui lòng nhập đuôi @gmail.com");
                EmailGVCS.Focus();
            }
            else if (MessageBox.Show("Bạn có muốn cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận cập nhật
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("Update GiaoVien set HTGV = @HTGV, NgaySinh = @NSGV, DCGV = @DCGV, SDTGV = @SDTGV, EmailGV = @EmailGV where IDGV = @IDGV", conn);
                sql.Parameters.AddWithValue("@HTGV", HTGVCS.Text);
                sql.Parameters.AddWithValue("@NSGV", NSGVCS.Text);
                sql.Parameters.AddWithValue("@DCGV", DCGVCS.Text);
                sql.Parameters.AddWithValue("@SDTGV", SDTGVCS.Text);
                sql.Parameters.AddWithValue("@EmailGV", EmailGVCS.Text);
                sql.Parameters.AddWithValue("@IDGV", IDGVCS.Text);
                sql.ExecuteNonQuery();
                sql.Parameters.Clear();
                conn.Close();
                // Đóng các controls không cho nhập dữ liệu và xác nhận
                HTGVCS.Enabled = false;
                NSGVCS.Enabled = false;
                DCGVCS.Enabled = false;
                SDTGVCS.Enabled = false;
                EmailGVCS.Enabled = false;
                SuaTT.Text = "Sửa thông tin"; // Thay đổi Text nút
            }
        }
    }
}