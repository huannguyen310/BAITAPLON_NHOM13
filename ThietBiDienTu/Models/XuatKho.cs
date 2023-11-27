using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    public class XuatKho
    {
        [Key]
        public string? MaHH { get; set; }
        [ForeignKey("MaHH")]
        public HangHoa? HangHoa { get; set; }
        public string? TenHH { get; set; }
        public string? MaNCC { get; set; }
        [ForeignKey("MaNCC")]
        public NhaCungCap? NhaCungCap { get; set; }
        public string? TenNCC { get; set; }
        public string? TrangThai { get; set; }
        public string? MaXK { get; set; }
        public string? MaKH { get; set; }
        [ForeignKey("MaKH")]
        public KhachHang? KhachHang { get; set; }
    }
}