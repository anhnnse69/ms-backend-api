using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class PatientSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    UserId = Guid.Parse("55555555-5555-5555-5555-555555555558"), // patient1 user
                    DisplayName = "Nguyễn Văn A",
                    FullName = "Nguyễn Văn A",
                    DateOfBirth = new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    Gender = MS.Domain.Enums.Types.Gender.Male,
                    PhoneNumber = "0901234567",
                    Email = "nguyenvana@example.com",
                    Address = "456 Đường Nhỏ, Hà Nội",
                    IdentityCard = "123456789",
                    InsuranceNumber = "987654321",
                    AvatarUrl = "/assets/images/patients/nguyenvana.png",
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Patient
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    UserId = Guid.Parse("55555555-5555-5555-5555-555555555559"), // patient2 user
                    DisplayName = "Trần Thị B",
                    FullName = "Trần Thị B",
                    DateOfBirth = new DateTimeOffset(1985, 5, 15, 0, 0, 0, TimeSpan.Zero),
                    Gender = MS.Domain.Enums.Types.Gender.Female,
                    PhoneNumber = "0912345678",
                    Email = "tranthib@example.com",
                    Address = "789 Đường Trung Ương, TP. Hồ Chí Minh",
                    IdentityCard = "987654321",
                    InsuranceNumber = "123456789",
                    AvatarUrl = "/assets/images/patients/tranthib.png",
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Patient
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444446"),
                    // No UserId - patient without account (walk-in)
                    DisplayName = "Phạm Minh C",
                    FullName = "Phạm Minh C",
                    DateOfBirth = new DateTimeOffset(1992, 8, 20, 0, 0, 0, TimeSpan.Zero),
                    Gender = MS.Domain.Enums.Types.Gender.Male,
                    PhoneNumber = "0923456789",
                    Email = "phamminhc@example.com",
                    Address = "321 Lê Lợi, Đà Nẵng",
                    IdentityCard = "456789123",
                    InsuranceNumber = "654321987",
                    AvatarUrl = "/assets/images/patients/phamminhc.png",
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Patient
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444447"),
                    // No UserId - patient without account (walk-in)
                    DisplayName = "Hoàng Thúy D",
                    FullName = "Hoàng Thúy D",
                    DateOfBirth = new DateTimeOffset(1988, 3, 10, 0, 0, 0, TimeSpan.Zero),
                    Gender = MS.Domain.Enums.Types.Gender.Female,
                    PhoneNumber = "0934567890",
                    Email = "hoangthuy@example.com",
                    Address = "555 Trần Phú, Hải Phòng",
                    IdentityCard = "789123456",
                    InsuranceNumber = "321654987",
                    AvatarUrl = "/assets/images/patients/hoangthuy.png",
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}