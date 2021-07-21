using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUserContextLabelNoteLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserModelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserModelID",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Reminder",
                table: "Notes",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserModelID = table.Column<int>(type: "int", nullable: false),
                    LabelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelID);
                    table.ForeignKey(
                        name: "LabelsFK_UserID",
                        column: x => x.UserModelID,
                        principalTable: "Users",
                        principalColumn: "UserModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteLabels",
                columns: table => new
                {
                    NoteLabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotesId = table.Column<int>(type: "int", nullable: false),
                    LabelId = table.Column<int>(type: "int", nullable: false),
                    UserModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteLabels", x => x.NoteLabelId);
                    table.ForeignKey(
                        name: "NoteLabelFK_LabelID",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "LabelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "NoteLabelFK_NoteID",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "NoteLabelFK_UserID",
                        column: x => x.UserModelID,
                        principalTable: "Users",
                        principalColumn: "UserModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserModelID",
                table: "Labels",
                column: "UserModelID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabels_LabelId",
                table: "NoteLabels",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabels_NotesId",
                table: "NoteLabels",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabels_UserModelID",
                table: "NoteLabels",
                column: "UserModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteLabels");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Reminder",
                table: "Notes",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserModelID", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "niha@gmail.com", "Niha", "Jain", "Pass@123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserModelID", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 2, "janvi@gmail.com", "Janvi", "Kirsten", "Pass@123" });
        }
    }
}
