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
        public delegate void Action(bool isAction = false);
        public Action closeAction;
        public frmNhatKyNuoi(string maTC = null)
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
            matc = maTC;
            txtThuCung.Text = tenThuCuongByid(Convert.ToInt64(matc));

        }
        public static string matc;
        public static bool isLoading = false;
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
        private string tenThuCuongByid(long idTC)
        {
            string kq = "";
            var data = db.tbl_ThuCungKyGui.Find(idTC);
            if(data != null)
            {
                kq = data.Giong;
            }
            return kq;
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
                    info.MaSoThuCung = Convert.ToInt64(matc);
                    info.Ngay = dateNgay.Value;
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
                    isLoading = true;
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
                        info.Ngay = dateNgay.Value;
                        info.Buoi = cboBuoi.Text;
                        info.TrangThai = cboTrangThai.Text;
                        info.GhiChu = txtGhiChu.Text;
                        info.DichVu = cboDV.Text;
                        info.ThanhTien = Convert.ToDouble(txtThanhTien.Text);
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblNhatKy.Refresh();
                        loadData();
                        isLoading = true;
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
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (txtMaNK.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaNK.Text);

                        var info = db.tbl_NhatKyNuoi.Find(id);
                        db.tbl_NhatKyNuoi.Remove(info);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Thành Công");
                        tblNhatKy.Refresh();
                        loadData();
                        isLoading = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
                }
            }
               
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDV_SizeChanged(object sender, EventArgs e)
        {

        }
        double giaDV(long idDV)
        {
            double kq = 0;
            var data = db.tbl_DichVu.Find(idDV);
            if (data != null)
            {
                kq = (double)data.Gia;          
            }
            return kq;
        }
        private void cboDV_SelectionChangeCommitted(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(cboDV.SelectedValue);
            var data = db.tbl_DichVu.Find(id);
            if (data != null)
            {
                lblGiaDV.Text = data.Gia + "";
                txtThanhTien.Text = data.Gia + "";
            }
        }

        private void tblNhatKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNK.Text = tblNhatKy[0, e.RowIndex].Value.ToString();
            txtThuCung.Text = tenThuCuongByid(Convert.ToInt64(tblNhatKy[1, e.RowIndex].Value.ToString()));
            dateNgay.Value = DateTime.Parse(tblNhatKy[2, e.RowIndex].Value.ToString());
            cboBuoi.SelectedValue = tblNhatKy[3, e.RowIndex].Value;
            cboTrangThai.SelectedValue = tblNhatKy[4, e.RowIndex].Value;
            txtGhiChu.Text = tblNhatKy[5, e.RowIndex].Value.ToString();
            cboDV.Text = tblNhatKy[6, e.RowIndex].Value.ToString();
            txtThanhTien.Text = tblNhatKy[7, e.RowIndex].Value.ToString();
            lblGiaDV.Text = giaDV(Convert.ToInt64(cboDV.SelectedValue)) + "";
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            if(closeAction != null)
            {
                closeAction(true);
            }
        }

        private void frmNhatKyNuoi_Load(object sender, EventArgs e)
        {
            loadDichVu();
            loadData();
        }
    }
}
