using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VismaTechnicalTask.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HcPersons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HcPersons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelperInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastAddedXmlDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HCPersonID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAdr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeleAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receivers_Depts_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receivers_HcPersons_HCPersonID",
                        column: x => x.HCPersonID,
                        principalTable: "HcPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HCPersonID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAdr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeleAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Senders_Depts_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Senders_HcPersons_HCPersonID",
                        column: x => x.HCPersonID,
                        principalTable: "HcPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppRecs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MsgType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MIGversion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRecs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRecs_Receivers_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Receivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppRecs_Senders_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErrorReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppRecID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Err_S = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Err_V = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Err_DN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorReasons_AppRecs_AppRecID",
                        column: x => x.AppRecID,
                        principalTable: "AppRecs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRecs_ReceiverID",
                table: "AppRecs",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_AppRecs_SenderID",
                table: "AppRecs",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorReasons_AppRecID",
                table: "ErrorReasons",
                column: "AppRecID");

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_DeptID",
                table: "Receivers",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Receivers_HCPersonID",
                table: "Receivers",
                column: "HCPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Senders_DeptID",
                table: "Senders",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Senders_HCPersonID",
                table: "Senders",
                column: "HCPersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorReasons");

            migrationBuilder.DropTable(
                name: "HelperInfo");

            migrationBuilder.DropTable(
                name: "AppRecs");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.DropTable(
                name: "Senders");

            migrationBuilder.DropTable(
                name: "Depts");

            migrationBuilder.DropTable(
                name: "HcPersons");
        }
    }
}
