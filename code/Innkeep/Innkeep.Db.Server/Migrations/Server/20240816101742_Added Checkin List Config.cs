using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Db.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedCheckinListConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedCheckinListId",
                table: "PretixConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SelectedCheckinListName",
                table: "PretixConfigs",
                type: "TEXT",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedCheckinListId",
                table: "PretixConfigs");

            migrationBuilder.DropColumn(
                name: "SelectedCheckinListName",
                table: "PretixConfigs");
        }
    }
}
