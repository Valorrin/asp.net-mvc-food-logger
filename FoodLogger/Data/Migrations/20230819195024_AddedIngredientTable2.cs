using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIngredientTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipeId",
                table: "ingredients",
                newName: "IX_ingredients_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_Recipes_RecipeId",
                table: "ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_Recipes_RecipeId",
                table: "ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_ingredients_RecipeId",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipes_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
