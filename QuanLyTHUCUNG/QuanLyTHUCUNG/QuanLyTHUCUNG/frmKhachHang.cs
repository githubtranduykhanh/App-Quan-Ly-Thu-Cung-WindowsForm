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
    public partial class frmKhachHang : Form
    {
        QLThuCungDBContext db;
        public frmKhachHang()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        void loadData()
        {
            var data = db.tbl_KhachHang.ToList();
            if (data != null && data.Count() > 0)
            {
                tblKh.DataSource = data;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        bool them, sua;

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            hide(true);
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDC.Text = "";
            txtSDT.Text = "";
        }
        bool KTDL()
        {
            int check = 0;
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Tên Khách Hàng không được bỏ trống!!!");
                txtTenKH.Focus();
                return false;
            }
            else if (txtDC.Text == "")
            {
                MessageBox.Show("Dịa Chỉ không được bỏ trống!!!");
                txtDC.Focus();
                return false;
            }
            else if (!int.TryParse(txtSDT.Text, out check))
            {
                MessageBox.Show("SĐT  phải là số!!!");
                txtSDT.Focus();
                return false;
            }
            return true;
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
                if (KTDL())
                {
                    try
                    {
                        tbl_KhachHang dichVu = new tbl_KhachHang();
                        dichVu.TenKH = txtTenKH.Text;
                        dichVu.DiaChi= txtDC.Text;
                        dichVu.SDT= Convert.ToInt32(txtSDT.Text);
                        dichVu.Loai= cboloai.Text;
                        
                        db.tbl_KhachHang.Add(dichVu);

                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblKh.Refresh();
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
                if (txtMaKH.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaKH.Text);
                        var chuong = db.tbl_KhachHang.Find(id);
                        chuong.TenKH = txtTenKH.Text;
                        chuong.DiaChi = txtDC.Text;
                        chuong.SDT = Convert.ToInt32(txtSDT.Text);
                        chuong.Loai = cboloai.Text;
                        
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblKh.Refresh();
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

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void tblKh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = tblKh[0, e.RowIndex].Value.ToString();
            txtTenKH.Text = tblKh[1, e.RowIndex].Value.ToString();
            cboloai.Text = tblKh[2, e.RowIndex].Value.ToString();
            txtSDT.Text = tblKh[3, e.RowIndex].Value.ToString();
            txtDC.Text = tblKh[4, e.RowIndex].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text != "")
            {
                frmThuKyGui frm = new frmThuKyGui(txtMaKH.Text);
                frm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }
    }
}
