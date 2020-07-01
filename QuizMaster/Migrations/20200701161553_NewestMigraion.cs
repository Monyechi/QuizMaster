using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMaster.Migrations
{
    public partial class NewestMigraion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c72e7f0-4c0a-4dfa-ba12-9a7edebcb9a0");

            migrationBuilder.AddColumn<string>(
                name: "StudentKey",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorKey = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "965fea85-11d8-42fc-901c-d84dda8d8523", "39192297-532a-4e9c-b672-f4e54379e9ac", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f237ac52-e0f8-4dc8-aad1-42048ea6fddb", "86cfeef6-a8e1-4b60-be1d-2af20d4fc3e3", "Instructor", "INSTRUCTOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_IdentityUserId",
                table: "Instructors",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "965fea85-11d8-42fc-901c-d84dda8d8523");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f237ac52-e0f8-4dc8-aad1-42048ea6fddb");

            migrationBuilder.DropColumn(
                name: "StudentKey",
                table: "Students");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c72e7f0-4c0a-4dfa-ba12-9a7edebcb9a0", "5bde96d5-f6e7-45d8-a5d1-fad21526a04e", "Student", "STUDENT" });
        }
    }
}
