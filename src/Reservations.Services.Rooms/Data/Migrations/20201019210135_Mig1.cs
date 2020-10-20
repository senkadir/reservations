using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservations.Services.Rooms.Data.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rooms");

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    office_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 250, nullable: false),
                    capacity = table.Column<int>(nullable: false),
                    chair_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rooms",
                schema: "rooms");
        }
    }
}
