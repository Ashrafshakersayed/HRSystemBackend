﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HR_Employees.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230610120934_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HR_Employees.Entities.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerID")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ManagerID");

                    b.ToTable("Employees", "MasterData");
                });

            modelBuilder.Entity("HR_Employees.Entities.WorkingHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SigninTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SignoutTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("WorkingHours")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("WorkingHours", "MasterData");
                });

            modelBuilder.Entity("HR_Employees.Entities.Employee", b =>
                {
                    b.HasOne("HR_Employees.Entities.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerID");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("HR_Employees.Entities.WorkingHour", b =>
                {
                    b.HasOne("HR_Employees.Entities.Employee", "Employee")
                        .WithMany("WorkingHours")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HR_Employees.Entities.Employee", b =>
                {
                    b.Navigation("WorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
