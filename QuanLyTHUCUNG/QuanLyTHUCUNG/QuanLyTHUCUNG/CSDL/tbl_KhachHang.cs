namespace QuanLyTHUCUNG.CSDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_KhachHang()
        {
        }

        [Key]
        public long MaKH { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        public int? SDT { get; set; }

        [StringLength(50)]
        public string Loai { get; set; }

    }
}
