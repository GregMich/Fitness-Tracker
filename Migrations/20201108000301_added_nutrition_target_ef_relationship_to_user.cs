using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Tracker.Migrations
{
    public partial class added_nutrition_target_ef_relationship_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NutritionTargets_UserId",
                table: "NutritionTargets");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionTargets_UserId",
                table: "NutritionTargets",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NutritionTargets_UserId",
                table: "NutritionTargets");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionTargets_UserId",
                table: "NutritionTargets",
                column: "UserId");
        }
    }
}
