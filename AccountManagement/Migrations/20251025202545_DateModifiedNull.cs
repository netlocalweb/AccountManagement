using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Migrations
{
    /// <inheritdoc />
    public partial class DateModifiedNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModefied",
                table: "BankAccounts",
                newName: "DateModified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "BankAccounts",
                newName: "DateModefied");
        }
    }
}
