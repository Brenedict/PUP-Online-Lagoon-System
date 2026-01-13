using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderAndStallAttr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PrepTime",
                table: "FoodStalls",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "PrepTime",
                table: "FoodStalls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
