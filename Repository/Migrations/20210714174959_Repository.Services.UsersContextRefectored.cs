using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUsersContextRefectored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ReminderOn",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Notes",
                newName: "Reminder");

            migrationBuilder.RenameColumn(
                name: "IsArchive",
                table: "Notes",
                newName: "IsArchived");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "Notes",
                newName: "NotesId");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "Reminder",
                table: "Notes",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "IsArchived",
                table: "Notes",
                newName: "IsArchive");

            migrationBuilder.RenameColumn(
                name: "NotesId",
                table: "Notes",
                newName: "NoteId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderOn",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
