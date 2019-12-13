using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Data.Migrations
{
    public partial class InitTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Projects",
                columns: new[] { "Id", "CreatedDate", "Description", "LecturerId", "ProjectTypeId", "Status", "Title" },
                values: new object[] { 1, new DateTime(2019, 10, 21, 13, 30, 0, 0, DateTimeKind.Unspecified), "", "50717a31-498c-434a-972b-d79c0b9453a7", (short)2, (byte)0, "Hệ thống quản lý đồ án Hutech" });

            migrationBuilder.InsertData(
                table: "ProjectMembers",
                columns: new[] { "ProjectId", "StudentId", "Grade" },
                values: new object[] { 1, "dc291a7b-65b1-4a7f-a7c5-5e8dfef5e122", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumns: new[] { "ProjectId", "StudentId" },
                keyValues: new object[] { 1, "dc291a7b-65b1-4a7f-a7c5-5e8dfef5e122" });

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: (short)2);
        }
    }
}
