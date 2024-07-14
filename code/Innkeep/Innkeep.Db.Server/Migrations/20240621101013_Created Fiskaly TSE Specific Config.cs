using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreatedFiskalyTSESpecificConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TsePuk",
                table: "FiskalyConfigs");

            migrationBuilder.CreateTable(
                name: "TseConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TseId = table.Column<string>(type: "TEXT", nullable: false),
                    TsePuk = table.Column<string>(type: "TEXT", nullable: true),
                    TseAdminPassword = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TseConfigs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TseConfigs");

            migrationBuilder.AddColumn<string>(
                name: "TsePuk",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: true);
        }
    }
}
