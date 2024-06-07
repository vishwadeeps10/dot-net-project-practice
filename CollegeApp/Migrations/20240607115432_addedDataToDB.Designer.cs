﻿// <auto-generated />
using System;
using CollegeApp.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollegeApp.Migrations
{
    [DbContext(typeof(CollegeDbContext))]
    [Migration("20240607115432_addedDataToDB")]
    partial class addedDataToDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CollegeApp.data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Added_On")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_of_birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entollment_no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fathers_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Added_On = new DateTime(2024, 6, 7, 17, 24, 31, 719, DateTimeKind.Local).AddTicks(9808),
                            Address = "Patna Bihar",
                            Category = 3,
                            Date_of_birth = new DateTime(1998, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vishwadeep@gmail.com",
                            Entollment_no = "123456731",
                            Fathers_name = "Shankar Singh",
                            Gender = 0,
                            Name = "Vishwadeep Singh"
                        },
                        new
                        {
                            Id = 2,
                            Added_On = new DateTime(2024, 6, 7, 17, 24, 31, 719, DateTimeKind.Local).AddTicks(9822),
                            Address = "Chamtha Bihar",
                            Category = 3,
                            Date_of_birth = new DateTime(1999, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "rohit@gmail.com",
                            Entollment_no = "123456731E",
                            Fathers_name = "Nagendra Singh",
                            Gender = 0,
                            Name = "Rohit Singh"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}