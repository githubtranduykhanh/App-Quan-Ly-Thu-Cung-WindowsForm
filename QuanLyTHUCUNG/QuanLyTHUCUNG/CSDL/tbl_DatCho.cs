namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DatCho
    {
        [Key]
        public long MaDatCho { get; set; }

        public long MaChuong { get; set; }

        [StringLength(50)]
        public string TenKhachHang { get; set; }

        public int? SDT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySD { get; set; }

        public int? SoLuong { get; set; }

    }
}
