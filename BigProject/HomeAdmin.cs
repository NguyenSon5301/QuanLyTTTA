using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BigProject
{
    public partial class HomeAdmin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=desktop-fo8qlmb;Initial Catalog=QuanlyTTTA;Integrated Security=True"); // Tạo đối tượng từ một chuỗi kết nối tới SQL Server
        // Tạo các biến Private(chỉ được truy xuất trong nội bộ lớp)
        private Button nutchay;
        private Random random;
        private int chisotam;
        private Form ChayForm;
        private string tilte;
        public HomeAdmin()
        {
            InitializeComponent();
            random = new Random(); // Tạo các biến số Ramdom
            btn_CloseFormCon.Visible = false; // Ẩn nút Close của Form con
            this.Text = string.Empty; // Để trống tên của Form
            this.ControlBox = false; // Tắt caption của Form
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; // Cho phép hiện ra các dòng menu hiện ứng dụng ( Giờ, Cốc cốc, Start,...) khi mở to
        }
        // Tạo sự kiện khi Load Form
        private void HomeAdmin_Load(object sender, EventArgs e)
        {
            conn.Open(); // Mở kết nối 
            // Các câu lệnh thực hiện truy vấn hiện thị lên ở Desktop Home như tổng số lớp, tổng số giáo viên,....
            SqlCommand sql = new SqlCommand("Select Sum(SLHVDK) as 'Tổng số học viên đăng ký', count(MaLop) as 'Tổng số lớp hiện có' from InClass", conn);
            SqlDataReader dr = sql.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr.Read())
            {
                TSHV.Text = (string)dr["Tổng số học viên đăng ký"].ToString();
                TSL.Text = (string)dr["Tổng số lớp hiện có"].ToString();
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql2 = new SqlCommand("select Count(IDHV) as 'Tổng số học viên đang theo học' from InClass,InStudent where Datediff(day,InStudent.NDKH,InClass.NBD) <= datediff(day,InStudent.NDKH,getdate()) and InClass.MaLop = InStudent.MaLop", conn);
            SqlDataReader dr1 = sql2.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr1.Read())
            {
                TSHVDH.Text = (string)dr1["Tổng số học viên đang theo học"].ToString();
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
            conn.Open(); // Mở kết nối 
            SqlCommand sql3 = new SqlCommand("select count(IDGV) as 'Tổng số giáo viên' from GiaoVien", conn);
            SqlDataReader dr2 = sql3.ExecuteReader();
            // Đọc kết quả truy vấn và in ra
            while (dr2.Read())
            {
                TSGV.Text = (string)dr2["Tổng số giáo viên"].ToString();
            }
            conn.Close(); // Không dùng đến kết nối thì đóng lại (giải phóng)
        }
        // Thêm các chức năng khi kéo biểu mẫu (Drag Form)
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        // Tạo sự kiện lấy màu ngẫu nhiên
        private Color Chonmau()
        {
            int chiso = random.Next(MauHome.ColorList.Count);
            while (chisotam == chiso) // Check xem có giống màu trước, nếu giống thì trả ra một màu khác
            {
                chiso = random.Next(MauHome.ColorList.Count); // Đổi màu khác
            }
            chisotam = chiso;
            string mau = MauHome.ColorList[chiso]; // Lấy màu từ một Class tên MauHome
            return ColorTranslator.FromHtml(mau); // Trả ra một màu nhất định
        }
        private void ChuyenNut(object btnchon)
        {
            if (btnchon != null) // Check xem đã chọn nút nếu đã chọn thì thực hiện tiếp
            {
                if (nutchay != (Button)btnchon) // Check xem cái nút chọn có phải cái nút đang chọn, nếu phải thì không thực hiện, nếu không phải thì thực hiện tiếp
                {
                    TatNut(); // Tắt nút trước (nút vừa chọn) để chuyển sang nút mới
                    // Các thay đổi về màu, font chữ,... của nút đang được chọn
                    Color mau = Chonmau(); 
                    nutchay = (Button)btnchon;
                    nutchay.BackColor = mau;
                    nutchay.ForeColor = Color.White;
                    nutchay.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pn_home.BackColor = mau;
                    pn_logo.BackColor = MauHome.Thaydoimau(mau, -0.3);
                    btn_CloseFormCon.Visible = true; // Hiện thị nút tắt Form Con
                }
            }
        }
        // Tạo sự kiện tắt nút trước khi chọn một nút mới
        private void TatNut()
        {
            foreach (Control nuttruoc in pn_chonlua.Controls) // Đại diện cho tất cả các nút trong Menu chọn
            {
                if (nuttruoc.GetType() == typeof(Button)) // Lấy ra những controls có kiểu là Button (nút), nếu là nút thì thực hiện đổi màu, font chữ,...
                {
                    nuttruoc.BackColor = Color.FromArgb(51, 51, 76);
                    nuttruoc.ForeColor = Color.Gainsboro;
                    nuttruoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        // Tạo sự kiện mở Form Con
        private void MoFormCon(Form FormCon, object btnSender)
        {
            if (ChayForm != null) // Kiểm tra xem trước đó đã mở Form Con nào chưa, nếu có thì tắt
            {
                ChayForm.Close();
            }
            ChuyenNut(btnSender); // Đổi nút được chọn, đổi màu, font chữ,....
            ChayForm = FormCon; // Gán cho ChayForm bằng Form được đưa vào, để kiểm tra xem có Form nào đang được chọn
            FormCon.TopLevel = false; // Luôn luôn trả TopLevel về false, vì Form để TopLevel(true) thường là form biểu mẫu chính trong ứng dụng, một số Form không có biểu mẫu mẹ hoặc có biểu mẫu mẹ là cửa sổ màn hình nền
            FormCon.FormBorderStyle = FormBorderStyle.None; // Tắt kiểu Form được đưa vào
            FormCon.Dock = DockStyle.Fill; // Lắp đầy khung panel Desktop
            this.pn_desktop.Controls.Add(FormCon); // Đưa form con vào panel Desktop
            this.pn_desktop.Tag = FormCon; // Lưa panel Desktop theo định dạng của Form Con
            // Hiển thị Form Con
            FormCon.BringToFront(); // Đè lên form Home
            FormCon.Show(); // Hiển thị Form Con
            lbl_Home.Text = tilte; // Đổi chữ Home thành tên của Form hiển thị
        }
        //Tạo sự kiện đóng Form Con
        private void btn_CloseFormCon_Click(object sender, EventArgs e)
        {
            if (ChayForm != null) // Kiểm tra xem có Form con nào đang mở, nếu có (null) thì đóng
                ChayForm.Close();
            Reset();
        }
        //Tạo sự kiện Reset, để trả về Form Home như ban đầu
        private void Reset()
        {
            TatNut(); // Tắt nút đang mở
            // Chuyển các controls về mặc định
            lbl_Home.Text = "HOME";
            pn_home.BackColor = Color.FromArgb(0, 150, 136);
            pn_logo.BackColor = Color.FromArgb(39, 39, 58);
            nutchay = null;
            btn_CloseFormCon.Visible = false; // Tắt nút Close Form Con
        }
        // Tạo sự kiện nhấn kéo ở Panel Home, sẽ di chuyển Form theo
        private void pn_home_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        // Tạo sự kiện đóng ứng dụng
        private void btn_CloseHome_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Tạo sự kiện mở to,nhỏ Form
        private void Btn_MoToHome_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) //Kiểm tra nếu đang ở kiểu bình thường thì phòng to còn ngược lại thì phóng bình thường lại
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }
        // Tạo sự kiện thu nhỏ Form
        private void btn_ThuNhoHome_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Tạo sự kiện click nút Thống kê
        private void btn_ThongKe_Click(object sender, EventArgs e)
        {
            tilte = "Thống kê";
            MoFormCon(new ThongKe(), sender);
        }
        //Tạo sự kiện click nút Thông tin các học viên
        private void btn_InFoHV_Click(object sender, EventArgs e)
        {
            tilte = "Thông tin các học viên";
            MoFormCon(new ThongTinCacHocVien(), sender);
        }
        //Tạo sự kiện click nút Thông tin các giáo viên
        private void btn_lienlacgv_Click(object sender, EventArgs e)
        {
            tilte = "Thông tin các giáo viên";
            MoFormCon(new ThongTinCacGiaoVien(), sender);
        }
        //Tạo sự kiện click nút Thông tin các lớp học
        private void btn_InFoClass_Click(object sender, EventArgs e)
        {
            tilte = "Thông tin các lớp học";
            MoFormCon(new ThongTinCacLopHoc(), sender);
        }
        //Tạo sự kiện click nút Thêm giáo viên
        private void btn_ThemGV_Click(object sender, EventArgs e)
        {
            tilte = "Thêm giáo viên";
            MoFormCon(new ThemGiaoVien(), sender);
        }
        //Tạo sự kiện click nút Thêm lớp
        private void btn_ThemLop_Click(object sender, EventArgs e)
        {
            tilte = "Thêm lớp học";
            MoFormCon(new ThemLopHoc(), sender);
        }
        //Tạo sự kiện click Thông tin nộp học phí
        private void NopHP_Click(object sender, EventArgs e)
        {
            tilte = "Thông tin các học viên chưa nộp học phí";
            MoFormCon(new ChuaNopHocPhi(), sender);
        }
    }
}
