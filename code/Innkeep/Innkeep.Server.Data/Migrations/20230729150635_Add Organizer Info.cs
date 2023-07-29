using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizerInfo",
                table: "ApplicationSettings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizerInfo",
                table: "ApplicationSettings");
        }
    }
}
