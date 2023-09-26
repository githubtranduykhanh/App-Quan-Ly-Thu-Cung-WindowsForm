using QuanLyTHUCUNG.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTHUCUNG
{
    public partial class frmThuKyGui : Form
    {
        QLThuCungDBContext db;
        public frmThuKyGui(string makh = null)
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
            maKh = makh;
            txtKH.Text = makh;
        }
        public static string maKh;

        void hide(bool tt)
        {
            btnThem.Enabled = tt;
            btnSua.Enabled = tt;
            btnLuu.Enabled = !tt;
            btnXoa.Enabled = tt;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        bool them, sua;

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            sua = true;
            hide(false);
        }
        static string fileAnh;

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            sua = false;
            hide(false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtCanNang.Text = "";
            txtGiongTC.Text = "";
            txtTTSK.Text = "";
        }

        bool KTDL()
        {
            int check = 0;
            if (txtGiongTC.Text == "")
            {
                MessageBox.Show("Giống thú cưng không được bỏ trống!!!");
                txtGiongTC.Focus();
                return false;
            }
            else if (txtCanNang.Text == "")
            {
                MessageBox.Show("Cân nặng không được bỏ trống!!!");
                txtCanNang.Focus();
                return false;
            }
            else if (txtTTSK.Text == "")
            {
                MessageBox.Show("Thông tin sức khỏe không được bỏ trống!!!");
                txtTTSK.Focus();
                return false;
            }
            return true;
        }
        void loadData()
        {
            long id = Convert.ToInt64(maKh);
            var data = db.tbl_ThuCungKyGui.Where(x => x.MaKH == id ).ToList();
            if(data!= null && data.Count() > 0)
            {
                tblTC.DataSource = data;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (them)
            {
                if (KTDL())
                {
                    try
                    {
                        tbl_ThuCungKyGui info = new tbl_ThuCungKyGui();
                        info.MaKH = Convert.ToInt64(txtKH.Text);
                        info.Giong = txtGiongTC.Text ;
                        info.Loai = cboloaiTC.Text ;
                        info.CanNang = float.Parse(txtCanNang.Text);
                        info.TinhTrangSK = txtTTSK.Text ;
                        info.HinhAnh = fileAnh;
                        info.TrangThai = "Chưa nhận";
                        db.tbl_ThuCungKyGui.Add(info);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblTC.Refresh();
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
                if (KTDL() && txtMaTC.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaTC.Text);

                        var info = db.tbl_ThuCungKyGui.Find(id);
                        info.Giong = txtGiongTC.Text;
                        info.Loai = cboloaiTC.Text;
                        info.CanNang = float.Parse(txtCanNang.Text);
                        info.TinhTrangSK = txtTTSK.Text;
                        info.HinhAnh = fileAnh;
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblTC.Refresh();
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

        private void tblTC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaTC.Text = tblTC[0, e.RowIndex].Value.ToString();
            txtGiongTC.Text = tblTC[1, e.RowIndex].Value.ToString();
            cboloaiTC.Text = tblTC[2, e.RowIndex].Value.ToString();
            txtCanNang.Text = tblTC[3, e.RowIndex].Value.ToString();
            txtTTSK.Text = tblTC[4, e.RowIndex].Value.ToString();
            fileAnh= tblTC[6, e.RowIndex].Value.ToString();
            var path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\" + "Img\\";
            ptbHinhanh.Image = Image.FromFile(path + fileAnh);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (txtMaTC.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaTC.Text);

                        var info = db.tbl_ThuCungKyGui.Find(id);
                        db.tbl_ThuCungKyGui.Remove(info);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Thành Công");
                        tblTC.Refresh();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThuKyGui_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (txtMaTC.Text != "")
            {
                frmNhatKyNuoi nhatKy = new frmNhatKyNuoi(txtMaTC.Text);
                nhatKy.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 đối tượng trên bảng để thao tác");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Img\"; // <---
            OpenFileDialog dalOpen = new OpenFileDialog();
            dalOpen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            dalOpen.FilterIndex = 2;
            dalOpen.Title = "Anh Minh Hoa";
            if (dalOpen.ShowDialog(this) == DialogResult.OK)
            {
                ptbHinhanh.Image = Image.FromFile(dalOpen.FileName);
                try
                {
                    System.IO.File.Copy(dalOpen.FileName, appPath + dalOpen.SafeFileName);
                    fileAnh = dalOpen.FileName.Substring(dalOpen.FileName.LastIndexOf("\\") + 1);
                }
                catch
                {
                    fileAnh = dalOpen.FileName.Substring(dalOpen.FileName.LastIndexOf("\\") + 1);
                }
            }
        }
    }
}
