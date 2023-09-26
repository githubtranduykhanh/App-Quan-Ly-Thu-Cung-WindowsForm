using QuanLyTHUCUNG.CSDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.Charts.WinForms;

namespace QuanLyTHUCUNG
{
    class Pie
    {
        private static QLThuCungDBContext db = new QLThuCungDBContext();      
        private static double loadPhieuGuiDaNhan(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd && x.TrangThai == "Đã nhận").ToList();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong+= (double)item.TongTien;
                }
            }
            return soLuong;
        }
        private static double loadPhieuChuaNhan(string start, string end)
        {
            double soLuong = 0;
            DateTime dtStart = Convert.ToDateTime(start);
            DateTime dtEnd = Convert.ToDateTime(end);
            var data = db.tbl_PhieuGui.Where(x => x.NgayGui >= dtStart && x.NgayGui <= dtEnd && x.TrangThai == "Chưa nhận").ToList();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    soLuong += (double)item.TongTien;
                }
            }
            return soLuong;
        }
       
        public static void Example(Guna.Charts.WinForms.GunaChart chart, string startDate, string endDate)
        {
            
            string[] options = { "Phiếu Gửi Đã Nhận", "Phiếu Gửi Chưa Nhận"};

            //Chart configuration  
            chart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            chart.XAxes.Display = false;
            chart.YAxes.Display = false;

            //Create a new dataset 
            var dataset = new Guna.Charts.WinForms.GunaPieDataset();
           
            for (int i = 0; i <= options.Length; i++)
            {
                if (i > 0)
                {
                    if (i == 1)
                    {
                        dataset.DataPoints.Add(options[i - 1], loadPhieuGuiDaNhan(startDate, endDate));
                    }
                    else if(i == 2)
                    {
                        dataset.DataPoints.Add(options[i - 1], loadPhieuChuaNhan(startDate, endDate));
                    }                                      
                }
            }

            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);

            //An update was made to re-render the chart
            chart.Update();
        }
    }
}
