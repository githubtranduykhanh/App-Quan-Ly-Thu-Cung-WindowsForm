using QuanLyTHUCUNG.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTHUCUNG
{
    public partial class frmDangNhap : Form
    {
        QLThuCungDBContext db;
        public frmDangNhap()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        public static long maLogin = 0;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" || txtPassword.Text != "")
            {
                try
                {
                    
                    string Password = QuanLyTHUCUNG.MaHoa.MaHoaMD5(txtPassword.Text);
                    var login = db.tbl_TaiKhoan.Where(x => x.TenTK == txtUserName.Text && x.MatKhau == Password).FirstOrDefault();
                    if (login != null)
                    {

                        if(login.Quyen == "0")
                        {
                            maLogin = login.MaNV;
                            Loading _load = new Loading();
                            _load.quyen = "0";
                            _load.Show();
                            this.Hide();
                        }
                        if (login.Quyen == "1")
                        {
                            maLogin = login.MaNV;
                            Loading _load = new Loading();
                            _load.quyen = "1";
                            _load.Show();
                            this.Hide();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Tên Đăng Nhâp Hoặc Mật Khẩu Ko Chính Xác !!!");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đăng Nhập KHÔNG Thành Công !! Kiểm tra lại thông tin");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
            }
            //Loading _load = new Loading();
            //_load.Show();
            //this.Hide(); 
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
