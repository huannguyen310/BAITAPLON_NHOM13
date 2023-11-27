using System.ComponentModel.DataAnnotations;

namespace ThietBiDienTu.Models
{
    public class NhaCungCap
    {
        [Key]
        [Required(ErrorMessage = "Mã NCC không được để trống!")]
        public string? MaNCC { get; set; }
        [Required(ErrorMessage = "Tên NCC không được để trống!")]
        public string? TenNCC { get; set; }
        public int SDT { get; set; }
        public string? DiaChi { get; set;}
        public string? Email { get; set; }
    }
}