using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreApiSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "FiskalySettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TseId",
                table: "FiskalySettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FiskalySettings");

            migrationBuilder.DropColumn(
                name: "TseId",
                table: "FiskalySettings");
        }
    }
}
