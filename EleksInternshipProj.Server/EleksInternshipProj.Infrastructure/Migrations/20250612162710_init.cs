using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EleksInternshipProj.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "day",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    day_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "space",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_space", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "taskstatus",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskstatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: true),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: true),
                    auth_provider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    external_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_solo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_space_space_id",
                        column: x => x.space_id,
                        principalSchema: "public",
                        principalTable: "space",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "marker",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marker", x => x.id);
                    table.ForeignKey(
                        name: "FK_marker_space_space_id",
                        column: x => x.space_id,
                        principalSchema: "public",
                        principalTable: "space",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timetable",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timetable", x => x.id);
                    table.ForeignKey(
                        name: "FK_timetable_space_space_id",
                        column: x => x.space_id,
                        principalSchema: "public",
                        principalTable: "space",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_space",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    space_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_space", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_space_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_space_space_space_id",
                        column: x => x.space_id,
                        principalSchema: "public",
                        principalTable: "space",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_space_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.id);
                    table.ForeignKey(
                        name: "FK_note_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "public",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "solo_event",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    event_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solo_event", x => x.id);
                    table.ForeignKey(
                        name: "FK_solo_event_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "public",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    status_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    event_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deadline = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_task_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "public",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_taskstatus_status_id",
                        column: x => x.status_id,
                        principalSchema: "public",
                        principalTable: "taskstatus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_marker",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    marker_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_marker", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_marker_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "public",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_marker_marker_marker_id",
                        column: x => x.marker_id,
                        principalSchema: "public",
                        principalTable: "marker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timetable_day",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timetable_id = table.Column<long>(type: "bigint", nullable: false),
                    day_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timetable_day", x => x.id);
                    table.ForeignKey(
                        name: "FK_timetable_day_day_day_id",
                        column: x => x.day_id,
                        principalSchema: "public",
                        principalTable: "day",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_timetable_day_timetable_timetable_id",
                        column: x => x.timetable_id,
                        principalSchema: "public",
                        principalTable: "timetable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_timetable_day",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    timetable_day_id = table.Column<long>(type: "bigint", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_timetable_day", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_timetable_day_event_event_id",
                        column: x => x.event_id,
                        principalSchema: "public",
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_timetable_day_timetable_day_timetable_day_id",
                        column: x => x.timetable_day_id,
                        principalSchema: "public",
                        principalTable: "timetable_day",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_event_space_id",
                schema: "public",
                table: "event",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_marker_event_id",
                schema: "public",
                table: "event_marker",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_marker_marker_id",
                schema: "public",
                table: "event_marker",
                column: "marker_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_timetable_day_event_id",
                schema: "public",
                table: "event_timetable_day",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_timetable_day_timetable_day_id",
                schema: "public",
                table: "event_timetable_day",
                column: "timetable_day_id");

            migrationBuilder.CreateIndex(
                name: "IX_marker_space_id",
                schema: "public",
                table: "marker",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "IX_note_event_id",
                schema: "public",
                table: "note",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_name",
                schema: "public",
                table: "role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_solo_event_event_id",
                schema: "public",
                table: "solo_event",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_event_id",
                schema: "public",
                table: "task",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_status_id",
                schema: "public",
                table: "task",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_timetable_space_id",
                schema: "public",
                table: "timetable",
                column: "space_id");

            migrationBuilder.CreateIndex(
                name: "IX_timetable_day_day_id",
                schema: "public",
                table: "timetable_day",
                column: "day_id");

            migrationBuilder.CreateIndex(
                name: "IX_timetable_day_timetable_id",
                schema: "public",
                table: "timetable_day",
                column: "timetable_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email_auth_provider",
                schema: "public",
                table: "user",
                columns: new[] { "email", "auth_provider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_space_role_id",
                schema: "public",
                table: "user_space",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_space_space_id_user_id",
                schema: "public",
                table: "user_space",
                columns: new[] { "space_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_space_user_id",
                schema: "public",
                table: "user_space",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_marker",
                schema: "public");

            migrationBuilder.DropTable(
                name: "event_timetable_day",
                schema: "public");

            migrationBuilder.DropTable(
                name: "note",
                schema: "public");

            migrationBuilder.DropTable(
                name: "solo_event",
                schema: "public");

            migrationBuilder.DropTable(
                name: "task",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_space",
                schema: "public");

            migrationBuilder.DropTable(
                name: "marker",
                schema: "public");

            migrationBuilder.DropTable(
                name: "timetable_day",
                schema: "public");

            migrationBuilder.DropTable(
                name: "event",
                schema: "public");

            migrationBuilder.DropTable(
                name: "taskstatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "day",
                schema: "public");

            migrationBuilder.DropTable(
                name: "timetable",
                schema: "public");

            migrationBuilder.DropTable(
                name: "space",
                schema: "public");
        }
    }
}
