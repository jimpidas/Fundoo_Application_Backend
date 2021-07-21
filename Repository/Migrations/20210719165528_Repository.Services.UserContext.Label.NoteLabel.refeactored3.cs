using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUserContextLabelNoteLabelrefeactored3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NotesId",
                table: "NoteLabels");

            migrationBuilder.DropIndex(
                name: "IX_NoteLabels_NotesId",
                table: "NoteLabels");

            migrationBuilder.AddColumn<int>(
                name: "NotesId1",
                table: "NoteLabels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabels_NotesId1",
                table: "NoteLabels",
                column: "NotesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Notes_NotesId1",
                table: "NoteLabels",
                column: "NotesId1",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NotesId1",
                table: "NoteLabels");

            migrationBuilder.DropIndex(
                name: "IX_NoteLabels_NotesId1",
                table: "NoteLabels");

            migrationBuilder.DropColumn(
                name: "NotesId1",
                table: "NoteLabels");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabels_NotesId",
                table: "NoteLabels",
                column: "NotesId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Notes_NotesId",
                table: "NoteLabels",
                column: "NotesId",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
