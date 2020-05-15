using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitness_Tracker.Migrations
{
    public partial class workout_entities_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "CardioSessions",
                columns: table => new
                {
                    CardioSessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardioSessionDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardioSessions", x => x.CardioSessionId);
                    table.ForeignKey(
                        name: "FK_CardioSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResistanceTrainingSessions",
                columns: table => new
                {
                    ResistanceTrainingSessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSessionDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResistanceTrainingSessions", x => x.ResistanceTrainingSessionId);
                    table.ForeignKey(
                        name: "FK_ResistanceTrainingSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excercise",
                columns: table => new
                {
                    ExcerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcerciseName = table.Column<string>(nullable: true),
                    ResistanceTrainingSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excercise", x => x.ExcerciseId);
                    table.ForeignKey(
                        name: "FK_Excercise_ResistanceTrainingSessions_ResistanceTrainingSessionId",
                        column: x => x.ResistanceTrainingSessionId,
                        principalTable: "ResistanceTrainingSessions",
                        principalColumn: "ResistanceTrainingSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    SetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reps = table.Column<int>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    WeightUnit = table.Column<string>(nullable: false),
                    RateOfPercievedExertion = table.Column<int>(nullable: true),
                    ExcerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.SetId);
                    table.ForeignKey(
                        name: "FK_Set_Excercise_ExcerciseId",
                        column: x => x.ExcerciseId,
                        principalTable: "Excercise",
                        principalColumn: "ExcerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardioSessions_UserId",
                table: "CardioSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercise_ResistanceTrainingSessionId",
                table: "Excercise",
                column: "ResistanceTrainingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResistanceTrainingSessions_UserId",
                table: "ResistanceTrainingSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_ExcerciseId",
                table: "Set",
                column: "ExcerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardioSessions");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Excercise");

            migrationBuilder.DropTable(
                name: "ResistanceTrainingSessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
