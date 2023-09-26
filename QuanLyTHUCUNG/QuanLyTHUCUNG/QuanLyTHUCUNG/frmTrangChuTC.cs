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
    public partial class frmTrangChuTC : Form
    {
        public frmTrangChuTC()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            frmPhieuGui frm = new frmPhieuGui();
            frm.Show();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

            frmDatCho frm = new frmDatCho();
            frm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

            frmChuong frm = new frmChuong();
            frm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            frmDichVu frm = new frmDichVu();
            frm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

            frmKhachHang frm = new frmKhachHang();
            frm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            frmHinhThuc frm = new frmHinhThuc();
            frm.Show();
        }
    }
}
