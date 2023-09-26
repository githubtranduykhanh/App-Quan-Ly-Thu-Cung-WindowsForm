using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.Charts.WinForms;
using QuanLyTHUCUNG.CSDL;

namespace QuanLyTHUCUNG
{
    class SplineArea
    {
        private static QLThuCungDBContext db = new QLThuCungDBContext();
        public static void Example(Guna.Charts.WinForms.GunaChart chart)
        {
            
            string[] months = { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"};
            
            //Chart configuration 
            chart.YAxes.GridLines.Display = false;

            //Create a new dataset 
            var dataset = new Guna.Charts.WinForms.GunaSplineAreaDataset();
            dataset.PointRadius = 3;
            dataset.PointStyle = PointStyle.Circle;           


            int dtYear = DateTime.Now.Year;                               
            for (int i = 0; i <= months.Length; i++)
            {
                if(i > 0)
                {
                    var dataMonth = db.tbl_PhieuGui.Where(x => x.NgayGui.Value.Year == dtYear && x.NgayGui.Value.Month == i && x.TrangThai == "Đã nhận").ToList();
                    
                    double number = 0;
                    if (dataMonth != null && dataMonth.Count > 0)
                    {

                        foreach (var item in dataMonth)
                        {
                            number += (double)item.TongTien;
                        }

                    }
                    dataset.DataPoints.Add(months[i - 1], number);
                }       
            }


            //var r = new Random();
            //for (int i = 0; i < months.Length; i++)
            //{
            //    //random number
            //    int num = r.Next(10, 100);

            //    dataset.DataPoints.Add(months[i], num);
            //}
            //Add a new dataset to a chart.Datasets
            chart.Datasets.Add(dataset);
            //An update was made to re-render the chart
            chart.Update();
        }
    }
}
