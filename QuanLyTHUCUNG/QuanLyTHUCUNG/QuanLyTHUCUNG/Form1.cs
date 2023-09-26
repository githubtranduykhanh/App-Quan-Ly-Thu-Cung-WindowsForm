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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public void callform(object formhija)
        {
            if (this.pnlMenu.Controls.Count > 0)
                this.pnlMenu.Controls.RemoveAt(0);
            Form f = formhija as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pnlMenu.Controls.Add(f);
            this.pnlMenu.Tag = f;
            f.Show();
        }
        private void guna2Button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            callform(new frmPhieuGui());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            callform(new frmDatCho());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            callform(new frmThuKyGui());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            callform(new frmChuong());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            callform(new frmNhatKyNuoi());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            callform(new frmDichVu());
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            callform(new frmHinhThuc());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            callform(new frmKhachHang());
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            callform(new frmHoaDon());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            callform(new frmTrangChu());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            callform(new frmTrangChu());
        }
    }
}
