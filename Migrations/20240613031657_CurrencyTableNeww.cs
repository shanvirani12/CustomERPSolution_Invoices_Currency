using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomERPSolution_Invoices_Currency.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyTableNeww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Currencies_CurrencyId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CurrencyId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CurrencyId",
                table: "Invoices",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Currencies_CurrencyId",
                table: "Invoices",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
