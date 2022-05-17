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
    public partial class ThemLopHoc : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public ThemLopHoc()
        {
            InitializeComponent();
        }
        // Tạo sự kiện Click nút Thêm lớp học
        private void AddClass_Click(object sender, EventArgs e)
        {
            // Các ràng buộc không cho phép để trống
            if(MaLop.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã lớp!!!");
                MaLop.Focus();
            }
            else if (SLHVTD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng học viên tối đa");
                SLHVTD.Focus();
            }
            else if (KGH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập khung giờ học");
                KGH.Focus();
            }
            else if (NBD.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày bắt đầu!!!");
                SLHVTD.Focus();
            }
            else if (SBH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng buổi học!!!");
                SBH.Focus();
            }
            else if (HP.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số học phí!!!");
                HP.Focus();
            }
            else if (IDGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID giáo viên!!!");
                IDGV.Focus();
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn thêm lớp học?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open(); // Mở kết nối 
                    // Câu lệnh truy vấn
                    SqlCommand cmd1 = new SqlCommand("Insert InClass(MaLop,SLHVTD,KGH,NBD,SBH,HP,IDGV) Values(@MaLop,@SLHVTD,@KGH,@NBD,@SBH,@HP,@IDGV)", conn); // Tạo đối tượng
                    cmd1.Parameters.AddWithValue("@MaLop", MaLop.Text); // Thiết lập tham số 
                    cmd1.Parameters.AddWithValue("@SLHVTD", SLHVTD.Text);
                    cmd1.Parameters.AddWithValue("@KGH", KGH.Text);
                    cmd1.Parameters.AddWithValue("@NBD", NBD.Text);
                    cmd1.Parameters.AddWithValue("@SBH", SBH.Text);
                    cmd1.Parameters.AddWithValue("@HP", HP.Text);
                    cmd1.Parameters.AddWithValue("@IDGV", IDGV.Text);
                    cmd1.ExecuteNonQuery(); // Thi hành truy vân
                    cmd1.Parameters.Clear();
                    conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                    this.Close(); // Đóng Form lại
                }
            }
        }
        // Tạo sự kiện Load hiện thị lớp vừa đăng ký
        private void ThemLopHoc_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh truy vấn
            string sql = "select top 1 (MaLop) from InClass order by MaLop DESC";
            SqlCommand cmd = new SqlCommand(sql, conn); // Tạo đối tượng
            SqlDataReader dr = cmd.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr.Read())
            {
                string malop = (string)dr["MaLop"].ToString();
                lb_MalopCu.Text = malop;
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
    }
}
