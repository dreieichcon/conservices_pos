using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Db.Server.Migrations.Transaction
{
    /// <inheritdoc />
    public partial class AddedPretixOrderCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
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
                name: "OrderCode",
                table: "TransactionModels");
        }
    }
}
