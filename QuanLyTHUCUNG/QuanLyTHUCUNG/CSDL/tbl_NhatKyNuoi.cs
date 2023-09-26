namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_NhatKyNuoi
    {
        [Key]
        public long MaNhatKy { get; set; }

        public long MaSoThuCung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        [StringLength(10)]
        public string Buoi { get; set; }

        [StringLength(100)]
        public string TrangThai { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        [StringLength(100)]
        public string DichVu { get; set; }

        public double? ThanhTien { get; set; }

    }
}
