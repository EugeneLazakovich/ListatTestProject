using Microsoft.EntityFrameworkCore.Migrations;

namespace ListatTestProject_DAL.Migrations
{
    public partial class SetForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sales_ItemId",
                table: "Sales",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ItemId",
                table: "Sales");
        }
    }
}
