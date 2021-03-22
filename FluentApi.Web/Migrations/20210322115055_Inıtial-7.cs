using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentApi.Web.Migrations
{
    public partial class Inıtial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customers",
                column: "CreditCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "Customers");
        }
    }
}
