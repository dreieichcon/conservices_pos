using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEventandOrganizer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Organizer_OrganizerId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Event_EventId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_EventId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Event_OrganizerId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "EventSlug",
                table: "Event",
                newName: "Slug");

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SelectedOrganizerId = table.Column<int>(type: "INTEGER", nullable: true),
                    SelectedEventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSettings_Event_SelectedEventId",
                        column: x => x.SelectedEventId,
                        principalTable: "Event",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationSettings_Organizer_SelectedOrganizerId",
                        column: x => x.SelectedOrganizerId,
                        principalTable: "Organizer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettings_SelectedEventId",
                table: "ApplicationSettings",
                column: "SelectedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettings_SelectedOrganizerId",
                table: "ApplicationSettings",
                column: "SelectedOrganizerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSettings");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Event",
                newName: "EventSlug");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Register",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Event",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Register_EventId",
                table: "Register",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizerId",
                table: "Event",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Organizer_OrganizerId",
                table: "Event",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Event_EventId",
                table: "Register",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
