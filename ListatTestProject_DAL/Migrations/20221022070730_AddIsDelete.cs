using Microsoft.EntityFrameworkCore.Migrations;

namespace ListatTestProject_DAL.Migrations
{
    public partial class AddIsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Items");
        }
    }
}
