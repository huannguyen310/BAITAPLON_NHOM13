using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public string MaHH { get; set; }
        public string TenHH { get; set; }
        public string HangSX { get; set; }
        public string XuatXu { get; set; }
        public int DonGia { get; set; }
    }
}