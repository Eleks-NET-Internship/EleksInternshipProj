using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimetableDayAndUpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_timetable_day_day_day_id",
                schema: "public",
                table: "event_timetable_day");

            migrationBuilder.DropForeignKey(
                name: "FK_event_timetable_day_event_event_id",
                schema: "public",
                table: "event_timetable_day");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_timetable_day",
                schema: "public",
                table: "event_timetable_day");

            migrationBuilder.RenameTable(
                name: "event_timetable_day",
                schema: "public",
                newName: "event_day",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_event_timetable_day_event_id",
                schema: "public",
                table: "event_day",
                newName: "IX_event_day_event_id");

            migrationBuilder.RenameIndex(
                name: "IX_event_timetable_day_day_id",
                schema: "public",
                table: "event_day",
                newName: "IX_event_day_day_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_day",
                schema: "public",
                table: "event_day",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_day_day_day_id",
                schema: "public",
                table: "event_day",
                column: "day_id",
                principalSchema: "public",
                principalTable: "day",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_day_event_event_id",
                schema: "public",
                table: "event_day",
                column: "event_id",
                principalSchema: "public",
                principalTable: "event",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_day_day_day_id",
                schema: "public",
                table: "event_day");

            migrationBuilder.DropForeignKey(
                name: "FK_event_day_event_event_id",
                schema: "public",
                table: "event_day");

            migrationBuilder.DropPrimaryKey(
                name: "PK_event_day",
                schema: "public",
                table: "event_day");

            migrationBuilder.RenameTable(
                name: "event_day",
                schema: "public",
                newName: "event_timetable_day",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_event_day_event_id",
                schema: "public",
                table: "event_timetable_day",
                newName: "IX_event_timetable_day_event_id");

            migrationBuilder.RenameIndex(
                name: "IX_event_day_day_id",
                schema: "public",
                table: "event_timetable_day",
                newName: "IX_event_timetable_day_day_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_event_timetable_day",
                schema: "public",
                table: "event_timetable_day",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_timetable_day_day_day_id",
                schema: "public",
                table: "event_timetable_day",
                column: "day_id",
                principalSchema: "public",
                principalTable: "day",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_event_timetable_day_event_event_id",
                schema: "public",
                table: "event_timetable_day",
                column: "event_id",
                principalSchema: "public",
                principalTable: "event",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
