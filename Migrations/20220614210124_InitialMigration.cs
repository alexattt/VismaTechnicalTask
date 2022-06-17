using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VismaTechnicalTask.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAdr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeleAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedSpeciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCPersonTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAdr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeleAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRecs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MsgType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MIGversion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRecs_Senders_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Err_DN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Err_OT = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }
    }
}
