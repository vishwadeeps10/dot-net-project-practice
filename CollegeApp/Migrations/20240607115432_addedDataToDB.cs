using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class addedDataToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Added_On", "Address", "Category", "Date_of_birth", "Email", "Entollment_no", "Fathers_name", "Gender", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 7, 17, 24, 31, 719, DateTimeKind.Local).AddTicks(9808), "Patna Bihar", 3, new DateTime(1998, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "vishwadeep@gmail.com", "123456731", "Shankar Singh", 0, "Vishwadeep Singh" },
                    { 2, new DateTime(2024, 6, 7, 17, 24, 31, 719, DateTimeKind.Local).AddTicks(9822), "Chamtha Bihar", 3, new DateTime(1999, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "rohit@gmail.com", "123456731E", "Nagendra Singh", 0, "Rohit Singh" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
