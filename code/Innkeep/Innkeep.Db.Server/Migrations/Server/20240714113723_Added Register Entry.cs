#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Innkeep.Db.Server.Migrations.Server
{
    /// <inheritdoc />
    public partial class AddedRegisterEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RegisterIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    RegisterDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registers");
        }
    }
}
