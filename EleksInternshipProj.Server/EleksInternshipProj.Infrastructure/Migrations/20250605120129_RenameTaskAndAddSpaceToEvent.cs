using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaskAndAddSpaceToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "space_id",
                schema: "public",
                table: "event",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_event_space_id",
                schema: "public",
                table: "event",
                column: "space_id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_space_space_id",
                schema: "public",
                table: "event",
                column: "space_id",
                principalSchema: "public",
                principalTable: "space",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_space_space_id",
                schema: "public",
                table: "event");

            migrationBuilder.DropIndex(
                name: "IX_event_space_id",
                schema: "public",
                table: "event");

            migrationBuilder.DropColumn(
                name: "space_id",
                schema: "public",
                table: "event");
        }
    }
}
