using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MS.Domain.Enums.Roles;
using MS.Domain.Shared.Utility;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class UserSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Username = "admin",
                    PasswordHash = PasswordHelper.HashPassword("Admin@123"),
                    DisplayName = "Admin User",
                    FullName = "Administrator",
                    AvatarUrl = "/assets/images/users/admin.png",
                    Email = "admin@example.com",
                    PhoneNumber = "0999999999",
                    Role = SystemRole.ITAdmin,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555556"),
                    Username = "manager1",
                    PasswordHash = PasswordHelper.HashPassword("Manager@123"),
                    DisplayName = "Manager One",
                    FullName = "Quản Lý Một",
                    AvatarUrl = "/assets/images/users/manager1.png",
                    Email = "manager1@hospital.vn",
                    PhoneNumber = "0987654311",
                    Role = SystemRole.Manager,
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555557"),
                    Username = "doctor1",
                    PasswordHash = PasswordHelper.HashPassword("Doctor@123"),
                    DisplayName = "Dr. John Doe",
                    FullName = "John Doe",
                    AvatarUrl = "/assets/images/users/doctor1.png",
                    Email = "john.doe@example.com",
                    PhoneNumber = "0123456789",
                    Role = SystemRole.Doctor,
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555558"),
                    Username = "patient1",
                    PasswordHash = PasswordHelper.HashPassword("Patient@123"),
                    DisplayName = "Nguyễn Văn A",
                    FullName = "Nguyễn Văn A",
                    AvatarUrl = "/assets/images/users/patient1.png",
                    Email = "nguyenvana@example.com",
                    PhoneNumber = "0901234567",
                    Role = SystemRole.Patient,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555559"),
                    Username = "patient2",
                    PasswordHash = PasswordHelper.HashPassword("Patient@123"),
                    DisplayName = "Trần Thị B",
                    FullName = "Trần Thị B",
                    AvatarUrl = "/assets/images/users/patient2.png",
                    Email = "tranthib@example.com",
                    PhoneNumber = "0912345678",
                    Role = SystemRole.Patient,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}