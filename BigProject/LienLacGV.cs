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
    public partial class LienLacGV : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        private string malop,idhv;
        public LienLacGV()
        {
            InitializeComponent();
        }
        public LienLacGV(string ml,string id)
        {
            InitializeComponent();
            malop = ml; // Nhận giá trị từ Form trước
            idhv = id; // Nhận giá trị từ Form trước
        }
        // Tạo sự kiện Load Form thì in ra thông tin giáo viên
        private void LienLacGV_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối
            // Câu lệnh thực hiện truy vấn
            string sql = @"SELECT * FROM GiaoVien,InClass WHERE GiaoVien.IDGV = InClass.IDGV and InClass.MaLop ='" + malop  + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr.Read())
            {
                string htgv = (string)dr["HTGV"].ToString();
                HTGV.Text = htgv;
                string sdtgv = (string)dr["SDTGV"].ToString();
                SDTGV.Text = sdtgv;
                string dcgv = (string)dr["DCGV"].ToString();
                DCGV.Text = dcgv;
                string emailgv = (string)dr["EMAILGV"].ToString();
                EmailGV.Text = emailgv;
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
    }
}
