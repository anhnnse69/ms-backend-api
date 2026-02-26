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
                    CreateDate = DateTimeOffset.UtcNow,
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}
