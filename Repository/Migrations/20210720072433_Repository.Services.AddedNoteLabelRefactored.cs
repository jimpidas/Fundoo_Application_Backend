using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesAddedNoteLabelRefactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LabelID",
                table: "NoteLabel",
                newName: "LabelId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteLabel_LabelID",
                table: "NoteLabel",
                newName: "IX_NoteLabel_LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LabelId",
                table: "NoteLabel",
                newName: "LabelID");

            migrationBuilder.RenameIndex(
                name: "IX_NoteLabel_LabelId",
                table: "NoteLabel",
                newName: "IX_NoteLabel_LabelID");
        }
    }
}
