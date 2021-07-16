using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUserContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "NotesFK_UserID",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "BackgroundColor",
                table: "Notes",
                newName: "Color");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserModelID",
                table: "Notes",
                column: "UserModelID",
                principalTable: "Users",
                principalColumn: "UserModelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserModelID",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Notes",
                newName: "BackgroundColor");

            migrationBuilder.AddForeignKey(
                name: "NotesFK_UserID",
                table: "Notes",
                column: "UserModelID",
                principalTable: "Users",
                principalColumn: "UserModelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
