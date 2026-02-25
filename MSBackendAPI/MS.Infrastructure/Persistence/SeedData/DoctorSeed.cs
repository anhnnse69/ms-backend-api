using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class DoctorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DisplayName = "Dr. John Doe",
                    FullName = "John Doe",
                    BioVi = "Bác sĩ chuyên khoa nội",
                    BioEn = "Internal medicine specialist",
                    AcademicTitleVi = "Tiến sĩ",
                    AcademicTitleEn = "PhD",
                    AvatarUrl = "/assets/images/doctors/john_doe.png",
                    PhotoUrl = "/assets/images/doctors/john_doe.png",
                    AverageRating = 4.8,
                    RatingCount = 120,
                    SpecialtyId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Email = "john.doe@example.com",
                    PhoneNumber = "0123456789",
                    YearsOfExperience = 15,
                    IsActive = true,
                    CreateDate = DateTimeOffset.UtcNow,
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}
