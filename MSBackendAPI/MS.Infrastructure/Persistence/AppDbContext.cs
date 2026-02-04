using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Domain.Enums.Roles;
using MS.Domain.Enums.Types;
using MS.Domain.Shared.Utility;
namespace MS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<DoctorFacility> DoctorFacilities { get; set; }
        public DbSet<DoctorLanguage> DoctorLanguages { get; set; }
        public DbSet<MessageTranslation> MessageTranslations { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure UUID as primary key with default value generation
            ConfigureGuidKeys(modelBuilder);
            // Configure composite keys
            ConfigureCompositeKeys(modelBuilder);
            // Configure relationships
            ConfigureRelationships(modelBuilder);
            // Configure indexes
            ConfigureIndexes(modelBuilder);
            // Seed initial data
            SeedData(modelBuilder);
        }
        /// <summary>
        /// Configure GUID primary keys with database default value (NEWID())
        /// This ensures UUID uniqueness at database level
        /// </summary>
        private void ConfigureGuidKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Doctor>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<DoctorAvailability>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Facility>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<MessageTranslation>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Patient>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Specialty>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
        }
        /// <summary>
        /// Configure composite keys for many-to-many relationships
        /// </summary>
        private void ConfigureCompositeKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorFacility>()
                .HasKey(df => new { df.DoctorId, df.FacilityId });
            modelBuilder.Entity<DoctorLanguage>()
                .HasKey(dl => new { dl.DoctorId, dl.Language });
        }
        /// <summary>
        /// Configure entity relationships and foreign keys
        /// </summary>
        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Appointment relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Facility)
                .WithMany(f => f.Appointments)
                .HasForeignKey(a => a.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Specialty)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            // Doctor relationships
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            // DoctorAvailability relationships
            modelBuilder.Entity<DoctorAvailability>()
                .HasOne(da => da.Doctor)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(da => da.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DoctorAvailability>()
                .HasOne(da => da.Facility)
                .WithMany()
                .HasForeignKey(da => da.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);
            // DoctorFacility relationships
            modelBuilder.Entity<DoctorFacility>()
                .HasOne(df => df.Doctor)
                .WithMany(d => d.Facilities)
                .HasForeignKey(df => df.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DoctorFacility>()
                .HasOne(df => df.Facility)
                .WithMany(f => f.DoctorFacilities)
                .HasForeignKey(df => df.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            // DoctorLanguage relationships
            modelBuilder.Entity<DoctorLanguage>()
                .HasOne(dl => dl.Doctor)
                .WithMany(d => d.Languages)
                .HasForeignKey(dl => dl.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            // User relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Facility)
                .WithMany()
                .HasForeignKey(u => u.FacilityId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
        /// <summary>
        /// Configure database indexes for performance optimization
        /// </summary>
        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // Appointment indexes
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.AppointmentTime);
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.DoctorId, a.AppointmentTime });
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => new { a.FacilityId, a.SpecialtyId });
            // Doctor indexes
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.SpecialtyId);
            // DoctorAvailability indexes
            modelBuilder.Entity<DoctorAvailability>()
                .HasIndex(da => new { da.DoctorId, da.DayOfWeek });
            // User unique indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            // Patient index
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.PhoneNumber);
            // MessageTranslation unique index
            modelBuilder.Entity<MessageTranslation>()
                .HasIndex(mt => new { mt.Code, mt.Language })
                .IsUnique();
        }
        /// <summary>
        /// Seed initial data into database
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            SeedFacilities(modelBuilder);
            SeedSpecialties(modelBuilder);
            SeedDoctors(modelBuilder);
            SeedDoctorFacilities(modelBuilder);
            SeedDoctorLanguages(modelBuilder);
            SeedDoctorAvailabilities(modelBuilder);
            SeedMessageTranslations(modelBuilder);
            SeedUsers(modelBuilder);
        }
        /// <summary>
        /// Seed Vinmec hospital facilities across Vietnam
        /// </summary>
        private void SeedFacilities(ModelBuilder modelBuilder)
        {
            var facilities = new[]
            {
                new Facility
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    NameVi = "Bệnh viện Đa khoa Quốc tế Vinmec Times City",
                    NameEn = "Vinmec Times City International Hospital",
                    Address = "458 Minh Khai, Hai Bà Trưng, Hà Nội",
                    Phone = "024 3974 3556",
                    Email = "info.timescity@vinmec.com",
                    City = "Hà Nội",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    NameVi = "Bệnh viện Đa khoa Quốc tế Vinmec Central Park",
                    NameEn = "Vinmec Central Park International Hospital",
                    Address = "208 Nguyễn Hữu Cảnh, Bình Thạnh, TP. Hồ Chí Minh",
                    Phone = "028 3622 1166",
                    Email = "info.centralpark@vinmec.com",
                    City = "TP. Hồ Chí Minh",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Đà Nẵng",
                    NameEn = "Vinmec Da Nang Hospital",
                    Address = "107-109 Nguyễn Văn Linh, Thanh Khê, Đà Nẵng",
                    Phone = "023 6371 1111",
                    Email = "info.danang@vinmec.com",
                    City = "Đà Nẵng",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Hải Phòng",
                    NameEn = "Vinmec Hai Phong Hospital",
                    Address = "Lô D20 Lê Hồng Phong, Ngô Quyền, Hải Phòng",
                    Phone = "022 5730 9888",
                    Email = "info.haiphong@vinmec.com",
                    City = "Hải Phòng",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Nha Trang",
                    NameEn = "Vinmec Nha Trang Hospital",
                    Address = "Khu đô thị Vinpearl, Vĩnh Nguyên, Nha Trang",
                    Phone = "025 8390 0168",
                    Email = "info.nhatrang@vinmec.com",
                    City = "Khánh Hòa",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Hạ Long",
                    NameEn = "Vinmec Ha Long Hospital",
                    Address = "Đảo Tuần Châu, Hạ Long, Quảng Ninh",
                    Phone = "020 3382 8188",
                    Email = "info.halong@vinmec.com",
                    City = "Quảng Ninh",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Phú Quốc",
                    NameEn = "Vinmec Phu Quoc Hospital",
                    Address = "Bãi Dài, Gành Dầu, Phú Quốc, Kiên Giang",
                    Phone = "029 7398 5588",
                    Email = "info.phuquoc@vinmec.com",
                    City = "Kiên Giang",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Facility
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    NameVi = "Bệnh viện Đa khoa Vinmec Cần Thơ",
                    NameEn = "Vinmec Can Tho Hospital",
                    Address = "Đường 30/4, Xuân Khánh, Ninh Kiều, Cần Thơ",
                    Phone = "029 2368 3003",
                    Email = "info.cantho@vinmec.com",
                    City = "Cần Thơ",
                    Type = FacilityType.Hospital,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Facility>().HasData(facilities);
        }
        /// <summary>
        /// Seed medical specialties based on Vinmec's departments
        /// </summary>
        private void SeedSpecialties(ModelBuilder modelBuilder)
        {
            var specialties = new[]
            {
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"),
                    NameVi = "Trung tâm Tim mạch",
                    NameEn = "Cardiology Center",
                    DescriptionVi = "Chuyên điều trị các bệnh lý tim mạch",
                    DescriptionEn = "Specializes in cardiovascular diseases",
                    DisplayOrder = 1,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"),
                    NameVi = "Trung tâm Ung bướu",
                    NameEn = "Oncology Center",
                    DescriptionVi = "Chuyên điều trị ung thư",
                    DescriptionEn = "Cancer treatment specialists",
                    DisplayOrder = 2,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/oncology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"),
                    NameVi = "Trung tâm Nhi",
                    NameEn = "Pediatrics Center",
                    DescriptionVi = "Chăm sóc sức khỏe trẻ em",
                    DescriptionEn = "Children's healthcare",
                    DisplayOrder = 3,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"),
                    NameVi = "Trung tâm Sức khỏe phụ nữ",
                    NameEn = "Women's Health Center",
                    DescriptionVi = "Sản phụ khoa và sức khỏe sinh sản",
                    DescriptionEn = "Obstetrics and reproductive health",
                    DisplayOrder = 4,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"),
                    NameVi = "Tiêu hóa - Gan mật",
                    NameEn = "Gastroenterology",
                    DescriptionVi = "Chuyên khoa tiêu hóa và gan mật",
                    DescriptionEn = "Digestive system and liver diseases",
                    DisplayOrder = 5,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"),
                    NameVi = "Thần kinh",
                    NameEn = "Neurology",
                    DescriptionVi = "Chẩn đoán và điều trị bệnh lý thần kinh",
                    DescriptionEn = "Neurological disorders diagnosis and treatment",
                    DisplayOrder = 6,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"),
                    NameVi = "Chấn thương chỉnh hình - Y học thể thao",
                    NameEn = "Orthopedics & Sports Medicine",
                    DescriptionVi = "Điều trị chấn thương và chỉnh hình",
                    DescriptionEn = "Trauma and orthopedic treatment",
                    DisplayOrder = 7,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"),
                    NameVi = "Trung tâm Mắt Vinmec-Alina",
                    NameEn = "Eye Center Vinmec-Alina",
                    DescriptionVi = "Chăm sóc mắt toàn diện",
                    DescriptionEn = "Comprehensive eye care",
                    DisplayOrder = 8,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"),
                    NameVi = "Nha khoa Vinmec-View Premium",
                    NameEn = "Vinmec-View Premium Dental",
                    DescriptionVi = "Dịch vụ nha khoa cao cấp",
                    DescriptionEn = "Premium dental services",
                    DisplayOrder = 9,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Specialty
                {
                    Id = Guid.Parse("a0000000-0000-0000-0000-000000000010"),
                    NameVi = "Thẩm mỹ Da liễu",
                    NameEn = "Dermatology & Aesthetics",
                    DescriptionVi = "Chăm sóc da và thẩm mỹ",
                    DescriptionEn = "Skin care and aesthetics",
                    DisplayOrder = 10,
                    IconUrl = "https://vinmec-static.s3.amazonaws.com/icons/cardiology.png",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Specialty>().HasData(specialties);
        }
        /// <summary>
        /// Seed sample doctors
        /// </summary>
        private void SeedDoctors(ModelBuilder modelBuilder)
        {
            var doctors = new[]
            {
                new Doctor
                {
                    Id = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FullName = "TS.BS Nguyễn Văn An",
                    AcademicTitleVi = "Tiến sĩ, Bác sĩ",
                    AcademicTitleEn = "PhD., MD.",
                    BioVi = "Chuyên gia tim mạch với 20 năm kinh nghiệm",
                    BioEn = "Cardiologist with 20 years of experience",
                    SpecialtyId = Guid.Parse("a0000000-0000-0000-0000-000000000001"),
                    Email = "bs.an@vinmec.com",
                    PhoneNumber = "0901234567",
                    YearsOfExperience = 20,
                    AverageRating = 4.8,
                    RatingCount = 150,
                    PhotoUrl = "https://vinmec-static.s3.amazonaws.com/dr-an.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Doctor
                {
                    Id = Guid.Parse("d0000000-0000-0000-0000-000000000002"),
                    FullName = "PGS.TS Trần Thị Bình",
                    AcademicTitleVi = "Phó Giáo sư, Tiến sĩ",
                    AcademicTitleEn = "Assoc. Prof., PhD.",
                    BioVi = "Chuyên gia ung thư hàng đầu",
                    BioEn = "Leading oncology specialist",
                    SpecialtyId = Guid.Parse("a0000000-0000-0000-0000-000000000002"),
                    Email = "pgs.binh@vinmec.com",
                    PhoneNumber = "0902345678",
                    YearsOfExperience = 25,
                    AverageRating = 4.9,
                    RatingCount = 200,
                    PhotoUrl = "https://vinmec-static.s3.amazonaws.com/dr-binh.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Doctor
                {
                    Id = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FullName = "BS.CK2 Lê Văn Cường",
                    AcademicTitleVi = "Bác sĩ Chuyên khoa 2",
                    AcademicTitleEn = "Specialist Doctor Level 2",
                    BioVi = "Bác sĩ Nhi khoa giàu kinh nghiệm",
                    BioEn = "Experienced pediatrician",
                    SpecialtyId = Guid.Parse("a0000000-0000-0000-0000-000000000003"),
                    Email = "bs.cuong@vinmec.com",
                    PhoneNumber = "0903456789",
                    YearsOfExperience = 15,
                    AverageRating = 4.7,
                    RatingCount = 120,
                    PhotoUrl = "https://vinmec-static.s3.amazonaws.com/dr-an.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Doctor
                {
                    Id = Guid.Parse("d0000000-0000-0000-0000-000000000004"),
                    FullName = "TS.BS Phạm Thị Dung",
                    AcademicTitleVi = "Tiến sĩ, Bác sĩ",
                    AcademicTitleEn = "PhD., MD.",
                    BioVi = "Chuyên gia sản phụ khoa",
                    BioEn = "Obstetrics and gynecology specialist",
                    SpecialtyId = Guid.Parse("a0000000-0000-0000-0000-000000000004"),
                    Email = "bs.dung@vinmec.com",
                    PhoneNumber = "0904567890",
                    YearsOfExperience = 18,
                    AverageRating = 4.9,
                    RatingCount = 180,
                    PhotoUrl = "https://vinmec-static.s3.amazonaws.com/dr-an.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Doctor
                {
                    Id = Guid.Parse("d0000000-0000-0000-0000-000000000005"),
                    FullName = "BS.CK1 Hoàng Văn Em",
                    AcademicTitleVi = "Bác sĩ Chuyên khoa 1",
                    AcademicTitleEn = "Specialist Doctor Level 1",
                    BioVi = "Chuyên gia tiêu hóa - gan mật",
                    BioEn = "Gastroenterology specialist",
                    SpecialtyId = Guid.Parse("a0000000-0000-0000-0000-000000000005"),
                    Email = "bs.em@vinmec.com",
                    PhoneNumber = "0905678901",
                    YearsOfExperience = 12,
                    AverageRating = 4.6,
                    RatingCount = 95,
                    PhotoUrl = "https://vinmec-static.s3.amazonaws.com/dr-an.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Doctor>().HasData(doctors);
        }
        /// <summary>
        /// Seed doctor-facility relationships
        /// </summary>
        private void SeedDoctorFacilities(ModelBuilder modelBuilder)
        {
            var doctorFacilities = new[]
            {
                // Doctor 1 - Works at Times City (primary) and Central Park
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IsPrimary = true,
                    AssignedDate = DateTime.UtcNow
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FacilityId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IsPrimary = false,
                    AssignedDate = DateTime.UtcNow
                },
               
                // Doctor 2 - Works at Central Park only
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000002"),
                    FacilityId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IsPrimary = true,
                    AssignedDate = DateTime.UtcNow
                },
               
                // Doctor 3 - Works at Times City and Da Nang
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IsPrimary = true,
                    AssignedDate = DateTime.UtcNow
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IsPrimary = false,
                    AssignedDate = DateTime.UtcNow
                },
               
                // Doctor 4 - Works at Central Park and Da Nang
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000004"),
                    FacilityId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IsPrimary = true,
                    AssignedDate = DateTime.UtcNow
                },
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000004"),
                    FacilityId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IsPrimary = false,
                    AssignedDate = DateTime.UtcNow
                },
               
                // Doctor 5 - Works at Times City only
                new DoctorFacility
                {
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000005"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IsPrimary = true,
                    AssignedDate = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<DoctorFacility>().HasData(doctorFacilities);
        }
        /// <summary>
        /// Seed doctor languages (Vietnamese and English only)
        /// </summary>
        private void SeedDoctorLanguages(ModelBuilder modelBuilder)
        {
            var doctorLanguages = new[]
            {
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"), Language = Language.Vi },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"), Language = Language.En },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000002"), Language = Language.Vi },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000002"), Language = Language.En },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"), Language = Language.Vi },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000004"), Language = Language.Vi },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000004"), Language = Language.En },
                new DoctorLanguage { DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000005"), Language = Language.Vi }
            };
            modelBuilder.Entity<DoctorLanguage>().HasData(doctorLanguages);
        }
        /// <summary>
        /// Seed doctor working schedules
        /// </summary>
        private void SeedDoctorAvailabilities(ModelBuilder modelBuilder)
        {
            var availabilities = new[]
            {
                // Doctor 1 - Monday, Wednesday, Friday mornings at Times City
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000001"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                },
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000002"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                },
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000003"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Friday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                },
               
                // Doctor 2 - Tuesday and Thursday afternoons at Central Park
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000004"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000002"),
                    FacilityId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    SlotDurationMinutes = 20,
                    IsActive = true
                },
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000005"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000002"),
                    FacilityId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    DayOfWeek = DayOfWeek.Thursday,
                    StartTime = new TimeSpan(13, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    SlotDurationMinutes = 20,
                    IsActive = true
                },
               
                // Doctor 3 - Monday to Wednesday mornings at Times City
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000006"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                },
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000007"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                },
                new DoctorAvailability
                {
                    Id = Guid.Parse("da000000-0000-0000-0000-000000000008"),
                    DoctorId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DayOfWeek = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0),
                    SlotDurationMinutes = 15,
                    IsActive = true
                }
            };
            modelBuilder.Entity<DoctorAvailability>().HasData(availabilities);
        }
        /// <summary>
        /// Seed bilingual message translations (Vietnamese and English only)
        /// </summary>
        private void SeedMessageTranslations(ModelBuilder modelBuilder)
        {
            var translations = new List<MessageTranslation>();
            // Define all message codes with Vietnamese and English translations
            var messages = new Dictionary<string, (string vi, string en)>
            {
                // Success messages (2xxx)
                ["APP_MESSAGE_2000"] = ("Thành công", "Success"),
                ["APP_MESSAGE_2001"] = ("Đặt lịch khám thành công", "Appointment booked successfully"),
                ["APP_MESSAGE_2002"] = ("Lịch hẹn đang chờ xác nhận", "Appointment pending confirmation"),
                ["APP_MESSAGE_2003"] = ("Lịch hẹn đã được xác nhận", "Appointment confirmed"),
                ["APP_MESSAGE_2004"] = ("Hủy lịch hẹn thành công", "Appointment cancelled successfully"),
                ["APP_MESSAGE_2005"] = ("Tạo hồ sơ bệnh nhân thành công", "Patient record created successfully"),
                ["APP_MESSAGE_2006"] = ("Cập nhật thông tin bệnh nhân thành công", "Patient information updated successfully"),
                // Client error messages (4xxx)
                ["APP_MESSAGE_4001"] = ("Số điện thoại không hợp lệ", "Invalid phone number"),
                ["APP_MESSAGE_4002"] = ("Ngày sinh không hợp lệ", "Invalid date of birth"),
                ["APP_MESSAGE_4003"] = ("Thiếu trường bắt buộc", "Missing required field"),
                ["APP_MESSAGE_4004"] = ("Thời gian khám không hợp lệ", "Invalid appointment time"),
                ["APP_MESSAGE_4005"] = ("Thời gian khám đã qua", "Appointment time is in the past"),
                ["APP_MESSAGE_4006"] = ("Bác sĩ không có lịch khám", "Doctor not available"),
                ["APP_MESSAGE_4007"] = ("Khung giờ đã được đặt", "Time slot already booked"),
                ["APP_MESSAGE_4008"] = ("Không tìm thấy cơ sở y tế", "Facility not found"),
                ["APP_MESSAGE_4009"] = ("Không tìm thấy chuyên khoa", "Specialty not found"),
                ["APP_MESSAGE_4010"] = ("Không tìm thấy bệnh nhân", "Patient not found"),
                ["APP_MESSAGE_4011"] = ("Không tìm thấy bác sĩ", "Doctor not found"),
                ["APP_MESSAGE_4012"] = ("Không tìm thấy lịch hẹn", "Appointment not found"),
                ["APP_MESSAGE_4013"] = ("Trạng thái lịch hẹn không hợp lệ", "Invalid appointment status"),
                ["APP_MESSAGE_4014"] = ("Không có quyền truy cập", "Unauthorized access"),
                ["APP_MESSAGE_4015"] = ("Lịch hẹn trùng lặp", "Duplicate appointment"),
                ["APP_MESSAGE_4016"] = ("Thông tin đăng nhập không đúng", "Invalid credentials"),
                ["APP_MESSAGE_4017"] = ("Email đã tồn tại trong hệ thống", "Email already exists"),
                ["APP_MESSAGE_4018"] = ("Số điện thoại đã được sử dụng", "Phone number already in use"),
                ["APP_MESSAGE_4019"] = ("Dữ liệu không hợp lệ", "General validation error"),
                // Server error messages (5xxx)
                ["APP_MESSAGE_5000"] = ("Lỗi hệ thống", "Internal server error"),
                ["APP_MESSAGE_5001"] = ("Lỗi cơ sở dữ liệu", "Database error"),
                ["APP_MESSAGE_5002"] = ("Dịch vụ không khả dụng", "Service unavailable"),
                ["APP_MESSAGE_5003"] = ("Lỗi dịch vụ bên ngoài", "External service error")
            };
            // Create translation entries for each message in both languages
            int counter = 1;
            foreach (var message in messages)
            {
                // Vietnamese translation
                translations.Add(new MessageTranslation
                {
                    Id = Guid.Parse($"{counter:D8}-0000-0000-0000-000000000001"),
                    Code = message.Key,
                    Language = "vi",
                    Text = message.Value.vi,
                    CreatedAt = DateTime.UtcNow
                });
                // English translation
                translations.Add(new MessageTranslation
                {
                    Id = Guid.Parse($"{counter:D8}-0000-0000-0000-000000000002"),
                    Code = message.Key,
                    Language = "en",
                    Text = message.Value.en,
                    CreatedAt = DateTime.UtcNow
                });
                counter++;
            }
            modelBuilder.Entity<MessageTranslation>().HasData(translations);
        }
        /// <summary>
        /// Seed system users (admin and sample doctor user)
        /// Note: Passwords should be properly hashed in production
        /// Adjusted roles to match the simplified 3-role system
        /// </summary>
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            var users = new[]
            {
                new User
                {
                    Id = Guid.Parse("f0000000-0000-0000-0000-000000000001"),
                    Username = "admin",
                    // TODO: Hash this password properly using BCrypt or Identity
                    PasswordHash = PasswordHelper.HashPassword("Admin@123!"),
                    FullName = "Quản trị viên hệ thống",
                    Email = "admin@vinmec.com",
                    PhoneNumber = "0900000000",
                    Role = SystemRole.ITAdmin,
                    FacilityId = null,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("f0000000-0000-0000-0000-000000000002"),
                    Username = "doctor.an",
                    // TODO: Hash this password properly using BCrypt or Identity
                    PasswordHash = PasswordHelper.HashPassword("Doctor@123!"),
                    FullName = "TS.BS Nguyễn Văn An",
                    Email = "bs.an@vinmec.com",
                    PhoneNumber = "0901234567",
                    Role = SystemRole.Manager, // Adjusted to Manager as per simplified roles (assuming managers include doctors)
                    FacilityId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}