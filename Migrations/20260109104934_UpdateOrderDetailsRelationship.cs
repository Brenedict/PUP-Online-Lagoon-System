using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDetailsRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Food_ID = table.Column<string>(type: "TEXT", nullable: false),
                    FoodName = table.Column<string>(type: "TEXT", nullable: false),
                    FoodDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Availability = table.Column<bool>(type: "INTEGER", nullable: false),
                    Stall_ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Food_ID);
                });

            migrationBuilder.CreateTable(
                name: "FoodStalls",
                columns: table => new
                {
                    Stall_ID = table.Column<string>(type: "TEXT", nullable: false),
                    StallName = table.Column<string>(type: "TEXT", nullable: false),
                    StallDescription = table.Column<string>(type: "TEXT", nullable: false),
                    PrepTime = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    Vendor_ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodStalls", x => x.Stall_ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_ID = table.Column<string>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<string>(type: "TEXT", nullable: false),
                    OrderTime = table.Column<string>(type: "TEXT", nullable: false),
                    OrderStatus = table.Column<string>(type: "TEXT", nullable: false),
                    EstPickupTime = table.Column<string>(type: "TEXT", nullable: false),
                    Customer_ID = table.Column<string>(type: "TEXT", nullable: false),
                    Stall_ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_FoodStalls_Stall_ID",
                        column: x => x.Stall_ID,
                        principalTable: "FoodStalls",
                        principalColumn: "Stall_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Vendor_ID = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNum = table.Column<string>(type: "TEXT", nullable: false),
                    User_ID = table.Column<string>(type: "TEXT", nullable: false),
                    Stall_ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Vendor_ID);
                    table.ForeignKey(
                        name: "FK_Vendors_FoodStalls_Stall_ID",
                        column: x => x.Stall_ID,
                        principalTable: "FoodStalls",
                        principalColumn: "Stall_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<double>(type: "REAL", nullable: false),
                    Order_ID = table.Column<string>(type: "TEXT", nullable: false),
                    Food_ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_FoodItems_Food_ID",
                        column: x => x.Food_ID,
                        principalTable: "FoodItems",
                        principalColumn: "Food_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Order_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_Stall_ID",
                table: "FoodItems",
                column: "Stall_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Food_ID",
                table: "OrderDetails",
                column: "Food_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_ID",
                table: "OrderDetails",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_ID",
                table: "Orders",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Stall_ID",
                table: "Orders",
                column: "Stall_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Stall_ID",
                table: "Vendors",
                column: "Stall_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_User_ID",
                table: "Vendors",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_FoodStalls_Stall_ID",
                table: "FoodItems",
                column: "Stall_ID",
                principalTable: "FoodStalls",
                principalColumn: "Stall_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodStalls_Vendors_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                principalTable: "Vendors",
                principalColumn: "Vendor_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_FoodStalls_Stall_ID",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "FoodStalls");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
