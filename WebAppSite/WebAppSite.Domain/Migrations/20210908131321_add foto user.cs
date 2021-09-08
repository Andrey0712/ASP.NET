using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppSite.Domain.Migrations
{
    public partial class addfotouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageProfile",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "AspNetUsers");
        }
    }
}
