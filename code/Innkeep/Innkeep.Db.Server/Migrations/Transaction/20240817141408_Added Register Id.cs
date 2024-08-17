using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Db.Server.Migrations.Transaction
{
    /// <inheritdoc />
    public partial class AddedRegisterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisterId",
                table: "TransactionModels",
                type: "TEXT",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "TransactionModels");
        }
    }
}
