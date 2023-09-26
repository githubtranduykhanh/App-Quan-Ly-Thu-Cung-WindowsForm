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
    public partial class frmNhanVien : Form
    {
        QLThuCungDBContext db;
        public frmNhanVien()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        void loadData()
        {
            var data = db.tbl_NhanVien.ToList();
            if (data != null && data.Count() > 0)
            {
                tblNhanVien.AutoGenerateColumns = false;
                tblNhanVien.DataSource = data;
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
        static string fileAnh;
        private string matKhauCu = "";
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            them = true;
            sua = false;
            hide(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            hide(true);
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTDN.Text = "";
            txtMatKhau.Text = "";
        }
        bool KTDL()
        {
            int check = 0;
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Tên Khách Hàng không được bỏ trống!!!");
                txtTenNV.Focus();
                return false;
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Dịa Chỉ không được bỏ trống!!!");
                txtDiaChi.Focus();
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
                        tbl_NhanVien nhanVien = new tbl_NhanVien();
                        nhanVien.TenNV = txtTenNV.Text;
                        nhanVien.DiaChi = txtDiaChi.Text;
                        nhanVien.SDT = Convert.ToInt32(txtSDT.Text);
                        nhanVien.GioiTinh = cboGioiTinh.Text;
                        nhanVien.NgaySinh = dateNgaySinh.Value;
                        nhanVien.LoaiNV = cboLoaiNV.Text;
                        nhanVien.HinhAnh = fileAnh;
                        nhanVien.GhiChu = txtGhiChu.Text;
                        db.tbl_NhanVien.Add(nhanVien);
                        db.SaveChanges();



                        tbl_TaiKhoan taiKhoan = new tbl_TaiKhoan();
                        taiKhoan.MaNV = nhanVien.MaNV;
                        taiKhoan.TenTK = txtTDN.Text;
                        taiKhoan.MatKhau = QuanLyTHUCUNG.MaHoa.MaHoaMD5(txtMatKhau.Text);
                        if (nhanVien.LoaiNV == "Chủ Tiệm")
                        {
                            taiKhoan.Quyen = "0";
                        }
                        else
                        {
                            taiKhoan.Quyen = "1";
                        }
                        db.tbl_TaiKhoan.Add(taiKhoan);
                        db.SaveChanges();
                        MessageBox.Show("Thêm Mới Thành Công");
                        tblNhanVien.Refresh();
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
                if (txtMaNV.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaNV.Text);
                        var nhanVienEdit = db.tbl_NhanVien.Find(id);
                        nhanVienEdit.TenNV = txtTenNV.Text;
                        nhanVienEdit.DiaChi = txtDiaChi.Text;
                        nhanVienEdit.SDT = Convert.ToInt32(txtSDT.Text);
                        nhanVienEdit.GioiTinh = cboGioiTinh.Text;
                        nhanVienEdit.NgaySinh = dateNgaySinh.Value;
                        nhanVienEdit.LoaiNV = cboLoaiNV.Text;
                        nhanVienEdit.HinhAnh = fileAnh;
                        nhanVienEdit.GhiChu = txtGhiChu.Text;
                        db.SaveChanges();
                        var taiKhoanEdit = db.tbl_TaiKhoan.Where(x => x.MaNV == id).First();
                        taiKhoanEdit.TenTK = txtTDN.Text;
                        if(txtMatKhau.Text != "")
                        {
                            matKhauCu = QuanLyTHUCUNG.MaHoa.MaHoaMD5(txtMatKhau.Text);
                        }                      
                        taiKhoanEdit.MatKhau = matKhauCu;
                        if (nhanVienEdit.LoaiNV == "Chủ Tiệm")
                        {
                            taiKhoanEdit.Quyen = "0";
                        }
                        else
                        {
                            taiKhoanEdit.Quyen = "1";
                        }
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblNhanVien.Refresh();
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
        private void taiKhoanById(string id)
        {
            long manv = Convert.ToInt64(id);
            var taiKhoanByIdData = db.tbl_TaiKhoan.Where(x => x.MaNV == manv).FirstOrDefault();
            txtTDN.Text = taiKhoanByIdData.TenTK;
            txtMatKhau.Text = "";
            matKhauCu = taiKhoanByIdData.MatKhau;
        }
        private void tblNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            txtMaNV.Text = tblNhanVien[0, e.RowIndex].Value.ToString();
            taiKhoanById(tblNhanVien[0, e.RowIndex].Value.ToString());
            txtTenNV.Text = tblNhanVien[1, e.RowIndex].Value.ToString();
            txtDiaChi.Text = tblNhanVien[2, e.RowIndex].Value.ToString();
            cboGioiTinh.Text = tblNhanVien[3, e.RowIndex].Value.ToString();
            dateNgaySinh.Value = DateTime.Parse(tblNhanVien[4, e.RowIndex].Value.ToString());
            cboLoaiNV.Text = tblNhanVien[5, e.RowIndex].Value.ToString();
            txtSDT.Text = tblNhanVien[6, e.RowIndex].Value.ToString();
            fileAnh = tblNhanVien[7, e.RowIndex].Value.ToString();
            var path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\" + "Img\\";
            ptbHinhanh.Image = Image.FromFile(path + fileAnh);
            txtGhiChu.Text = tblNhanVien[8, e.RowIndex].Value.ToString();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            loadData();
            txtMaNV.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                if (txtMaNV.Text != "")
                {
                    try
                    {
                        long id = Convert.ToInt64(txtMaNV.Text);
                        var deleteNhanVien = db.tbl_NhanVien.Find(id);
                        db.tbl_NhanVien.Remove(deleteNhanVien);
                        db.SaveChanges();
                        MessageBox.Show("Chỉnh sửa Thành Công");
                        tblNhanVien.Refresh();
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chỉnh sửa KHÔNG Thành Công !! Kiểm tra lại thông tin");
                    }

                }
            }
                
        }

        private void ptbHinhanh_Click(object sender, EventArgs e)
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
