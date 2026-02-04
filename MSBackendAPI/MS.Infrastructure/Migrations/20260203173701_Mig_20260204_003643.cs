using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20260204_003643 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3694));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3698));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3701));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3635));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3640));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3645));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3649));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3359));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3365));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3367));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3370));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3376));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3379));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4013));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4015));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4017));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4029));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4031));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4033));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4036));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4037));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4039));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4041));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4052));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4054));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4058));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4059));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4063));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4064));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4066));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4068));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4069));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4071));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4073));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4075));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4076));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4078));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4081));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4082));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4084));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4086));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4088));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4093));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4096));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4101));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4103));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4105));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4106));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4108));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_4019", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4110), "Dữ liệu không hợp lệ" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_4019", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4111), "General validation error" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5000", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4119), "Lỗi hệ thống" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5000", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4121), "Internal server error" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5001", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4123), "Lỗi cơ sở dữ liệu" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5001", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4125), "Database error" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5002", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4126), "Dịch vụ không khả dụng" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5002", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4128), "Service unavailable" });

            migrationBuilder.InsertData(
                table: "MessageTranslations",
                columns: new[] { "Id", "Code", "CreatedAt", "Language", "Text" },
                values: new object[,]
                {
                    { new Guid("00000030-0000-0000-0000-000000000001"), "APP_MESSAGE_5003", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4130), "vi", "Lỗi dịch vụ bên ngoài" },
                    { new Guid("00000030-0000-0000-0000-000000000002"), "APP_MESSAGE_5003", new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(4131), "en", "External service error" }
                });

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3570));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3572));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3575));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3579));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3581));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 17, 36, 59, 231, DateTimeKind.Utc).AddTicks(3585));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 17, 36, 59, 484, DateTimeKind.Utc).AddTicks(5019), "$2a$12$b15qaPl8n6KkuDKBahkZvehWDV.HHuqYIcyd4eBSr.J7tqcg4kN7K" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 17, 36, 59, 748, DateTimeKind.Utc).AddTicks(5927), "$2a$12$IirXHAk3XeWEBBYfWkOCKuEQKBPUzAsxC4N5bnEyYQcExra1dcKw." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000030-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000030-0000-0000-0000-000000000002"));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1607));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1609));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1612));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1614));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1616));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1618));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1622));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1545));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1556));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1561));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1153));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1164));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1173));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1176));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1178));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2049));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2052));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2054));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2057));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2062));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2065));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2070));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2072));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2074));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2076));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2079));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2081));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2083));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2085));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2087));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2090));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2092));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2094));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2096));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2099));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2101));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2103));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2105));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2107));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2109));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2111));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2115));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2119));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2121));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2127));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2129));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2131));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2166));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2168));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2171));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2173));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2178));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5001", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2182), "Lỗi hệ thống" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5001", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2184), "Internal server error" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5002", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2186), "Lỗi cơ sở dữ liệu" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5002", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2188), "Database error" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5003", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2190), "Dịch vụ không khả dụng" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5003", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2192), "Service unavailable" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000001"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5004", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2194), "Lỗi dịch vụ bên ngoài" });

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000002"),
                columns: new[] { "Code", "CreatedAt", "Text" },
                values: new object[] { "APP_MESSAGE_5004", new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2196), "External service error" });

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1432));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1436));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1442));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1444));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(1487));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 4, 22, 54, 416, DateTimeKind.Utc).AddTicks(9258), "$2a$12$Xk.9fMG1OH1mfA9.HGjj7uME7m.J.3BygTqKHIwGy8q878G/zLWSy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 4, 22, 54, 757, DateTimeKind.Utc).AddTicks(8930), "$2a$12$iRMV.uUp5VD2J/d941NLreww5jMAvefDKE2ZF/wQtkuENl6JOTmwW" });
        }
    }
}
