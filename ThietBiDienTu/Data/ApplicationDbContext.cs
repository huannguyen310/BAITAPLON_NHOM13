using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<HangHoa> HangHoa { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<NhapKho> NhapKho { get; set; }
    }
}