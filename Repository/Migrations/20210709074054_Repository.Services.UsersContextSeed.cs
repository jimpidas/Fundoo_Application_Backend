using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RepositoryServicesUsersContextSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserModelID", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "niha@gmail.com", "Niha", "Jain", "Pass@123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserModelID", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 2, "janvi@gmail.com", "Janvi", "Kirsten", "Pass@123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserModelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserModelID",
                keyValue: 2);
        }
    }
}
