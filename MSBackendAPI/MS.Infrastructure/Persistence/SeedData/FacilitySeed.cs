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
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Facility
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    NameVi = "Bệnh viện Chợ Rẫy",
                    NameEn = "Cho Ray Hospital",
                    DescriptionVi = "Bệnh viện đa khoa hàng đầu thành phố",
                    DescriptionEn = "Leading general hospital in the city",
                    LogoUrl = "/assets/images/facilities/choray_hospital.png",
                    Address = "201 Nguyễn Văn Cừ, TP. Hồ Chí Minh",
                    Phone = "0898765432",
                    Email = "info@chorayhospital.vn",
                    City = "TP. Hồ Chí Minh",
                    Type = MS.Domain.Enums.Types.FacilityType.Hospital,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Facility
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333335"),
                    NameVi = "Phòng khám Saigon Clinic",
                    NameEn = "Saigon Clinic",
                    DescriptionVi = "Phòng khám chuyên khoa hàng đầu",
                    DescriptionEn = "Leading specialized clinic",
                    LogoUrl = "/assets/images/facilities/saigon_clinic.png",
                    Address = "100 Nguyễn Hữu Cảnh, Quận 1",
                    Phone = "0776543210",
                    Email = "contact@saigonclinic.vn",
                    City = "TP. Hồ Chí Minh",
                    Type = MS.Domain.Enums.Types.FacilityType.Clinic,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                },
                new Facility
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333336"),
                    NameVi = "Nha khoa Rạng Ngời",
                    NameEn = "Bright Dental",
                    DescriptionVi = "Nha khoa hiện đại với công nghệ tiên tiến",
                    DescriptionEn = "Modern dental clinic with advanced technology",
                    LogoUrl = "/assets/images/facilities/bright_dental.png",
                    Address = "85 Trần Hưng Đạo, Đà Nẵng",
                    Phone = "0654321098",
                    Email = "smile@brightdental.vn",
                    City = "Đà Nẵng",
                    Type = MS.Domain.Enums.Types.FacilityType.Clinic,
                    IsDeleted = false,
                    CreateDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}