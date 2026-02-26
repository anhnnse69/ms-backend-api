using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class FacilitySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().HasData(
                new Facility
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    NameVi = "Bệnh viện Trung ương",
                    NameEn = "Central Hospital",
                    DescriptionVi = "Bệnh viện đa khoa lớn nhất khu vực",
                    DescriptionEn = "The largest general hospital in the region",
                    LogoUrl = "/assets/images/facilities/central_hospital.png",
                    Address = "123 Đường Lớn, Hà Nội",
                    Phone = "0987654321",
                    Email = "contact@centralhospital.vn",
                    City = "Hà Nội",
                    Type = MS.Domain.Enums.Types.FacilityType.Hospital,
                    IsActive = true,
                    CreateDate = DateTimeOffset.UtcNow,
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}
