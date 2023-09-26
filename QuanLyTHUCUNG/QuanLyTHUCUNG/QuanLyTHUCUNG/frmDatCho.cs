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
    public partial class frmDatCho : Form
    {
        QLThuCungDBContext db;
        public frmDatCho()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        void loadChuong()
        {
            var data = db.tbl_Chuong.ToList();
            if (data.Count() > 0)
            {
                cboCHuong.DataSource = data;
                cboCHuong.DisplayMember = "TenChuong";
                cboCHuong.ValueMember = "MaChuong";
                cboCHuong.SelectedValue = 0;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }
        bool them, sua;

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtSDT.Text = "";
            txtSL.Text = "";
            txtTenKH.Text = "";
        }
        bool KTDL()
        {
            int check = 0;
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Tên khách hàng không được bỏ trống!!!");
                txtTenKH.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("SDT không được bỏ trống!!!");
                txtSDT.Focus();
                return false;
            }
            else if (txtSL.Text == "")
            {
                MessageBox.Show("Số lượng không được bỏ trống!!!");
                txtSL.Focus();
                return false;
            }
            return true;
        }

        void loadData()
        {
            var data = db.tbl_DatCho.ToList();
            if (data.Count() > 0)
            {
                tblDatCho.DataSource = data;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
                if (KTDL())
                {
                    try
                    {
                        tbl_DatCho info = new tbl_DatCho();
                        info.TenKhachHang = txtTenKH.Text;
                        info.SDT = Convert.ToInt32(txtSDT.Text);
                        info.SoLuong = Convert.ToInt32(txtSL.Text);
                        info.NgayDat = dateNgayDat.Value;
                        info.NgaySD = dateNgaySD.Value;
                        info.MaChuong = Convert.ToInt64(cboCHuong.SelectedValue.ToString());
                        db.tbl_DatCho.Add(info);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblDatCho.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }
                }
            }
            if (sua)
            {
                //if (KTDL() && txtMaChuong.Text != "")
                //{
                //    try
                //    {
                //        long id = Convert.ToInt64(txtMaChuong.Text);
                //        var chuong = db.tbl_Chuong.Find(id);
                //        chuong.TenChuong = txtTenchuong.Text;
                //        chuong.Gia = float.Parse(txtGia.Text);
                //        chuong.LoaiChuong = cboloaichuong.Text;
                //        chuong.TrangThai = cboTrangThai.Text;
                //        db.SaveChanges();
                //        MessageBox.Show("Thêm Mới Thành Công");
                //        tblChuong.Refresh();
                //        loadData();
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
                //}
            }
        }

        private void frmDatCho_Load(object sender, EventArgs e)
        {
            loadChuong();
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }
    }
}
