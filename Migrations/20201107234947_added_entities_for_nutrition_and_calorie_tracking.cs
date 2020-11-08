using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Tracker.Migrations
{
    public partial class added_entities_for_nutrition_and_calorie_tracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyNutritionLogs",
                columns: table => new
                {
                    DailyNutritionLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    NutritionLogDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyNutritionLogs", x => x.DailyNutritionLogId);
                    table.ForeignKey(
                        name: "FK_DailyNutritionLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionTargets",
                columns: table => new
                {
                    NutritionTargetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CalorieTarget = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionTargets", x => x.NutritionTargetId);
                    table.ForeignKey(
                        name: "FK_NutritionTargets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodEntry",
                columns: table => new
                {
                    FoodEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyNutritionLogId = table.Column<int>(nullable: false),
                    Calories = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEntry", x => x.FoodEntryId);
                    table.ForeignKey(
                        name: "FK_FoodEntry_DailyNutritionLogs_DailyNutritionLogId",
                        column: x => x.DailyNutritionLogId,
                        principalTable: "DailyNutritionLogs",
                        principalColumn: "DailyNutritionLogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyNutritionLogs_UserId",
                table: "DailyNutritionLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntry_DailyNutritionLogId",
                table: "FoodEntry",
                column: "DailyNutritionLogId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionTargets_UserId",
                table: "NutritionTargets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodEntry");

            migrationBuilder.DropTable(
                name: "NutritionTargets");

            migrationBuilder.DropTable(
                name: "DailyNutritionLogs");
        }
    }
}
