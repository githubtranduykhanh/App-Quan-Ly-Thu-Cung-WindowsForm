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
        private void HandlAction(bool isAction = false)
        {
            if (isAction == true)
            {
                loadData();
            }
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
        private string loadKhachHangById(int id)
        {
            string kq = "";
            var data = db.tbl_KhachHang.Where(x => x.MaKH == id).First();
            if (data != null)
            {
                kq = data.TenKH.ToString();
            }
            return kq;
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
                long idKH = Convert.ToInt64(cboKH.SelectedValue);
                frmchitietPhieuGui frmchitietPhieuGui = new frmchitietPhieuGui(txtMaPhieuGui.Text, Convert.ToInt64(cboKH.SelectedValue));
                frmchitietPhieuGui.closeAction = new frmchitietPhieuGui.Action(HandlAction);
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
                    tbl_PhieuGui phieuGuiAdd = new tbl_PhieuGui();
                    phieuGuiAdd.MaHinhThuc = Convert.ToInt64(cboHT.SelectedValue);
                    phieuGuiAdd.MaKH = Convert.ToInt64(cboKH.SelectedValue);
                    phieuGuiAdd.NgayGui = dateNgayGui.Value;
                    phieuGuiAdd.TrangThai = "Chưa nhận";
                    phieuGuiAdd.TongTien = 0;

                    db.tbl_PhieuGui.Add(phieuGuiAdd);

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
                    if (cboTrangThai.Text == "Đã nhận")
                    {
                        try
                        {
                            long id = Convert.ToInt64(txtMaPhieuGui.Text);
                            var phieuGui = db.tbl_PhieuGui.Find(id);
                            phieuGui.MaHinhThuc = Convert.ToInt64(cboHT.SelectedValue);
                            phieuGui.MaKH = Convert.ToInt64(cboKH.SelectedValue);
                            phieuGui.NgayGui = dateNgayGui.Value;
                            phieuGui.TrangThai = cboTrangThai.Text;
                            //dichVu.TongTien = Convert.ToInt64(txtTongTien.Text);
                            db.SaveChanges();                            
                            tbl_HoaDon hoaDon = new tbl_HoaDon();
                            hoaDon.MaPhieuGui = phieuGui.MaPhieuGui;
                            hoaDon.NgayLap = DateTime.Now;
                            hoaDon.MaNV = frmDangNhap.maLogin;
                            hoaDon.TongTien = phieuGui.TongTien;
                            hoaDon.TrangThai = "Chưa In Bill";
                            db.tbl_HoaDon.Add(hoaDon);
                            db.SaveChanges();
                            var chiTietPhieuGui = db.tbl_ChiTietPhieuGui.Where(x => x.MaPhieuGui == phieuGui.MaPhieuGui).ToList();
                            if(chiTietPhieuGui != null && chiTietPhieuGui.Count > 0)
                            {
                                foreach(var item in chiTietPhieuGui)
                                {
                                    var chuong = db.tbl_Chuong.Find(item.MaChuong);
                                    chuong.TrangThai = "Trống";
                                    db.SaveChanges();
                                    var thukygui = db.tbl_ThuCungKyGui.Find(item.MaSoThuCung);
                                    thukygui.TrangThai = "Đã nhận";
                                    db.SaveChanges();
                                }
                            }                                                      
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
                        try
                        {
                            long id = Convert.ToInt64(txtMaPhieuGui.Text);
                            var hoaDon = db.tbl_PhieuGui.Find(id);
                            hoaDon.MaHinhThuc = Convert.ToInt64(cboHT.SelectedValue);
                            hoaDon.MaKH = Convert.ToInt64(cboKH.SelectedValue);
                            hoaDon.NgayGui = dateNgayGui.Value;
                            hoaDon.TrangThai = cboTrangThai.Text;
                            //dichVu.TongTien = Convert.ToInt64(txtTongTien.Text);
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
            txtTongTien.Text = tblPhieuGui[5, e.RowIndex].Value.ToString();
            cboKH.SelectedValue = tblPhieuGui[1, e.RowIndex].Value;
            cboHT.SelectedValue = tblPhieuGui[3, e.RowIndex].Value;
            dateNgayGui.Value = DateTime.Parse(tblPhieuGui[2, e.RowIndex].Value.ToString());
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (txtMaPhieuGui.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaPhieuGui.Text);
                        var chiTietPhieu = db.tbl_ChiTietPhieuGui.Where(x => x.MaPhieuGui == id).ToList();
                        if (chiTietPhieu != null && chiTietPhieu.Count > 0)
                        {
                            foreach (var item in chiTietPhieu)
                            {
                                var chuong = db.tbl_Chuong.Find(item.MaChuong);
                                if (chuong != null)
                                {
                                    chuong.TrangThai = "Trống";
                                    db.SaveChanges();
                                }
                                db.tbl_ChiTietPhieuGui.Remove(item);
                                db.SaveChanges();
                            }

                        }
                        var phieu = db.tbl_PhieuGui.Find(id);
                        db.tbl_PhieuGui.Remove(phieu);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Thành Công");
                        tblPhieuGui.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa KHÔNG Thành Công !! Kiểm tra lại thông tin" + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
                }
            }
                
        }

        private void frmPhieuGui_Load(object sender, EventArgs e)
        {
            loadHinhThuc();
            loadKhachHang();
            loadData();
            txtMaPhieuGui.Enabled = false;
            txtTongTien.Enabled = false;
        }
    }
}
