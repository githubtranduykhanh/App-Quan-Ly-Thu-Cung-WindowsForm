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
using Guna.Charts.WinForms;

namespace QuanLyTHUCUNG
{
    public partial class frmThongKe : Form
    {
        QLThuCungDBContext db;
        private Guna.Charts.WinForms.GunaChart chart2;
        public frmThongKe()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();
           
        }       
        private double loadPhieuGui(string start , string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd).ToList();
            if(data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong++;
                }
            }         
            return soLuong;
        }
        private double loadDatCho(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_DatCho.Where(x => x.NgayDat >= dtStart && x.NgayDat <= dtEnd).ToList();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong++;
                }
            }
            return soLuong;
        }
        private double loadDoanhThu(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd && x.TrangThai == "Đã nhận").ToList();
            if(data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong += (double)item.TongTien;
                }
            }
            
            return soLuong;
        }
        private double loadTienLoi(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd && x.TrangThai == "Đã nhận").ToList();
            if(data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong += (double)item.TongTien;
                }
            }      
            double kq = (soLuong / 100) * 60;
            return kq;
        }
        private double loadPhanTram(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd && x.TrangThai == "Đã nhận").ToList();
            if(data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong += (double)item.TongTien;
                }
            }
            //double kqtienloi = (soLuong / 100) * 60;
            double kq = (soLuong / 5000000) * 100;
            //double numDay = dtEnd.Day - dtStart.Day;
            //double kq = (soLuong / (numDay * 500000) * 100);
            guna2CircleProgressBar1.Value = (int)kq;
            return kq;
        }
       
        private void frmThongKe_Load(object sender, EventArgs e)
        {
            
            this.gunaChart1.Datasets.Clear();
            QuanLyTHUCUNG.SplineArea.Example(this.gunaChart1);
            this.chart2 = new Guna.Charts.WinForms.GunaChart();
            panel5.Controls.Clear();
            panel5.Controls.Add(this.chart2);
            chart2.Dock = DockStyle.Fill;
            QuanLyTHUCUNG.Pie.Example(this.chart2, DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"));
            DateTimeEnd.Value = DateTime.Now;
            DateTimeStart.Value = DateTime.Now;
            label2.Text += " " + DateTime.Now.Year;
            //QuanLyTHUCUNG.Pie.Example(gunaChart2,DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"));
            lblPhieuGui.Text = loadPhieuGui(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString();
            lblDatCho.Text = loadDatCho(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString();
            lblDoanhThu.Text = string.Format("{0:0,000}", loadDoanhThu(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"))) + "đ";
            lblTienLoi.Text = string.Format("{0:0,000}", loadTienLoi(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"))) + "đ";
            lblPhamTram.Text = loadPhanTram(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString() + "%";
        }
        
        private void DateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            this.chart2.Datasets.Clear();
            QuanLyTHUCUNG.Pie.Example(this.chart2, DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"));
            lblPhieuGui.Text = loadPhieuGui(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString();
            lblDatCho.Text = loadDatCho(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString();
            lblDoanhThu.Text = string.Format("{0:0,000}", loadDoanhThu(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"))) + "đ";
            lblTienLoi.Text = string.Format("{0:0,000}", loadTienLoi(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd"))) + "đ";
            lblPhamTram.Text = loadPhanTram(DateTimeStart.Value.ToString("yyyy-MM-dd"), DateTimeEnd.Value.ToString("yyyy-MM-dd")).ToString() + "%";
        }        
    }
}
