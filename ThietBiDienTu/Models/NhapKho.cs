using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    public class NhapKho
    {
        [Key]
        public string? MaNK { get; set; }
        public string? MaHH { get; set; }
        [ForeignKey("MaHH")]
        public HangHoa? HangHoa { get; set;}
        public string? SoLuong { get; set; }
        public DateTime NgayNhapKho { get; set; }
        public string? MaNCC { get; set; }
        [ForeignKey("MaNCC")]
        public NhaCungCap? NhaCungCap { get; set; }
    }
}