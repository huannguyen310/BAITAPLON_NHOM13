using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [Required(ErrorMessage = "Mã khách hàng không được để trống!")]
        public string? MaKH { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        public string? TenKH { get; set; }
        public string? DiaChi { get; set; }
        public int SDT { get; set; }
    }
}