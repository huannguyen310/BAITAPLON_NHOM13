using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThietBiDienTu.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_XuatKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XuatKho",
                columns: table => new
                {
                    MaHH = table.Column<string>(type: "TEXT", nullable: false),
                    TenHH = table.Column<string>(type: "TEXT", nullable: false),
                    MaNCC = table.Column<string>(type: "TEXT", nullable: false),
                    TenNCC = table.Column<string>(type: "TEXT", nullable: false),
                    MaXuatKho = table.Column<string>(type: "TEXT", nullable: false),
                    NgayXuatKho = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrangThai = table.Column<string>(type: "TEXT", nullable: false),
                    MaKH = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatKho", x => x.MaHH);
                    table.ForeignKey(
                        name: "FK_XuatKho_HangHoa_MaHH",
                        column: x => x.MaHH,
                        principalTable: "HangHoa",
                        principalColumn: "MaHH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XuatKho_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XuatKho_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XuatKho_MaKH",
                table: "XuatKho",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_XuatKho_MaNCC",
                table: "XuatKho",
                column: "MaNCC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XuatKho");
        }
    }
}
