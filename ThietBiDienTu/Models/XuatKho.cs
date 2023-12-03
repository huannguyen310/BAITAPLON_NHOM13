using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("XuatKho")]
    public class XuatKho
    {
        [Key]
        public string MaHH { get; set; }
        [ForeignKey("MaHH")]
        public HangHoa? HangHoa { get; set; }
        public string TenHH { get; set; }
        public string MaNCC { get; set; }
        [ForeignKey("MaNCC")]
        public NhaCungCap? NhaCungCap { get; set; }
        public string TenNCC { get; set; }
        public string MaXuatKho { get; set; }
        public DateTime NgayXuatKho { get; set; }
        public string TrangThai { get; set; }
        public string MaKH { get; set; }
        [ForeignKey("MaKH")]
        public KhachHang? KhachHang { get; set; }
    }
}