using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUP_Online_Lagoon_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialSQLServerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Customer_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customer_ID);
                    table.ForeignKey(
                        name: "FK_Customers_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Vendor_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Stall_ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Vendor_ID);
                    table.ForeignKey(
                        name: "FK_Vendors_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodStalls",
                columns: table => new
                {
                    Stall_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StallName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StallDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Vendor_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodStalls", x => x.Stall_ID);
                    table.ForeignKey(
                        name: "FK_FoodStalls_Vendors_Vendor_ID",
                        column: x => x.Vendor_ID,
                        principalTable: "Vendors",
                        principalColumn: "Vendor_ID");
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Food_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    Stall_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Food_ID);
                    table.ForeignKey(
                        name: "FK_FoodItems_FoodStalls_Stall_ID",
                        column: x => x.Stall_ID,
                        principalTable: "FoodStalls",
                        principalColumn: "Stall_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTotal = table.Column<double>(type: "float", nullable: false),
                    EstPickupTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Stall_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_ID");
                    table.ForeignKey(
                        name: "FK_Orders_FoodStalls_Stall_ID",
                        column: x => x.Stall_ID,
                        principalTable: "FoodStalls",
                        principalColumn: "Stall_ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    OriginalPrice = table.Column<double>(type: "float", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Food_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_FoodItems_Food_ID",
                        column: x => x.Food_ID,
                        principalTable: "FoodItems",
                        principalColumn: "Food_ID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Order_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_User_ID",
                table: "Customers",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_Stall_ID",
                table: "FoodItems",
                column: "Stall_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodStalls_Vendor_ID",
                table: "FoodStalls",
                column: "Vendor_ID",
                unique: true,
                filter: "[Vendor_ID] IS NOT NULL");

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
                name: "IX_Vendors_User_ID",
                table: "Vendors",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "FoodStalls");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
