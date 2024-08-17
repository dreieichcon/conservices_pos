#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Innkeep.Db.Server.Migrations.Transaction;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TransactionModels",
            columns: table => new
            {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                ReceiptType = table.Column<string>(type: "TEXT", nullable: false),
                TssId = table.Column<string>(type: "TEXT", nullable: false),
                ClientId = table.Column<string>(type: "TEXT", nullable: false),
                EventId = table.Column<string>(type: "TEXT", nullable: false),
                OrderSecret = table.Column<string>(type: "TEXT", nullable: false),
                AmountRequested = table.Column<decimal>(type: "TEXT", nullable: false),
                AmountGiven = table.Column<decimal>(type: "TEXT", nullable: false),
                AmountBack = table.Column<decimal>(type: "TEXT", nullable: false),
                ReceiptJson = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionModels", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TransactionModels");
    }
}