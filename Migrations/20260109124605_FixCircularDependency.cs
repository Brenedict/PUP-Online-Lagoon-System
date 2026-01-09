using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class FixCircularDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_FoodStalls_Stall_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_Stall_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.AlterColumn<string>(
                name: "Vendor_ID",
                table: "FoodStalls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                principalTable: "Vendors",
                principalColumn: "Vendor_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.AlterColumn<string>(
                name: "Vendor_ID",
                table: "FoodStalls",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Stall_ID",
                table: "Vendors",
                column: "Stall_ID");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_FoodStalls_Stall_ID",
                table: "Vendors",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
