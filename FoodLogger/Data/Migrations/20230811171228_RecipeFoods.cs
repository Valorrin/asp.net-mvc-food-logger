using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecipeFoods : Migration
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
                name: "RecipesFoods",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesFoods", x => new { x.RecipeId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_RecipesFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipesFoods_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipesFoods_FoodId",
                table: "RecipesFoods",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipesFoods");

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
