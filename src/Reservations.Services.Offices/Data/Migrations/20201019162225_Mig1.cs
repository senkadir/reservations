using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservations.Services.Offices.Data.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "offices");

            migrationBuilder.CreateTable(
                name: "offices",
                schema: "offices",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    location = table.Column<string>(maxLength: 250, nullable: false),
                    open_time = table.Column<TimeSpan>(nullable: false),
                    close_time = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_offices", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offices",
                schema: "offices");
        }
    }
}
