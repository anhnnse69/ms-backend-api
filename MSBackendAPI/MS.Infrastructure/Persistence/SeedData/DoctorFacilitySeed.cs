using MS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Persistence.SeedData
{
    public static class DoctorFacilitySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorFacility>().HasData(
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    AssignedDate = new DateTime(2024, 1, 1),
                    IsPrimary = true
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    AssignedDate = new DateTime(2024, 1, 1),
                    IsPrimary = true
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333335"),
                    AssignedDate = new DateTime(2024, 1, 1),
                    IsPrimary = true
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333336"),
                    AssignedDate = new DateTime(2024, 1, 1),
                    IsPrimary = true
                }
            );
        }
    }
}
