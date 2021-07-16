using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUserContexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserModelID",
                table: "Notes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Reminder",
                table: "Notes",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Notes",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "NotesFK_UserID",
                table: "Notes",
                column: "UserModelID",
                principalTable: "Users",
                principalColumn: "UserModelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "NotesFK_UserID",
                table: "Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Reminder",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserModelID",
                table: "Notes",
                column: "UserModelID",
                principalTable: "Users",
                principalColumn: "UserModelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
