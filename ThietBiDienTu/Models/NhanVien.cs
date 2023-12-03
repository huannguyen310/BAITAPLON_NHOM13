using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string ChucVu { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string TKNH { get; set; }
    }
}