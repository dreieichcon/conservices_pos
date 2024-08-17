#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Innkeep.Db.Server.Migrations.Server
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "FiskalyConfigs");

            migrationBuilder.DropColumn(
                name: "TokenValidUntil",
                table: "FiskalyConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "ApiSecret",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApiKey",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApiSecret",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ApiKey",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenValidUntil",
                table: "FiskalyConfigs",
                type: "TEXT",
                nullable: true);
        }
    }
}
