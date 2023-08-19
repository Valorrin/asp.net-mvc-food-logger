using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIngredientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Recipes_RecipeId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Grams = table.Column<double>(type: "float", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbs = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Foods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Recipes_RecipeId",
                table: "Foods",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
