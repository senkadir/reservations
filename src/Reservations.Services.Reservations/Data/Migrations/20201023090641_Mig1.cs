using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

namespace Reservations.Services.Reservations.Data.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reservations");

            migrationBuilder.CreateTable(
                name: "reservations",
                schema: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    room_id = table.Column<Guid>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    person_count = table.Column<int>(nullable: false),
                    duration = table.Column<NpgsqlRange<DateTime>>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "resources",
                schema: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    reservation_id = table.Column<Guid>(nullable: false),
                    resource_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resources", x => x.id);
                    table.ForeignKey(
                        name: "fk_resources_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalSchema: "reservations",
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reservations_duration",
                schema: "reservations",
                table: "reservations",
                column: "duration");

            migrationBuilder.CreateIndex(
                name: "ix_resources_reservation_id",
                schema: "reservations",
                table: "resources",
                column: "reservation_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resources",
                schema: "reservations");

            migrationBuilder.DropTable(
                name: "reservations",
                schema: "reservations");
        }
    }
}
