using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [Required(ErrorMessage = "Mã nhân viên không được để trống!")]
        public string? MaNV { get; set; }
        [Required(ErrorMessage = "Tên nhân viên không được để trống!")]
        public string? TenNV { get; set; }
        public string? DiaChi { get; set; }
        public int SDT { get; set; }
    }
}