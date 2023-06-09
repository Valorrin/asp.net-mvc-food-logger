using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFoodCategoryForeignKeyMistake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Foods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Foods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
