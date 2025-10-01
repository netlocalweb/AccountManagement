using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AccountManagement.Migrations.AppDb
{
    public partial class CreateClientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
        name: "Clients",
        columns: table => new
        {
            Id = table.Column<int>(nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            FirstName = table.Column<string>(maxLength: 100, nullable: false),
            LastName = table.Column<string>(maxLength: 100, nullable: false),
            Username = table.Column<string>(maxLength: 50, nullable: false),
            Email = table.Column<string>(maxLength: 100, nullable: false),
            PasswordHash = table.Column<string>(nullable: false),
            Phone = table.Column<string>(maxLength: 20, nullable: true),
            Birthdate = table.Column<DateTime>(nullable: true),
            DateCreated = table.Column<DateTime>(nullable: false),
            DateModified = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Clients", x => x.Id);
        });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Clients");

        }
    }
}
