using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesNotesContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "NotesFK_UserID",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notes",
                newName: "UserModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                newName: "IX_Notes_UserModelID");

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

            migrationBuilder.RenameColumn(
                name: "UserModelID",
                table: "Notes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_UserModelID",
                table: "Notes",
                newName: "IX_Notes_UserId");

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.UserId);
                });

            migrationBuilder.AddForeignKey(
                name: "NotesFK_UserID",
                table: "Notes",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
