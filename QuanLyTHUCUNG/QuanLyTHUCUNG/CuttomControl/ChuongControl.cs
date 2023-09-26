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

namespace QuanLyTHUCUNG.CuttomControl
{
    public partial class ChuongControl : UserControl
    {
        QLThuCungDBContext db;
        public ChuongControl()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
        }
        private Image _icon;
        private string _title;
        private string _subtitle;
        private long _machuong;
        private string _trangthai;
        

        [Category ("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; pb_icon.Image = value; }
        }
        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; lblLoaiChuong.Text = value; }
        }
        [Category("Custom Props")]
        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; lblTenChuong.Text = value; }
        }
        [Category("Custom Props")]
        public long MaChuong
        {
            get { return _machuong; }
            set { _machuong = value;}
        }
        [Category("Custom Props")]
        public string TrangThai
        {
            get { return _trangthai; }
            set { _trangthai = value; lblTThai.Text = value;

                if (_trangthai == "Đầy")
                {
                    //this.Cursor = Cursor.Current;
                    //var chiTietPhieuByChuong = db.tbl_ChiTietPhieuGui.Where(x => x.MaChuong == this.MaChuong).OrderByDescending(x => x.MaPhieuGui).FirstOrDefault();
                    //if(chiTietPhieuByChuong != null)
                    //{
                    //    var path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\" + "Img\\";
                    //    var imgThuCung = db.tbl_ThuCungKyGui.Find(chiTietPhieuByChuong.MaSoThuCung);
                    //    this.BackgroundImage = Image.FromFile(path + imgThuCung.HinhAnh);
                    //    this.BackColor = Color.Transparent;
                    //    this.BackgroundImageLayout = ImageLayout.Stretch;
                    //    lblLoaiChuong.ForeColor = Color.FromArgb(0, 118, 212);
                    //    lblTenChuong.ForeColor = Color.FromArgb(0, 118, 212);
                    //    lblTThai.ForeColor = Color.FromArgb(0, 118, 212);
                    //}
                    //else
                    //{
                    //    this.BackColor = Color.FromArgb(247, 114, 147);
                      
                    //}
                    this.Cursor = Cursor.Current;
                    this.BackColor = Color.FromArgb(247, 114, 147);

                }
                else if (_trangthai == "Đã Đặt")
                {
                    this.BackColor = Color.FromArgb(144, 150, 170);
                }
                else
                {
                    this.BackColor = Color.FromArgb(0, 118, 212);
                }
            }
        }
    }
}
