using System.ComponentModel.DataAnnotations;

namespace ThietBiDienTu.Models
{
    public class HangHoa
    {
        [Key]
        public string MaHH { get; set;}
        public string TenHH { get; set; }
        public string ThongTinHH { get; set; }
    }
}