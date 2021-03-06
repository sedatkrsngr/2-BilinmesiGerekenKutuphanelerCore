using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentApi.Web.Migrations
{
    public partial class Inıtial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)//Update-Database dediğimizde burası çalışır. 
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)//Burası  ise ilgili migrationu silmek için yani remove-migration
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
