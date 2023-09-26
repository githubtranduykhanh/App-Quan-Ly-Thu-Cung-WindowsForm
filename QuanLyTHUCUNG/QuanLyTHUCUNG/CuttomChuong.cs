using QuanLyTHUCUNG.CSDL;
using QuanLyTHUCUNG.CuttomControl;
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
    public partial class CuttomChuong : Form
    {
        QLThuCungDBContext db;
        public CuttomChuong()
        {
            InitializeComponent();
            db = new QLThuCungDBContext();           
        }
        private void HandlAction(bool isAction = false)
        {
            if (isAction == true)
            {
                loadData(idChuong);
            }
        }
        private void loadCuttomChuong()
        {
            flowLayoutPanel1.Controls.Clear();

            var Chuong = db.tbl_Chuong.ToList();
            if(Chuong != null && Chuong.Count > 0)
            {
                ChuongControl[] listChuong = new ChuongControl[Chuong.Count];



                for (int i = 0; i < 1; i++)
                {
                    
                    foreach(var item in Chuong)
                    {
                        listChuong[i] = new ChuongControl();


                        listChuong[i].Title = item.LoaiChuong;
                        listChuong[i].Subtitle = item.TenChuong;
                        listChuong[i].MaChuong = item.MaChuong;
                        listChuong[i].TrangThai = item.TrangThai;


                        flowLayoutPanel1.Controls.Add(listChuong[i]);

                        listChuong[i].Click += new System.EventHandler(this.ChuongControl_Click);
                    }
                }
            }
        }
        private long masothucung = 0;
        private long idNhatKyNuoi = 0;
        private long idChuong = 0;
        private long idPhieu = 0; 
        //private long idChiTietPhieu = 0;
        private void Clear(bool isClear = false)
        {
            if(isClear == false)
            {
                idPhieu = 0;
                idChuong = 0;
                masothucung = 0;
                tblListChuong.DataSource = new
                {
                    ID = "",
                    Giong = "",
                    DichVu = "",
                    TrangThai = "",
                    ThanhTien = ""
                };
                lblTongTien.Text = string.Format("{0:0,000}", 0) + "đ";
            }
            else
            {
                tblListChuong.DataSource = new
                {
                    ID = "",
                    Giong = "",
                    DichVu = "",
                    TrangThai = "",
                    ThanhTien = ""
                };
                lblTongTien.Text = string.Format("{0:0,000}", 0) + "đ";
            }
            
        }
        void loadData(long machuong)
        {
            tblListChuong.Refresh();
            double tongTien = 0;
            var checkPhieuGui = db.tbl_ChiTietPhieuGui.Where(x => x.MaChuong == machuong).OrderByDescending(x => x.MaPhieuGui).FirstOrDefault();
            var chuong = db.tbl_Chuong.Find(machuong);
            if(chuong.TrangThai != "Đầy")
            {
                btnThanhToan.Enabled = false;
                InHoaDonChuong.Enabled = false;
            }
            else
            {
                btnThanhToan.Enabled = true;
                InHoaDonChuong.Enabled = true;
            }
            lbltenchuong.Text = chuong.TenChuong;
            lblgiachuong.Text = string.Format("{0:0,000}", chuong.Gia) + "đ";
            if (checkPhieuGui != null)
            {
                var phieuGui = db.tbl_PhieuGui.Find(checkPhieuGui.MaPhieuGui);
                if (phieuGui.TrangThai == "Chưa nhận")
                {
                    var checkThuCung = db.tbl_ThuCungKyGui.Find(checkPhieuGui.MaSoThuCung);
                    if (checkThuCung.TrangThai == "Chưa nhận")
                    {
                        idPhieu = phieuGui.MaPhieuGui;
                        if (checkThuCung.MaSoThuCung != masothucung)
                        {
                            idChuong = machuong;
                            masothucung = checkThuCung.MaSoThuCung;
                        }

                        var innerJoin = db.tbl_ChiTietPhieuGui.Where(x => x.MaChuong == machuong && x.MaPhieuGui == checkPhieuGui.MaPhieuGui).Join(// outer sequence 
                      db.tbl_ThuCungKyGui.Where(x => x.TrangThai == "Chưa nhận"),  // inner sequence 
                      chitietpieu => chitietpieu.MaSoThuCung,    // outerKeySelector
                      thukugui => thukugui.MaSoThuCung,  // innerKeySelector
                      (chitietpieu, thukugui) => new  // result selector
                      {
                          MaSoThuCung = thukugui.MaSoThuCung,
                          Giong = thukugui.Giong,
                      }).Join(db.tbl_NhatKyNuoi.Where(x =>x.Ngay >= phieuGui.NgayGui),
                       dataNew => dataNew.MaSoThuCung,
                       nhatkynuoi => nhatkynuoi.MaSoThuCung,
                       (dataNew, nhatkynuoi) => new {
                           ID = nhatkynuoi.MaNhatKy,
                           Giong = dataNew.Giong,
                           DichVu = nhatkynuoi.DichVu,
                           TrangThai = nhatkynuoi.TrangThai,
                           ThanhTien = nhatkynuoi.ThanhTien
                       }
                      ).ToList();
                        if (innerJoin != null && innerJoin.Count() > 0)
                        {
                            foreach (var item in innerJoin)
                            {
                                if (item.TrangThai == "Đã hoàn thành")
                                {
                                    tongTien += (double)item.ThanhTien;
                                }
                            }
                            tongTien += (double)chuong.Gia;
                            tblListChuong.DataSource = innerJoin;                           
                            lblTongTien.Text = string.Format("{0:0,000}", tongTien) + "đ" /*+ checkPhieuGui.MaPhieuGui.ToString() + "TTT OK"*/;
                        }
                        else
                        {
                            Clear(true);
                        }
                    }
                    else
                    {
                        Clear(false);
                    }
                    
                }
                else
                {
                    Clear(false);
                }
            }
            else
            {
                Clear(false);
            }
                     
        }
        void ChuongControl_Click(object sender, EventArgs e)
        {
            idNhatKyNuoi = 0;
            ChuongControl obj = (ChuongControl)sender;          
            loadData(obj.MaChuong);

            //var idthukygui = db.tbl_ChiTietPhieuGui.Where(x => x.MaChuong == obj.MaChuong).OrderByDescending(x => x.MaPhieuGui).FirstOrDefault();           
            //if(idthukygui != null)
            //{               
            //    var phieuGui = db.tbl_PhieuGui.Find(idthukygui.MaPhieuGui);
            //    if(phieuGui.TrangThai == "Chưa nhận")
            //    {
                    
            //        var thuCung = db.tbl_ThuCungKyGui.Find(idthukygui.MaSoThuCung);
            //        if(thuCung.TrangThai  == "Chưa nhận")
            //        {
            //            idPhieu = phieuGui.MaPhieuGui;
            //            if (idthukygui.MaSoThuCung != masothucung)
            //            {
            //                idChuong = obj.MaChuong;
            //                masothucung = idthukygui.MaSoThuCung;
            //            }
            //        }
            //        else
            //        {
            //            idPhieu = 0;
            //            idChuong = 0;
            //            masothucung = 0;
            //        }               
            //    }
            //    else
            //    {
            //        idPhieu = 0;
            //        idChuong = 0;
            //        masothucung = 0;
            //    }
            //}
            //else
            //{
            //    idPhieu = 0;
            //    idChuong = 0;
            //    masothucung = 0;              
            //}

            //var data = db.tbl_ChiTietPhieuGui.Where(x => x.MaChuong == obj.MaChuong).ToList();
            //if (data != null && data.Count() > 0)
            //{
            //    foreach(var item in data)
            //    {
            //        tongTien += (double)item.TongTien;
            //    }

            //    tblListChuong.DataSource = data;
            //    lblTongTien.Text = string.Format("{0:0,000}", tongTien) + "đ";
            //}           
        }

        private void CuttomChuong_Load(object sender, EventArgs e)
        {
            loadCuttomChuong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(masothucung != 0)
            {
                frmNhatKyNuoi nhatKy = new frmNhatKyNuoi(masothucung.ToString());
                nhatKy.closeAction = new frmNhatKyNuoi.Action(HandlAction);
                nhatKy.Show();
            }
            else
            {
                MessageBox.Show("Chuồng không có thú ký gửi");
            }
        }

        private void tblListChuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idNhatKyNuoi = (long)tblListChuong[0, e.RowIndex].Value;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa ?", "Xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(res == DialogResult.OK)
            {
                if (idNhatKyNuoi != 0)
                {
                    try
                    {
                        var info = db.tbl_NhatKyNuoi.Find(idNhatKyNuoi);
                        db.tbl_NhatKyNuoi.Remove(info);
                        db.SaveChanges();
                        MessageBox.Show("Xóa Thành Công");
                        loadData(idChuong);
                        idNhatKyNuoi = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa KHÔNG Thành Công !! Kiểm tra lại thông tin");
                        idNhatKyNuoi = 0;
                    }

                }
                else
                {
                    MessageBox.Show(" Vui lòng chọn thông tin muốn xóa");
                }
            }          
        }

        private void InHoaDonChuong_Click(object sender, EventArgs e)
        {
            
            frmInHoaDonChuong inHoaDon = new frmInHoaDonChuong();
            inHoaDon.idChuong = idChuong;
            inHoaDon.idPhieuGui = idPhieu;
            inHoaDon.Show();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (idChuong != 0)
                {
                    try
                    {
                        var chuong = db.tbl_Chuong.Find(idChuong);
                        chuong.TrangThai = "Trống";
                        db.SaveChanges();
                        if (masothucung != 0)
                        {
                            try
                            {
                                var thucung = db.tbl_ThuCungKyGui.Find(masothucung);
                                thucung.TrangThai = "Đã nhận";
                                db.SaveChanges();
                                var nhatky = db.tbl_NhatKyNuoi.Where(x => x.MaSoThuCung == masothucung).ToList();
                                if (nhatky != null && nhatky.Count > 0)
                                {
                                    foreach (var item in nhatky)
                                    {
                                        db.tbl_NhatKyNuoi.Remove(item);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Thanh Toan KHÔNG Thành Công !! Xoa Nhat KY khong thanh cong");
                            }
                            if (idPhieu != 0)
                            {
                                try
                                {
                                    var chitietphieu = db.tbl_ChiTietPhieuGui.Where(x => x.MaSoThuCung == masothucung && x.MaChuong == idChuong && x.MaPhieuGui == idPhieu).FirstOrDefault();
                                    if (chitietphieu != null)
                                    {
                                        try
                                        {
                                            var phieugui = db.tbl_PhieuGui.Find(idPhieu);
                                            phieugui.TongTien -= chitietphieu.TongTien;
                                            db.SaveChanges();
                                            db.tbl_ChiTietPhieuGui.Remove(chitietphieu);
                                            db.SaveChanges();
                                            MessageBox.Show("Thanh Toan Thanh Cong");
                                            loadCuttomChuong();
                                            loadData(idChuong);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Thanh Toan KHÔNG Thành Công !! Ko the tru TONG TIEN");
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Thanh Toan KHÔNG Thành Công !! Ko co chi tiet phieu gui");
                                }

                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Thanh Toan KHÔNG Thành Công !! Chuong da trong");
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Thanh Toan KHÔNG Thành Công !! Kiểm tra lại thông tin");
            }
                 
        }
    }
}
