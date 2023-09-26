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
    public partial class frmPhieuGui : Form
    {
        QLThuCungDBContext db;
        public frmPhieuGui()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        void loadKhachHang()
        {
            var data = db.tbl_KhachHang.ToList();
            if(data!=null && data.Count() > 0)
            {
                cboKH.DataSource = data;
                cboKH.DisplayMember = "TenKH";
                cboKH.ValueMember= "MaKH";
            }
        }
        void loadData()
        {
            var data = db.tbl_PhieuGui.Where(x => x.TrangThai.Contains("Chưa nhận")).ToList();
            if(data!=null && data.Count() > 0)
            {
                tblPhieuGui.DataSource = data;
            }
        }
        void loadHinhThuc()
        {
            var data = db.tbl_HinhThuc.ToList();
            if(data!=null && data.Count() > 0)
            {
                cboHT.DataSource = data;
                cboHT.ValueMember = "MaHinhThuc";
                cboHT.DisplayMember = "Loai";
            }
        }
        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        bool them, sua;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
          
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if(txtMaPhieuGui.Text != "")
            {
                frmchitietPhieuGui frmchitietPhieuGui = new frmchitietPhieuGui(txtMaPhieuGui.Text);
                frmchitietPhieuGui.Show();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
               
                try
                {
                    tbl_PhieuGui dichVu = new tbl_PhieuGui();
                    dichVu.MaHinhThuc = Convert.ToInt64(cboHT.SelectedValue);
                    dichVu.MaKH = Convert.ToInt64(cboKH.SelectedValue);
                    dichVu.NgayGui = dateNgayGui.Value;
                    dichVu.TrangThai = "Chưa nhận";
                    dichVu.TongTien = 0;

                    db.tbl_PhieuGui.Add(dichVu);

                    db.SaveChanges();
                    MessageBox.Show("Thêm Mới Thành Công");
                    tblPhieuGui.Refresh();
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                }
            }
            if (sua)
            {
                if (txtMaPhieuGui.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaPhieuGui.Text);
                        var dichVu = db.tbl_PhieuGui.Find(id);
                        dichVu.MaHinhThuc = Convert.ToInt64(cboHT.SelectedValue);
                        dichVu.MaKH = Convert.ToInt64(cboKH.SelectedValue);
                        dichVu.TrangThai = cboTrangThai.Text ;

                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblPhieuGui.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chỉnh sửa KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtMaPhieuGui.Text = "";
            txtTongTien.Text = "";
        }

        private void tblPhieuGui_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhieuGui.Text = tblPhieuGui[0, e.RowIndex].Value.ToString();

        }

        private void frmPhieuGui_Load(object sender, EventArgs e)
        {
            loadHinhThuc();
            loadKhachHang();
            loadData();
        }
    }
}
