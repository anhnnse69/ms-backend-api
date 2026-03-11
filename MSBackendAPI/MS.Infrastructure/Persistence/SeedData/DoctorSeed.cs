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
                    UserId = Guid.Parse("55555555-5555-5555-5555-555555555557"), // doctor1 user
                    DisplayName = "Dr. John Doe",
                    FullName = "John Doe",
                    BioVi = "Bác sĩ chuyên khoa nội, 15 năm kinh nghiệm",
                    BioEn = "Internal medicine specialist with 15 years experience",
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
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    DisplayName = "Dr. Nguyễn Văn Anh",
                    FullName = "Nguyễn Văn Anh",
                    BioVi = "Bác sĩ chuyên khoa tim mạch, 12 năm kinh nghiệm",
                    BioEn = "Cardiologist with 12 years experience",
                    AcademicTitleVi = "Thạc sĩ",
                    AcademicTitleEn = "Master",
                    AvatarUrl = "/assets/images/doctors/van_anh.png",
                    PhotoUrl = "/assets/images/doctors/van_anh.png",
                    AverageRating = 4.9,
                    RatingCount = 250,
                    SpecialtyId = Guid.Parse("22222222-2222-2222-2222-222222222224"),
                    Email = "van.anh@clinic.vn",
                    PhoneNumber = "0987654321",
                    YearsOfExperience = 12,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    DisplayName = "Dr. Trần Hương",
                    FullName = "Trần Hương",
                    BioVi = "Bác sĩ nhi khoa, 8 năm kinh nghiệm",
                    BioEn = "Pediatrician with 8 years experience",
                    AcademicTitleVi = "Thạc sĩ",
                    AcademicTitleEn = "Master",
                    AvatarUrl = "/assets/images/doctors/tran_huong.png",
                    PhotoUrl = "/assets/images/doctors/tran_huong.png",
                    AverageRating = 4.7,
                    RatingCount = 180,
                    SpecialtyId = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    Email = "huong.tran@clinic.vn",
                    PhoneNumber = "0912345678",
                    YearsOfExperience = 8,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Doctor
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    DisplayName = "Dr. Lê Minh Tân",
                    FullName = "Lê Minh Tân",
                    BioVi = "Bác sĩ thần kinh, chuyên gia hàng đầu",
                    BioEn = "Leading neurologist specialist",
                    AcademicTitleVi = "Tiến sĩ",
                    AcademicTitleEn = "PhD",
                    AvatarUrl = "/assets/images/doctors/minh_tan.png",
                    PhotoUrl = "/assets/images/doctors/minh_tan.png",
                    AverageRating = 5.0,
                    RatingCount = 310,
                    SpecialtyId = Guid.Parse("22222222-2222-2222-2222-222222222225"),
                    Email = "minh.tan@hospital.vn",
                    PhoneNumber = "0898765432",
                    YearsOfExperience = 20,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}