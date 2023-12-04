using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThietBiDienTu.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_NhapKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhapKho",
                columns: table => new
                {
                    MaNhapKho = table.Column<string>(type: "TEXT", nullable: false),
                    MaHH = table.Column<string>(type: "TEXT", nullable: false),
                    SoLuong = table.Column<int>(type: "INTEGER", nullable: false),
                    NgayNhapKho = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaNCC = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKho", x => x.MaNhapKho);
                    table.ForeignKey(
                        name: "FK_NhapKho_HangHoa_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhapKho_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhapKho_MaHH",
                table: "NhapKho",
                column: "MaHH");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKho_MaNCC",
                table: "NhapKho",
                column: "MaNCC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhapKho");
        }
    }
}
