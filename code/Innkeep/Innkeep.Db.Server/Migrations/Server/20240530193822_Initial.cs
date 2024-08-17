#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Innkeep.Db.Server.Migrations.Server
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PretixConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PretixAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedOrganizerSlug = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedOrganizerName = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedEventSlug = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedEventName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PretixConfigs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PretixConfigs");
        }
    }
}
