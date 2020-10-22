using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservations.Services.Reservations.Data.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                schema: "reservations",
                table: "reservations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "reservations",
                table: "reservations");
        }
    }
}
