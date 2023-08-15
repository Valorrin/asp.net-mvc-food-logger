using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodLogger.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToDiary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diary_AspNetUsers_AppUserId",
                table: "Diary");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntry_Diary_DiaryId",
                table: "DiaryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntry_Foods_FoodId",
                table: "DiaryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntry_Recipes_RecipeId",
                table: "DiaryEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryEntry",
                table: "DiaryEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diary",
                table: "Diary");

            migrationBuilder.RenameTable(
                name: "DiaryEntry",
                newName: "DiaryEntries");

            migrationBuilder.RenameTable(
                name: "Diary",
                newName: "Diaries");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntry_RecipeId",
                table: "DiaryEntries",
                newName: "IX_DiaryEntries_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntry_FoodId",
                table: "DiaryEntries",
                newName: "IX_DiaryEntries_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntry_DiaryId",
                table: "DiaryEntries",
                newName: "IX_DiaryEntries_DiaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Diary_AppUserId",
                table: "Diaries",
                newName: "IX_Diaries_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryEntries",
                table: "DiaryEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diaries",
                table: "Diaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_AspNetUsers_AppUserId",
                table: "Diaries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntries_Diaries_DiaryId",
                table: "DiaryEntries",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntries_Foods_FoodId",
                table: "DiaryEntries",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntries_Recipes_RecipeId",
                table: "DiaryEntries",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_AspNetUsers_AppUserId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntries_Diaries_DiaryId",
                table: "DiaryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntries_Foods_FoodId",
                table: "DiaryEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryEntries_Recipes_RecipeId",
                table: "DiaryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryEntries",
                table: "DiaryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diaries",
                table: "Diaries");

            migrationBuilder.RenameTable(
                name: "DiaryEntries",
                newName: "DiaryEntry");

            migrationBuilder.RenameTable(
                name: "Diaries",
                newName: "Diary");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntries_RecipeId",
                table: "DiaryEntry",
                newName: "IX_DiaryEntry_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntries_FoodId",
                table: "DiaryEntry",
                newName: "IX_DiaryEntry_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_DiaryEntries_DiaryId",
                table: "DiaryEntry",
                newName: "IX_DiaryEntry_DiaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Diaries_AppUserId",
                table: "Diary",
                newName: "IX_Diary_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryEntry",
                table: "DiaryEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diary",
                table: "Diary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diary_AspNetUsers_AppUserId",
                table: "Diary",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntry_Diary_DiaryId",
                table: "DiaryEntry",
                column: "DiaryId",
                principalTable: "Diary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntry_Foods_FoodId",
                table: "DiaryEntry",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryEntry_Recipes_RecipeId",
                table: "DiaryEntry",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
