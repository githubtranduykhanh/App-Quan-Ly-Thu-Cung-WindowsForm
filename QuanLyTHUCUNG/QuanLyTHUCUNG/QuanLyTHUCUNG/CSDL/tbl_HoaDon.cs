namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_HoaDon
    {
        [Key]
        public long MaHD { get; set; }

        public long MaPhieuGui { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        public long MaNV { get; set; }

        public double? TongTien { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public virtual tbl_NhanVien tbl_NhanVien { get; set; }

        public virtual tbl_PhieuGui tbl_PhieuGui { get; set; }
    }
}
