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
                    person_count = table.Column<int>(nullable: false),
                    duration = table.Column<NpgsqlRange<DateTime>>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reservations_duration",
                schema: "reservations",
                table: "reservations",
                column: "duration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations",
                schema: "reservations");
        }
    }
}
