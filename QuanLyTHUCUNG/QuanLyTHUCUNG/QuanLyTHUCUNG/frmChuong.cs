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
    public partial class frmChuong : Form
    {
        QLThuCungDBContext db;
        public frmChuong()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {

        }
        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }
        void loadData()
        {
            var data = db.tbl_Chuong.ToList();
            if(data != null && data.Count() > 0)
            {
                tblChuong.DataSource = data;
            }
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
            hide(true);
            txtGia.Text = "";
            txtMaChuong.Text = "";
            txtTenchuong.Text = "";
        }

        bool KTDL()
        {
            int check = 0;
            if (txtTenchuong.Text == "")
            {
                MessageBox.Show("Tên chuồng không được bỏ trống!!!");
                txtTenchuong.Focus();
                return false;
            }else if (txtGia.Text == "")
            {
                MessageBox.Show("Giá không được bỏ trống!!!");
                txtGia.Focus();
                return false;
            }else if (!int.TryParse(txtGia.Text, out check))
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
                        tbl_Chuong chuong = new tbl_Chuong();
                        chuong.TenChuong = txtTenchuong.Text;
                        chuong.Gia = float.Parse(txtGia.Text);
                        chuong.LoaiChuong = cboloaichuong.Text;
                        chuong.TrangThai = "Trống";
                        db.tbl_Chuong.Add(chuong);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblChuong.Refresh();
                        loadData();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }
                }
            }
            if (sua)
            {
                if(KTDL() && txtMaChuong.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaChuong.Text);
                        var chuong = db.tbl_Chuong.Find(id);
                        chuong.TenChuong = txtTenchuong.Text;
                        chuong.Gia = float.Parse(txtGia.Text);
                        chuong.LoaiChuong = cboloaichuong.Text;
                        chuong.TrangThai = cboTrangThai.Text;
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblChuong.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Thêm Mới KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
                }
            }
        }

        private void tblChuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

       

            txtMaChuong.Text = tblChuong[0, e.RowIndex].Value.ToString();
            txtTenchuong.Text = tblChuong[1, e.RowIndex].Value.ToString();
            cboloaichuong.Text = tblChuong[2, e.RowIndex].Value.ToString();
            txtGia.Text = tblChuong[3, e.RowIndex].Value.ToString();
            cboTrangThai.Text = tblChuong[4, e.RowIndex].Value.ToString();
        
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KTDL() && txtMaChuong.Text != "")
            {
                try
                {
                    long id = Convert.ToInt64(txtMaChuong.Text);
                    var chuong = db.tbl_Chuong.Find(id);
                    db.tbl_Chuong.Remove(chuong);
                    db.SaveChanges();
                    MessageBox.Show("Xóa Thành Công");
                    tblChuong.Refresh();
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

        private void frmChuong_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }
    }
}
