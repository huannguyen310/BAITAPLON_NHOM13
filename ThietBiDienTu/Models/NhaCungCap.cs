using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("NhaCungCap")]
    public class NhaCungCap
    {
        [Key]
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }
}