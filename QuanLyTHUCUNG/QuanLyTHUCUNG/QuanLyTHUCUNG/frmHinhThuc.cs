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
    public partial class frmHinhThuc : Form
    {
        QLThuCungDBContext db;
        public frmHinhThuc()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        void loadData()
        {
            var data = db.tbl_HinhThuc.ToList();
            if (data != null && data.Count() > 0)
            {
                tblHT.DataSource = data;
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

        private void frmHinhThuc_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtGia.Text = "";
            txtMaHT.Text = "";
        }

        bool KTDL()
        {
            int check = 0;
          
            if (txtGia.Text == "")
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
                        tbl_HinhThuc dichVu = new tbl_HinhThuc();
                        dichVu.Loai = cboLoai.Text;
                        dichVu.Gia = float.Parse(txtGia.Text);
                        db.tbl_HinhThuc.Add(dichVu);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblHT.Refresh();
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
                if (KTDL() && txtMaHT.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaHT.Text);
                        var chuong = db.tbl_HinhThuc.Find(id);
                        chuong.Gia = float.Parse(txtGia.Text);

                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblHT.Refresh();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KTDL() && txtMaHT.Text != "")
            {
                try
                {
                    long id = Convert.ToInt64(txtMaHT.Text);
                    var chuong = db.tbl_HinhThuc.Find(id);
                    db.tbl_HinhThuc.Remove(chuong);
                    db.SaveChanges();
                    MessageBox.Show("Xóa Thành Công");
                    tblHT.Refresh();
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

        private void btnThem_Click(object sender, EventArgs e)
        {

            them = true;
            sua = false;
            hide(false);
        }
    }
}
