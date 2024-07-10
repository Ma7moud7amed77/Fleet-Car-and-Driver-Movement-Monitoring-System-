﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestGP.Models;

#nullable disable

namespace TestGP.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240502143459_DriverIdCarIdTestLocation")]
    partial class DriverIdCarIdTestLocation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ImageHandling.Models.DriverTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DriverTest");
                });

            modelBuilder.Entity("TestGP.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompID");

                    b.HasIndex("MgrID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("TestGP.Models.AssCommunicateAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AdminID")
                        .HasColumnType("int");

                    b.Property<string>("AssComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AssID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfMessageAdmin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfMessageAss")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminID");

                    b.HasIndex("AssID");

                    b.ToTable("AssCommunicateAdmins");
                });

            modelBuilder.Entity("TestGP.Models.Assistant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AdminID")
                        .HasColumnType("int");

                    b.Property<int?>("CompID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminID");

                    b.HasIndex("CompID");

                    b.HasIndex("MgrID");

                    b.ToTable("Assistants");
                });

            modelBuilder.Entity("TestGP.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("AssId")
                        .HasColumnType("int");

                    b.Property<int?>("CompID")
                        .HasColumnType("int");

                    b.Property<int?>("DriverID")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LicensExpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("AssId");

                    b.HasIndex("CompID");

                    b.HasIndex("DriverID");

                    b.HasIndex("MgrID");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("TestGP.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MgrID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TestGP.Models.CurrentLocation", b =>
                {
                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentSpeed")
                        .HasColumnType("int");

                    b.Property<string>("lat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DriverId", "date");

                    b.ToTable("currentLocations");
                });

            modelBuilder.Entity("TestGP.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AdminID")
                        .HasColumnType("int");

                    b.Property<int?>("AssID")
                        .HasColumnType("int");

                    b.Property<int?>("CompID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Health_statusd")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LicenseExpDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminID");

                    b.HasIndex("AssID");

                    b.HasIndex("CompID");

                    b.HasIndex("MgrID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("TestGP.Models.DriverCommunicateAss", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AssComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfMessageAss")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfMessageDriver")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssID");

                    b.HasIndex("DriverID");

                    b.ToTable("DriverCommunicateAsses");
                });

            modelBuilder.Entity("TestGP.Models.ManageViolation", b =>
                {
                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.Property<int?>("ViolationID")
                        .HasColumnType("int");

                    b.Property<int?>("CarID")
                        .HasColumnType("int");

                    b.Property<int>("CurrentSpeed")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfViolation")
                        .HasColumnType("datetime2");

                    b.Property<string>("lat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DriverID", "ViolationID");

                    b.HasIndex("CarID");

                    b.HasIndex("ViolationID");

                    b.ToTable("ManageViolations");
                });

            modelBuilder.Entity("TestGP.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfSubscribtion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subcribtion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SuperAdminId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuperAdminId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("TestGP.Models.ReportData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("AssistantId")
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ViolationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("AssistantId");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.HasIndex("ViolationId");

                    b.ToTable("reportData");
                });

            modelBuilder.Entity("TestGP.Models.SuperAdminCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MyComp_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MyComp_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MyComp_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MyComp_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SuperAdminCompany");
                });

            modelBuilder.Entity("TestGP.Models.SuperAdminTreateManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfMgrFeedback")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfSuperFeedback")
                        .HasColumnType("datetime2");

                    b.Property<string>("MgrFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MgrID")
                        .HasColumnType("int");

                    b.Property<string>("SuperFeedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SuperID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MgrID");

                    b.HasIndex("SuperID");

                    b.ToTable("SuperAdminTreateManagers");
                });

            modelBuilder.Entity("TestGP.Models.TestLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("lan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TestLocation");
                });

            modelBuilder.Entity("TestGP.Models.Violation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarID")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("driverID")
                        .HasColumnType("int");

                    b.Property<string>("lat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarID");

                    b.HasIndex("driverID");

                    b.ToTable("Violations");
                });

            modelBuilder.Entity("TestGP.Models.carMaintenance", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("carId")
                        .HasColumnType("int");

                    b.Property<DateTime>("maintenanceDay")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("carId");

                    b.ToTable("carMaintenances");
                });

            modelBuilder.Entity("TestGP.Models.carObd2Violation", b =>
                {
                    b.Property<int>("carId")
                        .HasColumnType("int");

                    b.Property<string>("obd2Violation")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("carId", "obd2Violation");

                    b.ToTable("carObd2Violations");
                });

            modelBuilder.Entity("TestGP.Models.Admin", b =>
                {
                    b.HasOne("TestGP.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompID");

                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany("Admins")
                        .HasForeignKey("MgrID");

                    b.Navigation("Company");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TestGP.Models.AssCommunicateAdmin", b =>
                {
                    b.HasOne("TestGP.Models.Admin", "Admin")
                        .WithMany("AssCommunicateAdmin")
                        .HasForeignKey("AdminID");

                    b.HasOne("TestGP.Models.Assistant", "Assistant")
                        .WithMany("AssCommunicateAdmin")
                        .HasForeignKey("AssID");

                    b.Navigation("Admin");

                    b.Navigation("Assistant");
                });

            modelBuilder.Entity("TestGP.Models.Assistant", b =>
                {
                    b.HasOne("TestGP.Models.Admin", "Admin")
                        .WithMany("Assistants")
                        .HasForeignKey("AdminID");

                    b.HasOne("TestGP.Models.Company", "Company")
                        .WithMany("Assistants")
                        .HasForeignKey("CompID");

                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany("Assistants")
                        .HasForeignKey("MgrID");

                    b.Navigation("Admin");

                    b.Navigation("Company");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TestGP.Models.Car", b =>
                {
                    b.HasOne("TestGP.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("TestGP.Models.Assistant", "Assistant")
                        .WithMany()
                        .HasForeignKey("AssId");

                    b.HasOne("TestGP.Models.Company", "Company")
                        .WithMany("Cars")
                        .HasForeignKey("CompID");

                    b.HasOne("TestGP.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverID");

                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("MgrID");

                    b.Navigation("Admin");

                    b.Navigation("Assistant");

                    b.Navigation("Company");

                    b.Navigation("Driver");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TestGP.Models.Company", b =>
                {
                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany("Companies")
                        .HasForeignKey("MgrID");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TestGP.Models.CurrentLocation", b =>
                {
                    b.HasOne("TestGP.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("TestGP.Models.Driver", b =>
                {
                    b.HasOne("TestGP.Models.Admin", "Admin")
                        .WithMany("Drivers")
                        .HasForeignKey("AdminID");

                    b.HasOne("TestGP.Models.Assistant", "Assistant")
                        .WithMany("Drivers")
                        .HasForeignKey("AssID");

                    b.HasOne("TestGP.Models.Company", "Company")
                        .WithMany("Drivers")
                        .HasForeignKey("CompID");

                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany("Drivers")
                        .HasForeignKey("MgrID");

                    b.Navigation("Admin");

                    b.Navigation("Assistant");

                    b.Navigation("Company");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TestGP.Models.DriverCommunicateAss", b =>
                {
                    b.HasOne("TestGP.Models.Assistant", "Assistant")
                        .WithMany("DriverCommunicateAss")
                        .HasForeignKey("AssID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Driver", "Driver")
                        .WithMany("DriverCommunicateAss")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assistant");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("TestGP.Models.ManageViolation", b =>
                {
                    b.HasOne("TestGP.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarID");

                    b.HasOne("TestGP.Models.Driver", "Driver")
                        .WithMany("ManageViolation")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Violation", "Violation")
                        .WithMany()
                        .HasForeignKey("ViolationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Driver");

                    b.Navigation("Violation");
                });

            modelBuilder.Entity("TestGP.Models.Manager", b =>
                {
                    b.HasOne("TestGP.Models.SuperAdminCompany", "SuperAdminCompany")
                        .WithMany("Managers")
                        .HasForeignKey("SuperAdminId");

                    b.Navigation("SuperAdminCompany");
                });

            modelBuilder.Entity("TestGP.Models.ReportData", b =>
                {
                    b.HasOne("TestGP.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Assistant", "Assistant")
                        .WithMany()
                        .HasForeignKey("AssistantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestGP.Models.Violation", "Violation")
                        .WithMany()
                        .HasForeignKey("ViolationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Assistant");

                    b.Navigation("Car");

                    b.Navigation("Driver");

                    b.Navigation("Violation");
                });

            modelBuilder.Entity("TestGP.Models.SuperAdminTreateManager", b =>
                {
                    b.HasOne("TestGP.Models.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("MgrID");

                    b.HasOne("TestGP.Models.SuperAdminCompany", "SuperAdminCompany")
                        .WithMany()
                        .HasForeignKey("SuperID");

                    b.Navigation("Manager");

                    b.Navigation("SuperAdminCompany");
                });

            modelBuilder.Entity("TestGP.Models.Violation", b =>
                {
                    b.HasOne("TestGP.Models.Car", "Car")
                        .WithMany("violations")
                        .HasForeignKey("CarID");

                    b.HasOne("TestGP.Models.Driver", "driver")
                        .WithMany()
                        .HasForeignKey("driverID");

                    b.Navigation("Car");

                    b.Navigation("driver");
                });

            modelBuilder.Entity("TestGP.Models.carMaintenance", b =>
                {
                    b.HasOne("TestGP.Models.Car", "Car")
                        .WithMany("carMaintenances")
                        .HasForeignKey("carId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("TestGP.Models.carObd2Violation", b =>
                {
                    b.HasOne("TestGP.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("carId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("TestGP.Models.Admin", b =>
                {
                    b.Navigation("AssCommunicateAdmin");

                    b.Navigation("Assistants");

                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("TestGP.Models.Assistant", b =>
                {
                    b.Navigation("AssCommunicateAdmin");

                    b.Navigation("DriverCommunicateAss");

                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("TestGP.Models.Car", b =>
                {
                    b.Navigation("carMaintenances");

                    b.Navigation("violations");
                });

            modelBuilder.Entity("TestGP.Models.Company", b =>
                {
                    b.Navigation("Assistants");

                    b.Navigation("Cars");

                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("TestGP.Models.Driver", b =>
                {
                    b.Navigation("DriverCommunicateAss");

                    b.Navigation("ManageViolation");
                });

            modelBuilder.Entity("TestGP.Models.Manager", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Assistants");

                    b.Navigation("Companies");

                    b.Navigation("Drivers");
                });

            modelBuilder.Entity("TestGP.Models.SuperAdminCompany", b =>
                {
                    b.Navigation("Managers");
                });
#pragma warning restore 612, 618
        }
    }
}
