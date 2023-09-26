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
    public partial class frmHoaDon : Form
    {
        QLThuCungDBContext db;
        public frmHoaDon()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        void loadData()
        {
            var data = db.tbl_HoaDon.OrderByDescending(x => x.MaHD).ToList();
            if (data != null && data.Count() > 0)
            {
                tblHD.AutoGenerateColumns = false;
                tblHD.DataSource = data;
            }
        }
        void loadPhieuGui()
        {
            var data = db.tbl_PhieuGui.Where(x => x.TrangThai == "Đã nhận").ToList();
            if (data != null && data.Count() > 0)
            {
                cboMaPG.DataSource = data;
                cboMaPG.ValueMember = "MaPhieuGui";
                cboMaPG.DisplayMember = "MaPhieuGui";
            }
        }
        void loadNhanVien()
        {
            var data = db.tbl_NhanVien.Where(x => x.LoaiNV == "Nhân Viên").ToList();
            if (data != null && data.Count() > 0)
            {
                cboMaNV.DataSource = data;
                cboMaNV.ValueMember = "MaNV";
                cboMaNV.DisplayMember = "TenNV";
            }
        }
        void hide(bool tt)
        {
            
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        bool them, sua;
        bool KTDL()
        {
            int check = 0;      
            if (txtTongTien.Text == "")
            {
                MessageBox.Show("Tổng tiền không được bỏ trống!!!");
                txtTongTien.Focus();
                return false;
            }            
            return true;
        }
        private void txtMaDC_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtMaHD.Text = "";
            cboMaPG.Text = "";
            cboMaNV.Text = "";
            txtTongTien.Text = "";
            cboTrangThai.Text = "";
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
     
            if (sua)
            {
                if (txtMaHD.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaHD.Text);
                        var hoaDonEdit = db.tbl_HoaDon.Find(id);
                        hoaDonEdit.MaPhieuGui = Convert.ToInt64(cboMaPG.SelectedValue);
                        hoaDonEdit.NgayLap = dateNgayLap.Value;
                        hoaDonEdit.MaNV = Convert.ToInt64(cboMaNV.SelectedValue);
                        hoaDonEdit.TrangThai = cboTrangThai.Text;
                        hoaDonEdit.TongTien = Convert.ToInt64(txtTongTien.Text);
                      
                        db.SaveChanges();                      
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblHD.Refresh();
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

        private void tblHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = tblHD[0, e.RowIndex].Value.ToString();
            cboMaPG.SelectedValue = tblHD[1, e.RowIndex].Value;
            dateNgayLap.Value = DateTime.Parse(tblHD[2, e.RowIndex].Value.ToString());
            cboMaNV.SelectedValue = tblHD[3, e.RowIndex].Value;
            txtTongTien.Text = tblHD[4, e.RowIndex].Value.ToString();
            cboTrangThai.Text = tblHD[5, e.RowIndex].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {

                if (txtMaHD.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaHD.Text);
                        var deleteHoaDon = db.tbl_HoaDon.Find(id);
                        db.tbl_HoaDon.Remove(deleteHoaDon);
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblHD.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chỉnh sửa KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }

                }
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(cboMaPG.SelectedValue);
            frmInPhieuGui inPhieu = new frmInPhieuGui();
            inPhieu.id = id;
            inPhieu.Show();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(cboMaPG.SelectedValue);
            frmInNhatKy inPhieu = new frmInNhatKy();
            inPhieu.id = id;
            inPhieu.Show();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            loadData();
            loadPhieuGui();
            loadNhanVien();
            txtMaHD.Enabled = false;
        }
    }
}
