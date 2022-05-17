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
    public partial class ThongTinCacLopHoc : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True");
        public ThongTinCacLopHoc()
        {
            InitializeComponent();
            ThongTinCacLopHoc_Load();
            Addbinding();
        }
        private void Addbinding()
        {
            TLCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Tên lớp"));
            SLHVTDCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Số lượng học viên tối đa"));
            KGHCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Khung giờ học"));
            SBHCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Số buổi học"));
            HPCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Học phí"));
            IDGVCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "ID giáo viên"));
            NBDCS.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Ngày bắt đầu", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty)); // Chuyển sang kiểu
        }
        private void ThongTinCacLopHoc_Load()
        {
            conn.Open(); // Mở kết nối 
            // Câu lệnh thực hiện truy vấn
            SqlCommand sql = new SqlCommand("Select MaLop as 'Tên lớp', SLHVDK as 'Số lượng học viên của lớp', SLHVTD as 'Số lượng học viên tối đa', KGH as 'Khung giờ học', NBD as 'Ngày bắt đầu', SBH as 'Số buổi học', HP as 'Học phí',InClass.IDGV as 'ID giáo viên', HTGV as 'Tên giáo viên' from InClass,GiaoVien where InClass.IDGV = GiaoVien.IDGV order by MaLop", conn); // Tạo đối tượng
            sql.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu = new SqlDataAdapter(sql); // Chuyển kiểu của Đối tượng truy vấn
            DataTable bangphu = new DataTable(); // Tạo một bảng giả
            chuyenkieu.Fill(bangphu); // Lấp đầy dữ liệu cho bảng giả bằng các dữ liệu đã được chuyển kiểu
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu; // In dữ liệu lên bằng DataGridView
        }
        private void MaLop_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql1 = new SqlCommand("Select MaLop as 'Tên lớp', SLHVDK as 'Số lượng học viên của lớp', SLHVTD as 'Số lượng học viên tối đa', KGH as 'Khung giờ học', NBD as 'Ngày bắt đầu', SBH as 'Số buổi học', HP as 'Học phí',InClass.IDGV as 'ID giáo viên', HTGV as 'Tên giáo viên' from InClass,GiaoVien where InClass.IDGV = GiaoVien.IDGV and MaLop like '%" + MaLop.Text + "%' order by MaLop", conn);
            sql1.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql1);
            DataTable bangphu1 = new DataTable();
            chuyenkieu1.Fill(bangphu1);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu1;
        }

        private void HTGV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql2 = new SqlCommand("Select MaLop as 'Tên lớp', SLHVDK as 'Số lượng học viên của lớp', SLHVTD as 'Số lượng học viên tối đa', KGH as 'Khung giờ học', NBD as 'Ngày bắt đầu', SBH as 'Số buổi học', HP as 'Học phí',InClass.IDGV as 'ID giáo viên', HTGV as 'Tên giáo viên' from InClass,GiaoVien where InClass.IDGV = GiaoVien.IDGV and HTGV like N'%" + HTGV.Text + "%' order by MaLop", conn);
            sql2.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu1 = new SqlDataAdapter(sql2);
            DataTable bangphu2 = new DataTable();
            chuyenkieu1.Fill(bangphu2);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu2;
        }

        private void SuaTT_Click(object sender, EventArgs e)
        {
            if (IDGVCS.Enabled == false)
            {
                IDGVCS.Enabled = true;
                SLHVTDCS.Enabled = true;
                KGHCS.Enabled = true;
                SBHCS.Enabled = true;
                HPCS.Enabled = true;
                NBDCS.Enabled = true;
                XacNhan.Enabled = true;
                SuaTT.Text = "Dừng sửa";
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn dừng cập nhật thông tin?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Tạo thông báo xác nhận dừng cập nhật
                {
                    IDGVCS.Enabled = false;
                    SLHVTDCS.Enabled = false;
                    KGHCS.Enabled = false;
                    SBHCS.Enabled = false;
                    HPCS.Enabled = false;
                    NBDCS.Enabled = false;
                    XacNhan.Enabled = false;
                    SuaTT.Text = "Sửa thông tin";
                }
            }
        }

        private void XacNhan_Click(object sender, EventArgs e)
        {
            if (SLHVTDCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng học viên tối đa");
                SLHVTDCS.Focus();
            }
            else if (KGHCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập khung giờ học");
                KGHCS.Focus();
            }
            else if (NBDCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày bắt đầu!!!");
                NBDCS.Focus();
            }
            else if (SBHCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng buổi học!!!");
                SBHCS.Focus();
            }
            else if (HPCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số học phí!!!");
                HPCS.Focus();
            }
            else if (IDGVCS.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID giáo viên!!!");
                IDGVCS.Focus();
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn sửa lớp học?", "Thông Báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open(); // Mở kết nối 
                    // Câu lệnh truy vấn
                    SqlCommand cmd1 = new SqlCommand("Update InClass set SLHVTD = @SLHVTD,KGH = @KGH,NBD = @NBD,SBH = @SBH,HP = @HP,IDGV = @IDGV where MaLop = @MaLop", conn); // Tạo đối tượng
                    cmd1.Parameters.AddWithValue("@SLHVTD", SLHVTDCS.Text); // Thiết lập tham số 
                    cmd1.Parameters.AddWithValue("@MaLop", TLCS.Text);
                    cmd1.Parameters.AddWithValue("@KGH", KGHCS.Text);
                    cmd1.Parameters.AddWithValue("@NBD", NBDCS.Text);
                    cmd1.Parameters.AddWithValue("@SBH", SBHCS.Text);
                    cmd1.Parameters.AddWithValue("@HP", HPCS.Text);
                    cmd1.Parameters.AddWithValue("@IDGV", IDGVCS.Text);
                    cmd1.ExecuteNonQuery(); // Thi hành truy vân
                    cmd1.Parameters.Clear();
                    conn.Close();
                    IDGVCS.Enabled = false;
                    SLHVTDCS.Enabled = false;
                    KGHCS.Enabled = false;
                    SBHCS.Enabled = false;
                    HPCS.Enabled = false;
                    NBDCS.Enabled = false;
                    XacNhan.Enabled = false;
                    SuaTT.Text = "Sửa thông tin";
                }
            }
        }

        private void IDGV_TextChanged(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            SqlCommand sql3 = new SqlCommand("Select MaLop as 'Tên lớp', SLHVDK as 'Số lượng học viên của lớp', SLHVTD as 'Số lượng học viên tối đa', KGH as 'Khung giờ học', NBD as 'Ngày bắt đầu', SBH as 'Số buổi học', HP as 'Học phí',InClass.IDGV as 'ID giáo viên', HTGV as 'Tên giáo viên' from InClass,GiaoVien where InClass.IDGV = GiaoVien.IDGV and InClass.IDGV like '%" + IDGV.Text + "%' order by MaLop", conn);
            sql3.CommandType = CommandType.Text;
            SqlDataAdapter chuyenkieu3 = new SqlDataAdapter(sql3);
            DataTable bangphu3 = new DataTable();
            chuyenkieu3.Fill(bangphu3);
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            dataGridView1.DataSource = bangphu3;
        }
    }
}
