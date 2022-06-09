using Microsoft.EntityFrameworkCore.Migrations;

namespace VismaTechnicalTask.Migrations
{
    public partial class ErrorModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Err_OT",
                table: "ErrorReasons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Err_OT",
                table: "ErrorReasons");
        }
    }
}
