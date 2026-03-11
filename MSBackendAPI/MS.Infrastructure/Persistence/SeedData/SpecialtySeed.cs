using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class SpecialtySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialty>().HasData(
                new Specialty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    NameVi = "Nội tổng quát",
                    NameEn = "General Internal Medicine",
                    DescriptionVi = "Chuyên khoa nội tổng quát",
                    DescriptionEn = "General internal medicine specialty",
                    IconUrl = "/assets/images/specialties/internal_medicine.png",
                    DisplayOrder = 1,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Specialty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                    NameVi = "Nhi khoa",
                    NameEn = "Pediatrics",
                    DescriptionVi = "Chuyên khoa nhi",
                    DescriptionEn = "Pediatrics specialty",
                    IconUrl = "/assets/images/specialties/pediatrics.png",
                    DisplayOrder = 2,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Specialty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222224"),
                    NameVi = "Tim mạch",
                    NameEn = "Cardiology",
                    DescriptionVi = "Chuyên khoa tim mạch",
                    DescriptionEn = "Cardiology specialty",
                    IconUrl = "/assets/images/specialties/cardiology.png",
                    DisplayOrder = 3,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Specialty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222225"),
                    NameVi = "Thần kinh",
                    NameEn = "Neurology",
                    DescriptionVi = "Chuyên khoa thần kinh",
                    DescriptionEn = "Neurology specialty",
                    IconUrl = "/assets/images/specialties/neurology.png",
                    DisplayOrder = 4,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Specialty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222226"),
                    NameVi = "Nha khoa",
                    NameEn = "Dentistry",
                    DescriptionVi = "Chuyên khoa nha khoa",
                    DescriptionEn = "Dentistry specialty",
                    IconUrl = "/assets/images/specialties/dentistry.png",
                    DisplayOrder = 5,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}