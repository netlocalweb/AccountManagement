using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brirthdate",
                table: "Clients",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "IsActtive",
                table: "BankAccounts",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Clients",
                newName: "Brirthdate");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "BankAccounts",
                newName: "IsActtive");
        }
    }
}
