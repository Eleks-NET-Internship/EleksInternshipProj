using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeToMarker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "public",
                table: "marker",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                schema: "public",
                table: "marker");
        }
    }
}
