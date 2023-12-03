using Microsoft.EntityFrameworkCore;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<HangHoa> HangHoa { get; set; }
    }
}