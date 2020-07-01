using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMaster.Migrations
{
    public partial class newMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "965fea85-11d8-42fc-901c-d84dda8d8523");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f237ac52-e0f8-4dc8-aad1-42048ea6fddb");

            migrationBuilder.AddColumn<string>(
                name: "InstructorName",
                table: "Students",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e28cbe81-aa97-4002-bcc8-565209c4f166", "d3c94723-4a80-47c8-843d-aa923b18318c", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab2921a8-24b5-48bb-b351-6a13689f5328", "cac0b128-6acf-4f50-a16a-5a92b79a97b9", "Instructor", "INSTRUCTOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab2921a8-24b5-48bb-b351-6a13689f5328");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e28cbe81-aa97-4002-bcc8-565209c4f166");

            migrationBuilder.DropColumn(
                name: "InstructorName",
                table: "Students");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "965fea85-11d8-42fc-901c-d84dda8d8523", "39192297-532a-4e9c-b672-f4e54379e9ac", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f237ac52-e0f8-4dc8-aad1-42048ea6fddb", "86cfeef6-a8e1-4b60-be1d-2af20d4fc3e3", "Instructor", "INSTRUCTOR" });
        }
    }
}
