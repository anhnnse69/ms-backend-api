using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateMedicalRecordentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 16, 48, 305, DateTimeKind.Unspecified).AddTicks(7374), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 16, 48, 305, DateTimeKind.Unspecified).AddTicks(7340), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 16, 48, 305, DateTimeKind.Unspecified).AddTicks(7427), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 16, 48, 305, DateTimeKind.Unspecified).AddTicks(7176), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 3, 10, 1, 16, 48, 554, DateTimeKind.Unspecified).AddTicks(3625), new TimeSpan(0, 0, 0, 0, 0)), "$2a$12$j6ds7AtBLhb7cj4gcRaLCel7S4J1EbY81bvJ77DccnoLGPEu.zORu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 5, 26, 125, DateTimeKind.Unspecified).AddTicks(8423), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 5, 26, 125, DateTimeKind.Unspecified).AddTicks(8386), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 5, 26, 125, DateTimeKind.Unspecified).AddTicks(8472), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreateDate",
                value: new DateTimeOffset(new DateTime(2026, 3, 10, 1, 5, 26, 125, DateTimeKind.Unspecified).AddTicks(8224), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreateDate", "PasswordHash" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 3, 10, 1, 5, 26, 352, DateTimeKind.Unspecified).AddTicks(4466), new TimeSpan(0, 0, 0, 0, 0)), "$2a$12$YeOf.oU5ud/So.bodldokOYl7uHcVPIW.viniJsbrO/ho4eWY22A2" });
        }
    }
}
