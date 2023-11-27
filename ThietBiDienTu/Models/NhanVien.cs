using System.ComponentModel.DataAnnotations;

namespace ThietBiDienTu.Models
{
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