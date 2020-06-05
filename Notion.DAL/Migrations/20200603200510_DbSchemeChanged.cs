using Microsoft.EntityFrameworkCore.Migrations;

namespace Notion.DAL.Migrations
{
    public partial class DbSchemeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestModels",
                table: "RequestModels");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RequestModels");

            migrationBuilder.RenameTable(
                name: "RequestModels",
                newName: "RequestStreams");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "RequestStreams",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestStreams",
                table: "RequestStreams",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestStreams",
                table: "RequestStreams");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "RequestStreams");

            migrationBuilder.RenameTable(
                name: "RequestStreams",
                newName: "RequestModels");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RequestModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestModels",
                table: "RequestModels",
                column: "Id");
        }
    }
}
