using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Migrations
{
    public partial class subjectsTableBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_UserId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_UserId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SubjectUser",
                columns: table => new
                {
                    subjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectUser", x => new { x.subjectsId, x.usersId });
                    table.ForeignKey(
                        name: "FK_SubjectUser_AspNetUsers_usersId",
                        column: x => x.usersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectUser_Subjects_subjectsId",
                        column: x => x.subjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectUser_usersId",
                table: "SubjectUser",
                column: "usersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_UserId",
                table: "Subjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_UserId",
                table: "Subjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
