using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesAddedNoteLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteLabel",
                columns: table => new
                {
                    NoteLabelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotesID = table.Column<int>(type: "int", nullable: false),
                    LabelID = table.Column<int>(type: "int", nullable: false),
                    UserModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteLabel", x => x.NoteLabelID);
                    table.ForeignKey(
                        name: "NoteLabelFK_LabelID",
                        column: x => x.LabelID,
                        principalTable: "Labels",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "NoteLabelFK_NotesID",
                        column: x => x.NotesID,
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
                name: "IX_NoteLabel_LabelID",
                table: "NoteLabel",
                column: "LabelID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabel_NotesID",
                table: "NoteLabel",
                column: "NotesID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabel_UserModelID",
                table: "NoteLabel",
                column: "UserModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteLabel");
        }
    }
}
