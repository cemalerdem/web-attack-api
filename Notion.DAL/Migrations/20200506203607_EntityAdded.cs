using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notion.DAL.Migrations
{
    public partial class EntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAtUTC = table.Column<DateTime>(nullable: false),
                    UpdatedAtUTC = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    MethodType = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    StatusCode = table.Column<string>(nullable: true),
                    QueryParameter = table.Column<string>(nullable: true),
                    RequestPayload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestModels", x => x.Id);
                    table.CheckConstraint("CK_RequestModels_MethodType_Enum_Constraint", "[MethodType] IN(1, 2, 3, 4)");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestModels");
        }
    }
}
