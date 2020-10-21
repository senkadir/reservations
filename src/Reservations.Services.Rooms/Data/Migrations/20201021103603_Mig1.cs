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
                name: "resources",
                schema: "rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    specific = table.Column<bool>(nullable: false),
                    total_quantity = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 250, nullable: true),
                    portable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resources", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "room_resources",
                schema: "rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    room_id = table.Column<Guid>(nullable: false),
                    resource_id = table.Column<Guid>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_resources", x => x.id);
                    table.ForeignKey(
                        name: "fk_room_resources_resources_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "rooms",
                        principalTable: "resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_room_resources_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "rooms",
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_room_resources_resource_id",
                schema: "rooms",
                table: "room_resources",
                column: "resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_room_resources_room_id",
                schema: "rooms",
                table: "room_resources",
                column: "room_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "room_resources",
                schema: "rooms");

            migrationBuilder.DropTable(
                name: "resources",
                schema: "rooms");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "rooms");
        }
    }
}
