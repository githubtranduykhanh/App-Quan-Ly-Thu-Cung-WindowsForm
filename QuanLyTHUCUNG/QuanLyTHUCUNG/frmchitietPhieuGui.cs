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
    public partial class frmchitietPhieuGui : Form
    {
        QLThuCungDBContext db;
        public delegate void Action(bool isAction = false);
        public Action closeAction;
        public frmchitietPhieuGui(string maPhieuGui = null, long maKhachHang = 0)
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
            id = maPhieuGui;
            idKH = maKhachHang;
            txtMaPhieuGui.Text = id;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if(closeAction != null)
            {
                closeAction(true);
                this.Close();
            }          
        }
        public static string id;
        public static long idKH;
        void loadThuCung()
        {
            var data = db.tbl_ThuCungKyGui.Where(x => x.MaKH == idKH).ToList();
            if(data!=null && data.Count() > 0)
            {
                cboTC.DataSource = data;
                cboTC.DisplayMember = "Giong";
                cboTC.ValueMember = "MaSoThuCung";
            }
        }
        void loadData()
        {
            long ma = Convert.ToInt64(id);
            var data = db.tbl_ChiTietPhieuGui.Where(x => x.MaPhieuGui == ma).ToList();
            if(data!=null && data.Count() > 0)
            {
                tblCTPG.DataSource = data;
            }
        }
        void loadChuongAdd()
        {
            var data = db.tbl_Chuong.Where(x => x.TrangThai == "Trống").ToList();
            if (data != null && data.Count() > 0)
            {
                cbochuong.DataSource = data;
                cbochuong.ValueMember = "MaChuong";
                cbochuong.DisplayMember = "TenChuong";
            }
        }
        void loadChuong()
        {
            var data = db.tbl_Chuong.ToList();
            if(data!=null && data.Count() > 0)
            {
                cbochuong.DataSource = data;
                cbochuong.ValueMember= "MaChuong";
                cbochuong.DisplayMember = "TenChuong";
            }
        }

        private void frmchitietPhieuGui_Load(object sender, EventArgs e)
        {
            loadThuCung();
            loadChuong();
            loadData();
        }
        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnLuu.Enabled = !tt;

        }
        bool them, sua;

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            loadChuong();
            hide(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
                
                try
                {
                    tbl_ChiTietPhieuGui dichVu = new tbl_ChiTietPhieuGui();
                    dichVu.MaPhieuGui = Convert.ToInt64(txtMaPhieuGui.Text);
                    dichVu.MaSoThuCung = Convert.ToInt64(cboTC.SelectedValue);
                    dichVu.MaChuong = Convert.ToInt64(cbochuong.SelectedValue);
                    dichVu.TongTien = Convert.ToInt64(txtTongTien.Text);

                    db.tbl_ChiTietPhieuGui.Add(dichVu);

                    db.SaveChanges();
                    MessageBox.Show("Thêm Mới Thành Công");
                    var chuongTrangThai = db.tbl_Chuong.Find(dichVu.MaChuong);
                    chuongTrangThai.TrangThai = "Đầy";
                    db.SaveChanges();
                    var thuKygui = db.tbl_ThuCungKyGui.Find(dichVu.MaSoThuCung);
                    thuKygui.TrangThai = "Chưa nhận";
                    db.SaveChanges();
                    long id = Convert.ToInt64(txtMaPhieuGui.Text);
                    var phieuGui = db.tbl_PhieuGui.Find(id);
                    double? tong = phieuGui.TongTien;
                    phieuGui.TongTien = tong+ Convert.ToDouble(txtTongTien.Text);
                    db.SaveChanges();
                    tblCTPG.Refresh();
                    loadData();
                    loadChuongAdd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                }
            }
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuGui.Text != "")
            {
                
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
        }

        private void cbochuong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbochuong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            long idChuong = Convert.ToInt64(cbochuong.SelectedValue);
            var chuong = db.tbl_Chuong.Find(idChuong);
            txtTongTien.Text = chuong.Gia + "";
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(txtMaPhieuGui.Text);
            frmInPhieuGui inPhieu = new frmInPhieuGui();
            inPhieu.id = id;
            inPhieu.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(txtMaPhieuGui.Text);
            frmInNhatKy inPhieu = new frmInNhatKy();
            inPhieu.id = id;
            inPhieu.Show();
        }

        private void tblCTPG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhieuGui.Text = tblCTPG[0, e.RowIndex].Value.ToString();
            cboTC.SelectedValue = tblCTPG[1, e.RowIndex].Value;
            cbochuong.SelectedValue = tblCTPG[2, e.RowIndex].Value;
            txtTongTien.Text = tblCTPG[3, e.RowIndex].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            loadChuongAdd();
            them = true;
            sua = false;
            hide(false);
        }
    }
}
