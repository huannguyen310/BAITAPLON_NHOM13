using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThietBiDienTu.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_HangHoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHH = table.Column<string>(type: "TEXT", nullable: false),
                    TenHH = table.Column<string>(type: "TEXT", nullable: true),
                    ThongTinHH = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHH);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");
        }
    }
}
