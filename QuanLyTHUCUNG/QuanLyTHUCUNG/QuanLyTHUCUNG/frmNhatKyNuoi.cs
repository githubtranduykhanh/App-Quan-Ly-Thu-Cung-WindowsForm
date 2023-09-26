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
    public partial class frmNhatKyNuoi : Form
    {
        QLThuCungDBContext db;
        public frmNhatKyNuoi(string maTC = null)
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
            matc = maTC;
            txtThuCung.Text = matc;

        }
        public static string matc;

        void loadDichVu()
        {
            var data = db.tbl_DichVu.ToList();
            if(data!=null && data.Count() > 0)
            {
                cboDV.DataSource = data;
                cboDV.DisplayMember = "TenDichVu";
                cboDV.ValueMember = "MaDV";
                cboDV.SelectedIndex = 0;
            }
        }
        void loadData()
        {
            long id = Convert.ToInt64(matc);
            var data = db.tbl_NhatKyNuoi.Where(x=>x.MaSoThuCung == id).ToList();
            if (data != null && data.Count() > 0)
            {
                tblNhatKy.DataSource = data;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }

        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        bool them, sua;

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
                    tbl_NhatKyNuoi info = new tbl_NhatKyNuoi();
                    info.MaSoThuCung = Convert.ToInt64(txtThuCung.Text);
                    info.Ngay = DateTime.Now;
                    info.Buoi = cboBuoi.Text;
                    info.TrangThai = cboTrangThai.Text;
                    info.GhiChu = txtGhiChu.Text;
                    info.DichVu = cboDV.Text;
                    info.ThanhTien = Convert.ToDouble(txtThanhTien.Text) ;
                    db.tbl_NhatKyNuoi.Add(info);
                    db.SaveChanges();
                    MessageBox.Show("Thêm Mới Thành Công");
                    tblNhatKy.Refresh();
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                }
            }
            if (sua)
            {
                if (txtMaNK.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaNK.Text);

                        var info = db.tbl_NhatKyNuoi.Find(id);
                        info.Ngay = DateTime.Now;
                        info.Buoi = cboBuoi.Text;
                        info.TrangThai = cboTrangThai.Text;
                        info.GhiChu = txtGhiChu.Text;
                        info.DichVu = cboDV.Text;
                        info.ThanhTien = Convert.ToDouble(txtThanhTien.Text);
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblNhatKy.Refresh();
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
            txtThanhTien.Text = "";
            txtGhiChu.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNK.Text != "")
            {
                try
                {
                    long id = Convert.ToInt64(txtMaNK.Text);

                    var info = db.tbl_NhatKyNuoi.Find(id);
                    db.tbl_NhatKyNuoi.Remove(info);
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa Thành Công");
                    tblNhatKy.Refresh();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDV_SizeChanged(object sender, EventArgs e)
        {

        }

        private void cboDV_SelectionChangeCommitted(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(cboDV.SelectedValue);
            var data = db.tbl_DichVu.Find(id);
            if (data != null)
            {
                lblGiaDV.Text = data.Gia + "";
            }
        }

        private void frmNhatKyNuoi_Load(object sender, EventArgs e)
        {
            loadDichVu();
            loadData();
        }
    }
}
