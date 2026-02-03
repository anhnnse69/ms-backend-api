using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20260203_112245 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2182));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2184));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2188));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2192));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2194));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 22, 54, 82, DateTimeKind.Utc).AddTicks(2196));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8024));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8033));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8036));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8039));

            migrationBuilder.UpdateData(
                table: "DoctorFacilities",
                keyColumns: new[] { "DoctorId", "FacilityId" },
                keyValues: new object[] { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111") },
                column: "AssignedDate",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8042));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(6996));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7001));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7010));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7015));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7023));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7027));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8821));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8884));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8888));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8893));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8901));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8906));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8916));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8932));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8937));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8944));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8949));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8953));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8957));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8963));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8967));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000010-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8976));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8989));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000013-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(8998));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9003));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000014-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9008));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9012));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000015-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9016));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9020));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000016-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000017-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000018-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9044));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9048));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9057));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000020-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9065));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000021-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9070));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9074));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000022-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000023-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000024-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9147));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000025-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9161));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000026-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000027-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9174));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000028-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9183));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9187));

            migrationBuilder.UpdateData(
                table: "MessageTranslations",
                keyColumn: "Id",
                keyValue: new Guid("00000029-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(9192));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7615));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7625));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7648));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7657));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 2, 3, 4, 21, 35, 842, DateTimeKind.Utc).AddTicks(7673));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 4, 21, 36, 218, DateTimeKind.Utc).AddTicks(6253), "$2a$12$/IqJWmWiH1J9uj3srevMxuuZYdlXiXGVHgWRsl0bVfhDOKWJimZoW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 3, 4, 21, 36, 603, DateTimeKind.Utc).AddTicks(6319), "$2a$12$/JoaY6f/u1KHAeGPcvQOeeS2vxXdyAao0HZJW6mot2Hp.GjZRa2pW" });
        }
    }
}
