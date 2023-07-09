using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettings_Event_SelectedEventId",
                table: "ApplicationSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettings_Organizer_SelectedOrganizerId",
                table: "ApplicationSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Event_PretixEventId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Register_DeviceId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Register",
                table: "Register");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizer",
                table: "Organizer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Register",
                newName: "Registers");

            migrationBuilder.RenameTable(
                name: "Organizer",
                newName: "Organizers");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameColumn(
                name: "PretixEventId",
                table: "Transactions",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PretixEventId",
                table: "Transactions",
                newName: "IX_Transactions_OrganizerId");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registers",
                table: "Registers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizers",
                table: "Organizers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EventId",
                table: "Transactions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettings_Events_SelectedEventId",
                table: "ApplicationSettings",
                column: "SelectedEventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettings_Organizers_SelectedOrganizerId",
                table: "ApplicationSettings",
                column: "SelectedOrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Organizers_OrganizerId",
                table: "Transactions",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Registers_DeviceId",
                table: "Transactions",
                column: "DeviceId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettings_Events_SelectedEventId",
                table: "ApplicationSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettings_Organizers_SelectedOrganizerId",
                table: "ApplicationSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Events_EventId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Organizers_OrganizerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Registers_DeviceId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_EventId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registers",
                table: "Registers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizers",
                table: "Organizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Registers",
                newName: "Register");

            migrationBuilder.RenameTable(
                name: "Organizers",
                newName: "Organizer");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Transactions",
                newName: "PretixEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OrganizerId",
                table: "Transactions",
                newName: "IX_Transactions_PretixEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Register",
                table: "Register",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizer",
                table: "Organizer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettings_Event_SelectedEventId",
                table: "ApplicationSettings",
                column: "SelectedEventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettings_Organizer_SelectedOrganizerId",
                table: "ApplicationSettings",
                column: "SelectedOrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Event_PretixEventId",
                table: "Transactions",
                column: "PretixEventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Register_DeviceId",
                table: "Transactions",
                column: "DeviceId",
                principalTable: "Register",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
