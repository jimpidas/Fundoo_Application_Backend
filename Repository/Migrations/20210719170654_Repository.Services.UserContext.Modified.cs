using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUserContextModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteLabel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteLabel",
                columns: table => new
                {
                    NoteLabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<int>(type: "int", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: false),
                    NotesId1 = table.Column<int>(type: "int", nullable: true),
                    UserModelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteLabel", x => x.NoteLabelId);
                    table.ForeignKey(
                        name: "FK_NoteLabel_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "LabelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteLabel_Notes_NotesId1",
                        column: x => x.NotesId1,
                        principalTable: "Notes",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NoteLabel_Users_UserModelID",
                        column: x => x.UserModelID,
                        principalTable: "Users",
                        principalColumn: "UserModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabel_LabelId",
                table: "NoteLabel",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabel_NotesId1",
                table: "NoteLabel",
                column: "NotesId1");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLabel_UserModelID",
                table: "NoteLabel",
                column: "UserModelID");
        }
    }
}
