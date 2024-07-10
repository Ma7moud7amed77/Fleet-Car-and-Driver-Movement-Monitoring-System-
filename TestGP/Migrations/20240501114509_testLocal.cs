using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class testLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdminCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyComp_id = table.Column<int>(type: "int", nullable: false),
                    MyComp_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyComp_Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyComp_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdminCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subcribtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfSubscribtion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuperAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_SuperAdminCompany_SuperAdminId",
                        column: x => x.SuperAdminId,
                        principalTable: "SuperAdminCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MgrID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuperAdminTreateManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperID = table.Column<int>(type: "int", nullable: true),
                    MgrID = table.Column<int>(type: "int", nullable: true),
                    MgrFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuperFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfMgrFeedback = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfSuperFeedback = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdminTreateManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperAdminTreateManagers_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuperAdminTreateManagers_SuperAdminCompany_SuperID",
                        column: x => x.SuperID,
                        principalTable: "SuperAdminCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    CompID = table.Column<int>(type: "int", nullable: true),
                    MgrID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Companies_CompID",
                        column: x => x.CompID,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Admins_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assistants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminID = table.Column<int>(type: "int", nullable: true),
                    CompID = table.Column<int>(type: "int", nullable: true),
                    MgrID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assistants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assistants_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assistants_Companies_CompID",
                        column: x => x.CompID,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assistants_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssCommunicateAdmins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssID = table.Column<int>(type: "int", nullable: true),
                    AdminID = table.Column<int>(type: "int", nullable: true),
                    DateOfMessageAdmin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfMessageAss = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssCommunicateAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssCommunicateAdmins_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssCommunicateAdmins_Assistants_AssID",
                        column: x => x.AssID,
                        principalTable: "Assistants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Health_statusd = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    MgrID = table.Column<int>(type: "int", nullable: true),
                    AdminID = table.Column<int>(type: "int", nullable: true),
                    AssID = table.Column<int>(type: "int", nullable: true),
                    CompID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Assistants_AssID",
                        column: x => x.AssID,
                        principalTable: "Assistants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Companies_CompID",
                        column: x => x.CompID,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompID = table.Column<int>(type: "int", nullable: true),
                    DriverID = table.Column<int>(type: "int", nullable: true),
                    AssId = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    MgrID = table.Column<int>(type: "int", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Assistants_AssId",
                        column: x => x.AssId,
                        principalTable: "Assistants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Companies_CompID",
                        column: x => x.CompID,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Managers_MgrID",
                        column: x => x.MgrID,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "currentLocations",
                columns: table => new
                {
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentSpeed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currentLocations", x => new { x.DriverId, x.date });
                    table.ForeignKey(
                        name: "FK_currentLocations_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverCommunicateAsses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssID = table.Column<int>(type: "int", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    DateOfMessageAss = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfMessageDriver = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverCommunicateAsses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverCommunicateAsses_Assistants_AssID",
                        column: x => x.AssID,
                        principalTable: "Assistants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverCommunicateAsses_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carMaintenances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carId = table.Column<int>(type: "int", nullable: false),
                    maintenanceDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carMaintenances", x => x.id);
                    table.ForeignKey(
                        name: "FK_carMaintenances_Cars_carId",
                        column: x => x.carId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carObd2Violations",
                columns: table => new
                {
                    carId = table.Column<int>(type: "int", nullable: false),
                    obd2Violation = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carObd2Violations", x => new { x.carId, x.obd2Violation });
                    table.ForeignKey(
                        name: "FK_carObd2Violations_Cars_carId",
                        column: x => x.carId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    driverID = table.Column<int>(type: "int", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: true),
                    CurrentSpeed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Violations_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Violations_Drivers_driverID",
                        column: x => x.driverID,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManageViolations",
                columns: table => new
                {
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    ViolationID = table.Column<int>(type: "int", nullable: false),
                    DateOfViolation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: true),
                    CurrentSpeed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManageViolations", x => new { x.DriverID, x.ViolationID });
                    table.ForeignKey(
                        name: "FK_ManageViolations_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManageViolations_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManageViolations_Violations_ViolationID",
                        column: x => x.ViolationID,
                        principalTable: "Violations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reportData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    ViolationId = table.Column<int>(type: "int", nullable: false),
                    AssistantId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ReportTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reportData_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reportData_Assistants_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Assistants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reportData_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reportData_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reportData_Violations_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "Violations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_CompID",
                table: "Admins",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_MgrID",
                table: "Admins",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_AssCommunicateAdmins_AdminID",
                table: "AssCommunicateAdmins",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_AssCommunicateAdmins_AssID",
                table: "AssCommunicateAdmins",
                column: "AssID");

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_AdminID",
                table: "Assistants",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_CompID",
                table: "Assistants",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_MgrID",
                table: "Assistants",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_carMaintenances_carId",
                table: "carMaintenances",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AdminId",
                table: "Cars",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AssId",
                table: "Cars",
                column: "AssId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CompID",
                table: "Cars",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverID",
                table: "Cars",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MgrID",
                table: "Cars",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_MgrID",
                table: "Companies",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCommunicateAsses_AssID",
                table: "DriverCommunicateAsses",
                column: "AssID");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCommunicateAsses_DriverID",
                table: "DriverCommunicateAsses",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AdminID",
                table: "Drivers",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AssID",
                table: "Drivers",
                column: "AssID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompID",
                table: "Drivers",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_MgrID",
                table: "Drivers",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_SuperAdminId",
                table: "Managers",
                column: "SuperAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ManageViolations_CarID",
                table: "ManageViolations",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_ManageViolations_ViolationID",
                table: "ManageViolations",
                column: "ViolationID");

            migrationBuilder.CreateIndex(
                name: "IX_reportData_AdminId",
                table: "reportData",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_reportData_AssistantId",
                table: "reportData",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_reportData_CarId",
                table: "reportData",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_reportData_DriverId",
                table: "reportData",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_reportData_ViolationId",
                table: "reportData",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdminTreateManagers_MgrID",
                table: "SuperAdminTreateManagers",
                column: "MgrID");

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdminTreateManagers_SuperID",
                table: "SuperAdminTreateManagers",
                column: "SuperID");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_CarID",
                table: "Violations",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_driverID",
                table: "Violations",
                column: "driverID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssCommunicateAdmins");

            migrationBuilder.DropTable(
                name: "carMaintenances");

            migrationBuilder.DropTable(
                name: "carObd2Violations");

            migrationBuilder.DropTable(
                name: "currentLocations");

            migrationBuilder.DropTable(
                name: "DriverCommunicateAsses");

            migrationBuilder.DropTable(
                name: "DriverTest");

            migrationBuilder.DropTable(
                name: "ManageViolations");

            migrationBuilder.DropTable(
                name: "reportData");

            migrationBuilder.DropTable(
                name: "SuperAdminTreateManagers");

            migrationBuilder.DropTable(
                name: "TestLocation");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Assistants");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "SuperAdminCompany");
        }
    }
}
