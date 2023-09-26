namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ThuCungKyGui
    {
        public tbl_ThuCungKyGui()
        {
        }

        [Key]
        public long MaSoThuCung { get; set; }

        [StringLength(50)]
        public string Giong { get; set; }

        [StringLength(50)]
        public string Loai { get; set; }

        public double? CanNang { get; set; }

        [StringLength(50)]
        public string TinhTrangSK { get; set; }

        public long MaKH { get; set; }

        [Column(TypeName = "ntext")]
        public string HinhAnh { get; set; }

        [StringLength(100)]
        public string TrangThai { get; set; }

    }
}
