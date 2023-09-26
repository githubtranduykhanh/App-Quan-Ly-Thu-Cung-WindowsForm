namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_HinhThuc
    {
        public tbl_HinhThuc()
        {
        }

        [Key]
        public long MaHinhThuc { get; set; }

        [StringLength(50)]
        public string Loai { get; set; }

        public double? Gia { get; set; }

    }
}
