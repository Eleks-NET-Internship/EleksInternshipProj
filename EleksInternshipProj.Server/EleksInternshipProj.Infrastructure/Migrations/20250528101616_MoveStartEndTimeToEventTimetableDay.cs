using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveStartEndTimeToEventTimetableDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_time",
                schema: "public",
                table: "event");

            migrationBuilder.DropColumn(
                name: "start_time",
                schema: "public",
                table: "event");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "end_time",
                schema: "public",
                table: "event_timetable_day",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "start_time",
                schema: "public",
                table: "event_timetable_day",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_time",
                schema: "public",
                table: "event_timetable_day");

            migrationBuilder.DropColumn(
                name: "start_time",
                schema: "public",
                table: "event_timetable_day");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "end_time",
                schema: "public",
                table: "event",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "start_time",
                schema: "public",
                table: "event",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
