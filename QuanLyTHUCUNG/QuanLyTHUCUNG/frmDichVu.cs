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
    public partial class frmDichVu : Form
    {
        QLThuCungDBContext db;
        public frmDichVu()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        void loadData()
        {
            var data = db.tbl_DichVu.ToList();
            if (data != null && data.Count() > 0)
            {
                tblDV.DataSource = data;
            }
        }
        private void txtMaDV_TextChanged(object sender, EventArgs e)
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtGia.Text = "";
            txtTenDV.Text = "";
        }
        bool KTDL()
        {
            int check = 0;
            if (txtTenDV.Text == "")
            {
                MessageBox.Show("Tên Dịch Vụ không được bỏ trống!!!");
                txtTenDV.Focus();
                return false;
            }
            else if (txtGia.Text == "")
            {
                MessageBox.Show("Giá không được bỏ trống!!!");
                txtGia.Focus();
                return false;
            }
            else if (!int.TryParse(txtGia.Text, out check))
            {
                MessageBox.Show("Giá  phải là số!!!");
                txtGia.Focus();
                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them)
            {
                if (KTDL())
                {
                    try
                    {
                        tbl_DichVu dichVu = new tbl_DichVu();
                        dichVu.TenDichVu = txtTenDV.Text;
                        dichVu.Gia = float.Parse(txtGia.Text);
                        db.tbl_DichVu.Add(dichVu);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblDV.Refresh();
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
                if (KTDL() && txtMaDV.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaDV.Text);
                        var chuong = db.tbl_DichVu.Find(id);
                        chuong.TenDichVu = txtTenDV.Text;
                        chuong.Gia = float.Parse(txtGia.Text);
                       
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblDV.Refresh();
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

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            loadData();
            txtMaDV.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (txtMaDV.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaDV.Text);
                        var chuong = db.tbl_DichVu.Find(id);
                        db.tbl_DichVu.Remove(chuong);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Thành Công");
                        tblDV.Refresh();
                        loadData();
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

        private void tblDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaDV.Text = tblDV[0, e.RowIndex].Value.ToString();
            txtTenDV.Text = tblDV[1, e.RowIndex].Value.ToString();
            txtGia.Text = tblDV[2, e.RowIndex].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }
    }
}
