#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Innkeep.Db.Server.Migrations.Server
{
    /// <inheritdoc />
    public partial class AddFiskalySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiskalyConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: true),
                    ApiSecret = table.Column<string>(type: "TEXT", nullable: true),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    TsePuk = table.Column<string>(type: "TEXT", nullable: true),
                    TokenValidUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TseId = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiskalyConfigs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiskalyConfigs");
        }
    }
}
