namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TaiKhoan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MaNV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TenTK { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string Quyen { get; set; }

        public virtual tbl_NhanVien tbl_NhanVien { get; set; }
    }
}
