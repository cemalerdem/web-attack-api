using Microsoft.EntityFrameworkCore.Migrations;

namespace Notion.DAL.Migrations
{
    public partial class EntityModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RequestModels_MethodType_Enum_Constraint",
                table: "RequestModels");

            migrationBuilder.AlterColumn<string>(
                name: "MethodType",
                table: "RequestModels",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateCheckConstraint(
                name: "CK_RequestModels_MethodType_Enum_Constraint",
                table: "RequestModels",
                sql: "[MethodType] IN(1, 2, 3, 4)");

            migrationBuilder.AlterColumn<int>(
                name: "MethodType",
                table: "RequestModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
