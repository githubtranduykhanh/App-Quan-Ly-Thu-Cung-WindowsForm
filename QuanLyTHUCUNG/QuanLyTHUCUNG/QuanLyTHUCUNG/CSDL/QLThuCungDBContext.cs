using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyTHUCUNG.CSDL
{
    public partial class QLThuCungDBContext : DbContext
    {
        public QLThuCungDBContext()
            : base("name=QLThuCungDBContext1")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbl_ChiTietPhieuGui> tbl_ChiTietPhieuGui { get; set; }
        public virtual DbSet<tbl_Chuong> tbl_Chuong { get; set; }
        public virtual DbSet<tbl_DatCho> tbl_DatCho { get; set; }
        public virtual DbSet<tbl_DichVu> tbl_DichVu { get; set; }
        public virtual DbSet<tbl_HinhThuc> tbl_HinhThuc { get; set; }
        public virtual DbSet<tbl_HoaDon> tbl_HoaDon { get; set; }
        public virtual DbSet<tbl_KhachHang> tbl_KhachHang { get; set; }
        public virtual DbSet<tbl_NhanVien> tbl_NhanVien { get; set; }
        public virtual DbSet<tbl_NhatKyNuoi> tbl_NhatKyNuoi { get; set; }
        public virtual DbSet<tbl_PhieuGui> tbl_PhieuGui { get; set; }
        public virtual DbSet<tbl_TaiKhoan> tbl_TaiKhoan { get; set; }
        public virtual DbSet<tbl_ThuCungKyGui> tbl_ThuCungKyGui { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

           
        }
    }
}
