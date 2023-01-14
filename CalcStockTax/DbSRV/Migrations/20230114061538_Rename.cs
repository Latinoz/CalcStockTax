using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbSRV.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankFee",
                table: "Tariffs",
                newName: "BrokerFee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrokerFee",
                table: "Tariffs",
                newName: "BankFee");
        }
    }
}
