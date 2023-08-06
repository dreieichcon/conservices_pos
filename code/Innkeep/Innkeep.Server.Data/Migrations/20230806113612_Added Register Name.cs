using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRegisterName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisterName",
                table: "Registers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterName",
                table: "Registers");
        }
    }
}
