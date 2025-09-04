using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASP_Reservations.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "ADMIN")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsPopular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Category = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Allergen = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNum = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdFk = table.Column<int>(type: "int", nullable: false),
                    TableIdFk = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuestNum = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers",
                        column: x => x.UserIdFk,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables",
                        column: x => x.TableIdFk,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "IsPopular", "Price" },
                values: new object[] { 1, 3, 0, "sourdough bread, pickled cucumber trout roe(E,G,L)", "Skagen Sandwich", null, true, 175m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "Price" },
                values: new object[] { 2, 3, 0, "sourdough bread, pickled cucumber trout roe (E,G,L)", "Skagen Sandwich", null, 245m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "IsPopular", "Price" },
                values: new object[] { 3, 3, 0, "parmesan grilled sour dough (G,L)", "Chantarelle toast", null, true, 160m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "Price" },
                values: new object[,]
                {
                    { 4, 3, 0, "pickled red onions, new potatoes (L)", "Smoked Tiger shrimps", null, 175m },
                    { 5, 0, 0, "angel fries, parmesan, grilled heart salad", "Steak Tartare", null, 185m },
                    { 6, 2, 1, "tuna, egg, capers", "Nicoise", null, 275m },
                    { 7, 3, 2, "aioli (E,L)", "Moules frites", null, 285m }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "IsPopular", "Price" },
                values: new object[] { 8, 3, 2, "Sandefjord sauce, trout roe, Chantarelles, boiled potatoes (L)", "Baked Trout", null, true, 295m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "Price" },
                values: new object[] { 9, 3, 2, "veal, crushed potatoes, red wine sauce, creamy gremolata (E,G,L)", "Ossobuco", null, 295m });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "IsPopular", "Price" },
                values: new object[,]
                {
                    { 10, 3, 2, "green peppar sauce, French fries, parmesan (L)", "Pepper Steak", null, true, 395m },
                    { 11, 3, 3, "vanilla ice cream (E,L)", "Apple pie", null, true, 130m }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Allergen", "Category", "Description", "DishName", "ImageUrl", "Price" },
                values: new object[,]
                {
                    { 12, 3, 3, "(E,L)", "Creme brulee", null, 110m },
                    { 13, 7, 3, "(E,G,L,N)", "Chocolate truffle", null, 45m }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "TableNum" },
                values: new object[,]
                {
                    { 1, 2, 101 },
                    { 2, 2, 102 },
                    { 3, 4, 201 },
                    { 4, 4, 202 },
                    { 5, 4, 203 },
                    { 6, 4, 204 },
                    { 7, 8, 301 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "One@email.com", "Test One", "123-456-7890" },
                    { 2, "Two@email.com", "Test Two", "234-567-8901" },
                    { 3, "Three@email.com", "Test Three", "345-678-9012" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Username",
                table: "Admins",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Status_TableIdFk_StartDateTime",
                table: "Booking",
                columns: new[] { "Status", "TableIdFk", "StartDateTime" },
                filter: "[Status] != 'Cancelled'");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TableIdFk",
                table: "Booking",
                column: "TableIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserIdFk",
                table: "Booking",
                column: "UserIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_IsPopular",
                table: "Dishes",
                column: "IsPopular");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_Capacity",
                table: "Tables",
                column: "Capacity");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Phone",
                table: "Users",
                columns: new[] { "Email", "Phone" },
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
