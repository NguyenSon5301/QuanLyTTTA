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
    public partial class NopHocPhi : Form
    {
        private string idhv;
        private string ht;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-FO8QLMB;Initial Catalog=QuanLyTTTA;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        public NopHocPhi()
        {
            InitializeComponent();
        }
        public NopHocPhi(string id)
        {
            InitializeComponent();
            idhv = id; // Nhận giá trị từ Form trước
        }

        private void NHP_Click(object sender, EventArgs e)
        {
            // Tạo các ràng buộc không để trống của các ô nhập
            if (IDSV.Text =="")
            {
                MessageBox.Show("Vui lòng nhập ID của bạn!!!");
                IDSV.Focus();
            }
            else if (HT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên của bạn");
                HT.Focus();
            }
            else if (LoiNhan.Text =="")
            {
                MessageBox.Show("Vui lòng nhập lời nhắn!!! Ví dụ : Nộp HP tháng 9");
                LoiNhan.Focus();
            }
            else if (IDSV.Text != idhv) // Nhập đúng ID sinh viên đang nộp học phí
            {
                MessageBox.Show("Bạn đã nhập sai ID, vui lòng nhập lại!!!");
                IDSV.Focus();
            }
            else if (HT.Text != ht) // Nhập đúng họ tên
            {
                MessageBox.Show("Bạn đã nhập sai họ tên, vui lòng nhập lại!!!");
                HT.Focus();
            }
            else
            {
                MessageBox.Show("Đã nộp học phí thành công!!!");
                conn.Open(); // Mở kết nối
                // Câu lệnh thực hiện truy vấn
                SqlCommand cmd1 = new SqlCommand("Insert NopHP(IDHV,HTHV,LoiNhan) Values (@IDHV,@HTHV,@LoiNhan)",conn);
                cmd1.Parameters.AddWithValue("@IDHV", IDSV.Text); // Thiết lập tham số 
                cmd1.Parameters.AddWithValue("@HTHV", HT.Text);
                cmd1.Parameters.AddWithValue("@LoiNhan", LoiNhan.Text);
                cmd1.ExecuteNonQuery(); // Thi hành truy vân
                cmd1.Parameters.Clear();
                // Câu lệnh thực hiện cập nhật
                SqlCommand cmd2 = new SqlCommand("Update InStudent set TTHP = 'True' where IDHV = '" + idhv + "'",conn); // Đổi đã đóng học phí
                cmd2.ExecuteNonQuery(); // Thi hành cập nhật
                conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
                this.Close(); // Đóng Form
            }
        }
        // Khi Form load lấy ra dữ liệu về họ tên, ID học viên đang muốn đóng học phí
        private void NopHocPhi_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối
            string sql = @"SELECT * FROM InStudent WHERE IDHV = '" + idhv + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ht = (string)dr["HT"].ToString();
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
    }
}
