using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomERPSolution_Invoices_Currency.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Currencies",
                newName: "CurrencyName");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "CurrencyName",
                table: "Currencies",
                newName: "Code");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Currencies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
