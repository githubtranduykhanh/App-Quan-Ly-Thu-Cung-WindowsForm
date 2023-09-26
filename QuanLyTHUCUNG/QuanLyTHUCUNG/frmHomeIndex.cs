using Guna.UI2.WinForms;
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
    public partial class frmHomeIndex : Form
    {
        public frmHomeIndex()
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
        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 118, b.Location.Y - 30);
            imgSlide.SendToBack();
        }
        private void guna2Button1_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            frmDangNhap.maLogin = 0;
            frmDangNhap login = new frmDangNhap();
            login.Show();
            this.Hide();
            
        }

        private void frmHomeIndex_Load(object sender, EventArgs e)
        {

            guna2ShadowForm1.SetShadowForm(this);
            callform(new CuttomChuong());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            callform(new CuttomChuong());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            callform(new frmPhieuGui());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            callform(new frmDatCho());
        }
     
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            callform(new frmKhachHang());
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            callform(new frmHoaDon());
        }
    }
}
