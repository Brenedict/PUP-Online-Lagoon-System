using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class FixedFKNullDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Vendors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Customer_ID",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<double>(
                name: "OrderTotal",
                table: "Orders",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Food_ID",
                table: "OrderDetails",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "FoodName",
                table: "OrderDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "OriginalPrice",
                table: "OrderDetails",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails",
                column: "Food_ID",
                principalTable: "FoodItems",
                principalColumn: "Food_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders",
                column: "Customer_ID",
                principalTable: "Customers",
                principalColumn: "Customer_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FoodName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Vendors",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Customer_ID",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderTime",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Food_ID",
                table: "OrderDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "OrderDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails",
                column: "Food_ID",
                principalTable: "FoodItems",
                principalColumn: "Food_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders",
                column: "Customer_ID",
                principalTable: "Customers",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
