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
                    IsActive = true,
                    CreateDate = DateTimeOffset.UtcNow,
                    CreateBy = "system",
                    LastModifiedBy = "system"
                }
            );
        }
    }
}
