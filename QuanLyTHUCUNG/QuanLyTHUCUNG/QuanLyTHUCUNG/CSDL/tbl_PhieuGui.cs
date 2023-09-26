namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_PhieuGui
    {
        public tbl_PhieuGui()
        {
        }

        [Key]
        public long MaPhieuGui { get; set; }

        public long MaKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayGui { get; set; }

        public long MaHinhThuc { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public double? TongTien { get; set; }




    }
}
