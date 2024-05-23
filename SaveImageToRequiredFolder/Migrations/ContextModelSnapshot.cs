﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaveImageToRequiredFolder.Data;

#nullable disable

namespace SaveImageToRequiredFolder.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("SaveImageToRequiredFolder.Models.Image", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("data")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("folderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
