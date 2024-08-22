using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Db.Server.Migrations.Server
{
    /// <inheritdoc />
    public partial class AddLastknownHostname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastHostname",
                table: "Registers",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastHostname",
                table: "Registers");
        }
    }
}
