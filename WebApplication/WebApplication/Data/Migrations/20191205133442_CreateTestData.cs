using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Data.Migrations
{
    public partial class CreateTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name" },
                values: new object[] { (short)1, "Công nghệ thông tin" });

            migrationBuilder.InsertData(
                table: "ProjectTypes",
                columns: new[] { "Id", "IsDisabled", "Name" },
                values: new object[,]
                {
                    { (short)1, false, "Đồ án cơ sở" },
                    { (short)2, false, "Đồ án chuyên ngành" },
                    { (short)3, false, "Đồ án thực tập" },
                    { (short)4, false, "Đồ án tổng hợp" }
                });

            migrationBuilder.InsertData(
                table: "SpecializedFaculties",
                columns: new[] { "Id", "FacultyId", "Name" },
                values: new object[] { (short)1, (short)1, "Công nghệ phần mềm" });

            migrationBuilder.InsertData(
                table: "SpecializedFaculties",
                columns: new[] { "Id", "FacultyId", "Name" },
                values: new object[] { (short)2, (short)1, "Hệ thống thông tin" });

            migrationBuilder.InsertData(
                table: "SpecializedFaculties",
                columns: new[] { "Id", "FacultyId", "Name" },
                values: new object[] { (short)3, (short)1, "An toàn thông tin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "SpecializedFaculties",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "SpecializedFaculties",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "SpecializedFaculties",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: (short)1);
        }
    }
}
