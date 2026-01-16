using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class DeletedVendorIdOnStall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropColumn(
                name: "Vendor_ID",
                table: "FoodStalls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Vendor_ID",
                table: "FoodStalls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                principalTable: "Vendors",
                principalColumn: "Vendor_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
