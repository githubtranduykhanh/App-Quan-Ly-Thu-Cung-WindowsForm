namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Chuong
    {
        public tbl_Chuong()
        {
        }

        [Key]
        public long MaChuong { get; set; }

        [StringLength(50)]
        public string TenChuong { get; set; }

        [StringLength(50)]
        public string LoaiChuong { get; set; }

        public double? Gia { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

    }
}
