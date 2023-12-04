﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThietBiDienTu.Data;

#nullable disable

namespace ThietBiDienTu.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204153858_Create_table_NhapKho")]
    partial class Create_table_NhapKho
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ThietBiDienTu.Models.HangHoa", b =>
                {
                    b.Property<string>("MaHH")
                        .HasColumnType("TEXT");

                    b.Property<int>("DonGia")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HangSX")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenHH")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("XuatXu")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaHH");

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("ThietBiDienTu.Models.KhachHang", b =>
                {
                    b.Property<string>("MaKH")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenKH")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaKH");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("ThietBiDienTu.Models.NhaCungCap", b =>
                {
                    b.Property<string>("MaNCC")
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenNCC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaNCC");

                    b.ToTable("NhaCungCap");
                });

            modelBuilder.Entity("ThietBiDienTu.Models.NhanVien", b =>
                {
                    b.Property<string>("MaNV")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TKNH")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenNV")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaNV");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("ThietBiDienTu.Models.NhapKho", b =>
                {
                    b.Property<string>("MaNhapKho")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaHH")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaNCC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NgayNhapKho")
                        .HasColumnType("TEXT");

                    b.Property<int>("SoLuong")
                        .HasColumnType("INTEGER");

                    b.HasKey("MaNhapKho");

                    b.HasIndex("MaHH");

                    b.HasIndex("MaNCC");

                    b.ToTable("NhapKho");
                });

            modelBuilder.Entity("ThietBiDienTu.Models.NhapKho", b =>
                {
                    b.HasOne("ThietBiDienTu.Models.HangHoa", "HangHoa")
                        .WithMany()
                        .HasForeignKey("MaHH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThietBiDienTu.Models.NhaCungCap", "NhaCungCap")
                        .WithMany()
                        .HasForeignKey("MaNCC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HangHoa");

                    b.Navigation("NhaCungCap");
                });
#pragma warning restore 612, 618
        }
    }
}
