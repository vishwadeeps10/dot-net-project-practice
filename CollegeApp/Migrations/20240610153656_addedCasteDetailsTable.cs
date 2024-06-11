using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class addedCasteDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCasteCertificateDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CasteCertiNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CasteCertiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CasteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecievedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecievedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCasteCertificateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCasteCertificateDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCasteCertificateDetails_StudentId",
                table: "StudentCasteCertificateDetails",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCasteCertificateDetails");
        }
    }
}
