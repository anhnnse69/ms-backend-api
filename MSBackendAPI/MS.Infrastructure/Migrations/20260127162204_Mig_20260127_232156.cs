using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig_20260127_232156 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NameVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTranslations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTranslations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NameVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BioVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BioEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicTitleVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorFacilities",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorFacilities", x => new { x.DoctorId, x.FacilityId });
                    table.ForeignKey(
                        name: "FK_DoctorFacilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorLanguages",
                columns: table => new
                {
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorLanguages", x => new { x.DoctorId, x.Language });
                    table.ForeignKey(
                        name: "FK_DoctorLanguages_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "Address", "City", "CreatedAt", "Email", "IsActive", "NameEn", "NameVi", "Phone", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "458 Minh Khai, Hai Bà Trưng, Hà Nội", "Hà Nội", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6900), "info.timescity@vinmec.com", true, "Vinmec Times City International Hospital", "Bệnh viện Đa khoa Quốc tế Vinmec Times City", "024 3974 3556", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "208 Nguyễn Hữu Cảnh, Bình Thạnh, TP. Hồ Chí Minh", "TP. Hồ Chí Minh", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6906), "info.centralpark@vinmec.com", true, "Vinmec Central Park International Hospital", "Bệnh viện Đa khoa Quốc tế Vinmec Central Park", "028 3622 1166", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "107-109 Nguyễn Văn Linh, Thanh Khê, Đà Nẵng", "Đà Nẵng", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6908), "info.danang@vinmec.com", true, "Vinmec Da Nang Hospital", "Bệnh viện Đa khoa Vinmec Đà Nẵng", "023 6371 1111", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Lô D20 Lê Hồng Phong, Ngô Quyền, Hải Phòng", "Hải Phòng", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6912), "info.haiphong@vinmec.com", true, "Vinmec Hai Phong Hospital", "Bệnh viện Đa khoa Vinmec Hải Phòng", "022 5730 9888", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Khu đô thị Vinpearl, Vĩnh Nguyên, Nha Trang", "Khánh Hòa", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6914), "info.nhatrang@vinmec.com", true, "Vinmec Nha Trang Hospital", "Bệnh viện Đa khoa Vinmec Nha Trang", "025 8390 0168", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Đảo Tuần Châu, Hạ Long, Quảng Ninh", "Quảng Ninh", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6916), "info.halong@vinmec.com", true, "Vinmec Ha Long Hospital", "Bệnh viện Đa khoa Vinmec Hạ Long", "020 3382 8188", 0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Bãi Dài, Gành Dầu, Phú Quốc, Kiên Giang", "Kiên Giang", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6919), "info.phuquoc@vinmec.com", true, "Vinmec Phu Quoc Hospital", "Bệnh viện Đa khoa Vinmec Phú Quốc", "029 7398 5588", 0 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Đường 30/4, Xuân Khánh, Ninh Kiều, Cần Thơ", "Cần Thơ", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(6921), "info.cantho@vinmec.com", true, "Vinmec Can Tho Hospital", "Bệnh viện Đa khoa Vinmec Cần Thơ", "029 2368 3003", 0 }
                });

            migrationBuilder.InsertData(
                table: "MessageTranslations",
                columns: new[] { "Id", "Code", "CreatedAt", "Language", "Text" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000001"), "APP_MESSAGE_2000", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7390), "vi", "Thành công" },
                    { new Guid("00000001-0000-0000-0000-000000000002"), "APP_MESSAGE_2000", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7395), "en", "Success" },
                    { new Guid("00000002-0000-0000-0000-000000000001"), "APP_MESSAGE_2001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7397), "vi", "Đặt lịch khám thành công" },
                    { new Guid("00000002-0000-0000-0000-000000000002"), "APP_MESSAGE_2001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7399), "en", "Appointment booked successfully" },
                    { new Guid("00000003-0000-0000-0000-000000000001"), "APP_MESSAGE_2002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7400), "vi", "Lịch hẹn đang chờ xác nhận" },
                    { new Guid("00000003-0000-0000-0000-000000000002"), "APP_MESSAGE_2002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7402), "en", "Appointment pending confirmation" },
                    { new Guid("00000004-0000-0000-0000-000000000001"), "APP_MESSAGE_2003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7404), "vi", "Lịch hẹn đã được xác nhận" },
                    { new Guid("00000004-0000-0000-0000-000000000002"), "APP_MESSAGE_2003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7441), "en", "Appointment confirmed" },
                    { new Guid("00000005-0000-0000-0000-000000000001"), "APP_MESSAGE_2004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7443), "vi", "Hủy lịch hẹn thành công" },
                    { new Guid("00000005-0000-0000-0000-000000000002"), "APP_MESSAGE_2004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7445), "en", "Appointment cancelled successfully" },
                    { new Guid("00000006-0000-0000-0000-000000000001"), "APP_MESSAGE_2005", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7447), "vi", "Tạo hồ sơ bệnh nhân thành công" },
                    { new Guid("00000006-0000-0000-0000-000000000002"), "APP_MESSAGE_2005", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7448), "en", "Patient record created successfully" },
                    { new Guid("00000007-0000-0000-0000-000000000001"), "APP_MESSAGE_2006", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7450), "vi", "Cập nhật thông tin bệnh nhân thành công" },
                    { new Guid("00000007-0000-0000-0000-000000000002"), "APP_MESSAGE_2006", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7451), "en", "Patient information updated successfully" },
                    { new Guid("00000008-0000-0000-0000-000000000001"), "APP_MESSAGE_4001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7453), "vi", "Số điện thoại không hợp lệ" },
                    { new Guid("00000008-0000-0000-0000-000000000002"), "APP_MESSAGE_4001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7454), "en", "Invalid phone number" },
                    { new Guid("00000009-0000-0000-0000-000000000001"), "APP_MESSAGE_4002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7456), "vi", "Ngày sinh không hợp lệ" },
                    { new Guid("00000009-0000-0000-0000-000000000002"), "APP_MESSAGE_4002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7458), "en", "Invalid date of birth" },
                    { new Guid("00000010-0000-0000-0000-000000000001"), "APP_MESSAGE_4003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7459), "vi", "Thiếu trường bắt buộc" },
                    { new Guid("00000010-0000-0000-0000-000000000002"), "APP_MESSAGE_4003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7461), "en", "Missing required field" },
                    { new Guid("00000011-0000-0000-0000-000000000001"), "APP_MESSAGE_4004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7462), "vi", "Thời gian khám không hợp lệ" },
                    { new Guid("00000011-0000-0000-0000-000000000002"), "APP_MESSAGE_4004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7464), "en", "Invalid appointment time" },
                    { new Guid("00000012-0000-0000-0000-000000000001"), "APP_MESSAGE_4005", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7465), "vi", "Thời gian khám đã qua" },
                    { new Guid("00000012-0000-0000-0000-000000000002"), "APP_MESSAGE_4005", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7467), "en", "Appointment time is in the past" },
                    { new Guid("00000013-0000-0000-0000-000000000001"), "APP_MESSAGE_4006", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7468), "vi", "Bác sĩ không có lịch khám" },
                    { new Guid("00000013-0000-0000-0000-000000000002"), "APP_MESSAGE_4006", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7469), "en", "Doctor not available" },
                    { new Guid("00000014-0000-0000-0000-000000000001"), "APP_MESSAGE_4007", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7471), "vi", "Khung giờ đã được đặt" },
                    { new Guid("00000014-0000-0000-0000-000000000002"), "APP_MESSAGE_4007", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7472), "en", "Time slot already booked" },
                    { new Guid("00000015-0000-0000-0000-000000000001"), "APP_MESSAGE_4008", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7473), "vi", "Không tìm thấy cơ sở y tế" },
                    { new Guid("00000015-0000-0000-0000-000000000002"), "APP_MESSAGE_4008", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7474), "en", "Facility not found" },
                    { new Guid("00000016-0000-0000-0000-000000000001"), "APP_MESSAGE_4009", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7476), "vi", "Không tìm thấy chuyên khoa" },
                    { new Guid("00000016-0000-0000-0000-000000000002"), "APP_MESSAGE_4009", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7477), "en", "Specialty not found" },
                    { new Guid("00000017-0000-0000-0000-000000000001"), "APP_MESSAGE_4010", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7478), "vi", "Không tìm thấy bệnh nhân" },
                    { new Guid("00000017-0000-0000-0000-000000000002"), "APP_MESSAGE_4010", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7481), "en", "Patient not found" },
                    { new Guid("00000018-0000-0000-0000-000000000001"), "APP_MESSAGE_4011", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7482), "vi", "Không tìm thấy bác sĩ" },
                    { new Guid("00000018-0000-0000-0000-000000000002"), "APP_MESSAGE_4011", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7483), "en", "Doctor not found" },
                    { new Guid("00000019-0000-0000-0000-000000000001"), "APP_MESSAGE_4012", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7485), "vi", "Không tìm thấy lịch hẹn" },
                    { new Guid("00000019-0000-0000-0000-000000000002"), "APP_MESSAGE_4012", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7486), "en", "Appointment not found" },
                    { new Guid("00000020-0000-0000-0000-000000000001"), "APP_MESSAGE_4013", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7487), "vi", "Trạng thái lịch hẹn không hợp lệ" },
                    { new Guid("00000020-0000-0000-0000-000000000002"), "APP_MESSAGE_4013", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7488), "en", "Invalid appointment status" },
                    { new Guid("00000021-0000-0000-0000-000000000001"), "APP_MESSAGE_4014", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7490), "vi", "Không có quyền truy cập" },
                    { new Guid("00000021-0000-0000-0000-000000000002"), "APP_MESSAGE_4014", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7491), "en", "Unauthorized access" },
                    { new Guid("00000022-0000-0000-0000-000000000001"), "APP_MESSAGE_4015", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7492), "vi", "Lịch hẹn trùng lặp" },
                    { new Guid("00000022-0000-0000-0000-000000000002"), "APP_MESSAGE_4015", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7494), "en", "Duplicate appointment" },
                    { new Guid("00000023-0000-0000-0000-000000000001"), "APP_MESSAGE_4016", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7495), "vi", "Thông tin đăng nhập không đúng" },
                    { new Guid("00000023-0000-0000-0000-000000000002"), "APP_MESSAGE_4016", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7496), "en", "Invalid credentials" },
                    { new Guid("00000024-0000-0000-0000-000000000001"), "APP_MESSAGE_4017", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7497), "vi", "Email đã tồn tại trong hệ thống" },
                    { new Guid("00000024-0000-0000-0000-000000000002"), "APP_MESSAGE_4017", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7499), "en", "Email already exists" },
                    { new Guid("00000025-0000-0000-0000-000000000001"), "APP_MESSAGE_4018", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7500), "vi", "Số điện thoại đã được sử dụng" },
                    { new Guid("00000025-0000-0000-0000-000000000002"), "APP_MESSAGE_4018", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7501), "en", "Phone number already in use" },
                    { new Guid("00000026-0000-0000-0000-000000000001"), "APP_MESSAGE_5001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7503), "vi", "Lỗi hệ thống" },
                    { new Guid("00000026-0000-0000-0000-000000000002"), "APP_MESSAGE_5001", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7504), "en", "Internal server error" },
                    { new Guid("00000027-0000-0000-0000-000000000001"), "APP_MESSAGE_5002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7530), "vi", "Lỗi cơ sở dữ liệu" },
                    { new Guid("00000027-0000-0000-0000-000000000002"), "APP_MESSAGE_5002", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7532), "en", "Database error" },
                    { new Guid("00000028-0000-0000-0000-000000000001"), "APP_MESSAGE_5003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7533), "vi", "Dịch vụ không khả dụng" },
                    { new Guid("00000028-0000-0000-0000-000000000002"), "APP_MESSAGE_5003", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7534), "en", "Service unavailable" },
                    { new Guid("00000029-0000-0000-0000-000000000001"), "APP_MESSAGE_5004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7536), "vi", "Lỗi dịch vụ bên ngoài" },
                    { new Guid("00000029-0000-0000-0000-000000000002"), "APP_MESSAGE_5004", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7537), "en", "External service error" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "CreatedAt", "DescriptionEn", "DescriptionVi", "DisplayOrder", "IconUrl", "IsActive", "NameEn", "NameVi" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7049), "Specializes in cardiovascular diseases", "Chuyên điều trị các bệnh lý tim mạch", 1, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Cardiology Center", "Trung tâm Tim mạch" },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7052), "Cancer treatment specialists", "Chuyên điều trị ung thư", 2, "https://vinmec-static.s3.amazonaws.com/icons/oncology.png", true, "Oncology Center", "Trung tâm Ung bướu" },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7054), "Children's healthcare", "Chăm sóc sức khỏe trẻ em", 3, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Pediatrics Center", "Trung tâm Nhi" },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7057), "Obstetrics and reproductive health", "Sản phụ khoa và sức khỏe sinh sản", 4, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Women's Health Center", "Trung tâm Sức khỏe phụ nữ" },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7059), "Digestive system and liver diseases", "Chuyên khoa tiêu hóa và gan mật", 5, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Gastroenterology", "Tiêu hóa - Gan mật" },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7061), "Neurological disorders diagnosis and treatment", "Chẩn đoán và điều trị bệnh lý thần kinh", 6, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Neurology", "Thần kinh" },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7063), "Trauma and orthopedic treatment", "Điều trị chấn thương và chỉnh hình", 7, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Orthopedics & Sports Medicine", "Chấn thương chỉnh hình - Y học thể thao" },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7065), "Comprehensive eye care", "Chăm sóc mắt toàn diện", 8, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Eye Center Vinmec-Alina", "Trung tâm Mắt Vinmec-Alina" },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7067), "Premium dental services", "Dịch vụ nha khoa cao cấp", 9, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Vinmec-View Premium Dental", "Nha khoa Vinmec-View Premium" },
                    { new Guid("a0000000-0000-0000-0000-000000000010"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7068), "Skin care and aesthetics", "Chăm sóc da và thẩm mỹ", 10, "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png", true, "Dermatology & Aesthetics", "Thẩm mỹ Da liễu" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FacilityId", "FullName", "IsActive", "LastLoginAt", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("f0000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7644), "admin@vinmec.com", null, "Quản trị viên hệ thống", true, null, "AQAAAAEAACcQAAAAEJ9z8ZqI5vYzJ5vZQ5vZQ5vZQ==", "0900000000", 1, "admin" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AcademicTitleEn", "AcademicTitleVi", "AverageRating", "BioEn", "BioVi", "CreatedAt", "Email", "FullName", "IsActive", "PhoneNumber", "PhotoUrl", "RatingCount", "SpecialtyId", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), "PhD., MD.", "Tiến sĩ, Bác sĩ", 4.7999999999999998, "Cardiologist with 20 years of experience", "Chuyên gia tim mạch với 20 năm kinh nghiệm", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7102), "bs.an@vinmec.com", "TS.BS Nguyễn Văn An", true, "0901234567", "https://vinmec-static.s3.amazonaws.com/dr-an.jpg", 150, new Guid("a0000000-0000-0000-0000-000000000001"), 20 },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), "Assoc. Prof., PhD.", "Phó Giáo sư, Tiến sĩ", 4.9000000000000004, "Leading oncology specialist", "Chuyên gia ung thư hàng đầu", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7105), "pgs.binh@vinmec.com", "PGS.TS Trần Thị Bình", true, "0902345678", "https://vinmec-static.s3.amazonaws.com/dr-binh.jpg", 200, new Guid("a0000000-0000-0000-0000-000000000002"), 25 },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), "Specialist Doctor Level 2", "Bác sĩ Chuyên khoa 2", 4.7000000000000002, "Experienced pediatrician", "Bác sĩ Nhi khoa giàu kinh nghiệm", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7111), "bs.cuong@vinmec.com", "BS.CK2 Lê Văn Cường", true, "0903456789", "https://vinmec-static.s3.amazonaws.com/dr-an.jpg", 120, new Guid("a0000000-0000-0000-0000-000000000003"), 15 },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), "PhD., MD.", "Tiến sĩ, Bác sĩ", 4.9000000000000004, "Obstetrics and gynecology specialist", "Chuyên gia sản phụ khoa", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7115), "bs.dung@vinmec.com", "TS.BS Phạm Thị Dung", true, "0904567890", "https://vinmec-static.s3.amazonaws.com/dr-an.jpg", 180, new Guid("a0000000-0000-0000-0000-000000000004"), 18 },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), "Specialist Doctor Level 1", "Bác sĩ Chuyên khoa 1", 4.5999999999999996, "Gastroenterology specialist", "Chuyên gia tiêu hóa - gan mật", new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7132), "bs.em@vinmec.com", "BS.CK1 Hoàng Văn Em", true, "0905678901", "https://vinmec-static.s3.amazonaws.com/dr-an.jpg", 95, new Guid("a0000000-0000-0000-0000-000000000005"), 12 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FacilityId", "FullName", "IsActive", "LastLoginAt", "PasswordHash", "PhoneNumber", "Role", "Username" },
                values: new object[] { new Guid("f0000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7647), "bs.an@vinmec.com", new Guid("11111111-1111-1111-1111-111111111111"), "TS.BS Nguyễn Văn An", true, null, "AQAAAAEAACcQAAAAEJ9z8ZqI5vYzJ5vZQ5vZQ5vZQ==", "0901234567", 2, "doctor.an" });

            migrationBuilder.InsertData(
                table: "DoctorAvailabilities",
                columns: new[] { "Id", "DayOfWeek", "DoctorId", "EndTime", "FacilityId", "IsActive", "SlotDurationMinutes", "StartTime" },
                values: new object[,]
                {
                    { new Guid("da000000-0000-0000-0000-000000000001"), 1, new Guid("d0000000-0000-0000-0000-000000000001"), new TimeSpan(0, 12, 0, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000002"), 3, new Guid("d0000000-0000-0000-0000-000000000001"), new TimeSpan(0, 12, 0, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000003"), 5, new Guid("d0000000-0000-0000-0000-000000000001"), new TimeSpan(0, 12, 0, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000004"), 2, new Guid("d0000000-0000-0000-0000-000000000002"), new TimeSpan(0, 17, 0, 0, 0), new Guid("22222222-2222-2222-2222-222222222222"), true, 20, new TimeSpan(0, 13, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000005"), 4, new Guid("d0000000-0000-0000-0000-000000000002"), new TimeSpan(0, 17, 0, 0, 0), new Guid("22222222-2222-2222-2222-222222222222"), true, 20, new TimeSpan(0, 13, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000006"), 1, new Guid("d0000000-0000-0000-0000-000000000003"), new TimeSpan(0, 11, 30, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000007"), 2, new Guid("d0000000-0000-0000-0000-000000000003"), new TimeSpan(0, 11, 30, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("da000000-0000-0000-0000-000000000008"), 3, new Guid("d0000000-0000-0000-0000-000000000003"), new TimeSpan(0, 11, 30, 0, 0), new Guid("11111111-1111-1111-1111-111111111111"), true, 15, new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "DoctorFacilities",
                columns: new[] { "DoctorId", "FacilityId", "AssignedDate", "IsPrimary" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7157), true },
                    { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7159), false },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7161), true },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7162), true },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7164), false },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7165), true },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7166), false },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 27, 16, 22, 3, 1, DateTimeKind.Utc).AddTicks(7168), true }
                });

            migrationBuilder.InsertData(
                table: "DoctorLanguages",
                columns: new[] { "DoctorId", "Language" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), 0 },
                    { new Guid("d0000000-0000-0000-0000-000000000001"), 1 },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), 0 },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), 1 },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), 0 },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), 0 },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), 1 },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentTime",
                table: "Appointments",
                column: "AppointmentTime");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_FacilityId_SpecialtyId",
                table: "Appointments",
                columns: new[] { "FacilityId", "SpecialtyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialtyId",
                table: "Appointments",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_DoctorId_DayOfWeek",
                table: "DoctorAvailabilities",
                columns: new[] { "DoctorId", "DayOfWeek" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilities_FacilityId",
                table: "DoctorAvailabilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorFacilities_FacilityId",
                table: "DoctorFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTranslations_Code_Language",
                table: "MessageTranslations",
                columns: new[] { "Code", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacilityId",
                table: "Users",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DoctorAvailabilities");

            migrationBuilder.DropTable(
                name: "DoctorFacilities");

            migrationBuilder.DropTable(
                name: "DoctorLanguages");

            migrationBuilder.DropTable(
                name: "MessageTranslations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
