using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_email",
                schema: "public",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "public",
                table: "user",
                newName: "username");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_salt",
                schema: "public",
                table: "user",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_hash",
                schema: "public",
                table: "user",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "public",
                table: "user",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "public",
                table: "user",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "auth_provider",
                schema: "public",
                table: "user",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "external_id",
                schema: "public",
                table: "user",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_email_auth_provider",
                schema: "public",
                table: "user",
                columns: new[] { "email", "auth_provider" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_email_auth_provider",
                schema: "public",
                table: "user");

            migrationBuilder.DropColumn(
                name: "auth_provider",
                schema: "public",
                table: "user");

            migrationBuilder.DropColumn(
                name: "external_id",
                schema: "public",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "public",
                table: "user",
                newName: "user_name");

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_salt",
                schema: "public",
                table: "user",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "password_hash",
                schema: "public",
                table: "user",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "public",
                table: "user",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "public",
                table: "user",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                schema: "public",
                table: "user",
                column: "email",
                unique: true);
        }
    }
}
