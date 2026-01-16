using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class MoreNullDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_Order_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_User_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropIndex(
                name: "IX_Customers_User_ID",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Vendors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "FoodItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Stall_ID",
                table: "Vendors",
                column: "Stall_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_User_ID",
                table: "Vendors",
                column: "User_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_User_ID",
                table: "Customers",
                column: "User_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                principalTable: "Vendors",
                principalColumn: "Vendor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails",
                column: "Food_ID",
                principalTable: "FoodItems",
                principalColumn: "Food_ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_Order_ID",
                table: "OrderDetails",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders",
                column: "Customer_ID",
                principalTable: "Customers",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_FoodStalls_Stall_ID",
                table: "Vendors",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_Order_ID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FoodStalls_Stall_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_FoodStalls_Stall_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_Stall_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_User_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls");

            migrationBuilder.DropIndex(
                name: "IX_Customers_User_ID",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stall_ID",
                table: "FoodItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_User_ID",
                table: "Vendors",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                unique: true,
                filter: "[Vendor_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_User_ID",
                table: "Customers",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                principalTable: "Vendors",
                principalColumn: "Vendor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_FoodItems_Food_ID",
                table: "OrderDetails",
                column: "Food_ID",
                principalTable: "FoodItems",
                principalColumn: "Food_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_Order_ID",
                table: "OrderDetails",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID");

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
    }
}
