using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbSRV.Migrations
{
    public partial class ModClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankFee",
                table: "Taxs");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Tariffs",
                newName: "BankFee");

            migrationBuilder.CreateIndex(
                name: "IX_Taxs_TariffId",
                table: "Taxs",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxs_Tariffs_TariffId",
                table: "Taxs",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "TariffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxs_Tariffs_TariffId",
                table: "Taxs");

            migrationBuilder.DropIndex(
                name: "IX_Taxs_TariffId",
                table: "Taxs");

            migrationBuilder.RenameColumn(
                name: "BankFee",
                table: "Tariffs",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "BankFee",
                table: "Taxs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
