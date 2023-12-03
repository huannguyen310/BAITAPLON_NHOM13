using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("NhapKho")]
    public class NhapKho
    {
        [Key]
        public string MaNhapKho { get; set; }
        public string MaHH { get; set; }
        [ForeignKey("MaHH")]
        public HangHoa? HangHoa { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayNhapKho { get; set; }
        public string MaNCC { get; set; }
        [ForeignKey("MaNCC")]
        public NhaCungCap? NhaCungCap { get; set; }
    }
}