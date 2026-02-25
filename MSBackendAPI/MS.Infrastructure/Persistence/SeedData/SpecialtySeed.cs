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
                    IsActive = true,
                    CreateDate = DateTimeOffset.UtcNow,
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}
