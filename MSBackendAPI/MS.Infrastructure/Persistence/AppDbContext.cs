using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
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
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<FacilitySpecialty> FacilitySpecialties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...existing configuration...
            ConfigureGuidKeys(modelBuilder);
            ConfigureCompositeKeys(modelBuilder);
            ConfigureSoftDeleteFilters(modelBuilder);
            ConfigureRelationships(modelBuilder);
            ConfigureIndexes(modelBuilder);

            // Seed data (call each seed class)
            SeedData.SpecialtySeed.Seed(modelBuilder);
            SeedData.FacilitySeed.Seed(modelBuilder);
            SeedData.DoctorSeed.Seed(modelBuilder);
            SeedData.DoctorFacilitySeed.Seed(modelBuilder);
            SeedData.PatientSeed.Seed(modelBuilder);
            SeedData.UserSeed.Seed(modelBuilder);
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
            modelBuilder.Entity<MedicalRecord>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Review>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Favorite>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Notification>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PasswordResetToken>()
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
            modelBuilder.Entity<FacilitySpecialty>()
                .HasKey(fs => new { fs.FacilityId, fs.SpecialtyId });
        }
        /// <summary>
        /// Configure global query filters for soft delete entities
        /// Automatically excludes soft-deleted records from queries
        /// </summary>
        private void ConfigureSoftDeleteFilters(ModelBuilder modelBuilder)
        {
            // Apply soft delete filter for all ISoftDeletable entities
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Doctor>()
                .HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Patient>()
                .HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Facility>()
                .HasQueryFilter(f => !f.IsDeleted);
            modelBuilder.Entity<Specialty>()
                .HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<DoctorAvailability>()
                .HasQueryFilter(da => !da.IsDeleted);
            modelBuilder.Entity<Appointment>()
                .HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<MedicalRecord>()
                .HasQueryFilter(mr => !mr.IsDeleted);
            modelBuilder.Entity<Review>()
                .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Favorite>()
                .HasQueryFilter(f => !f.IsDeleted);
            modelBuilder.Entity<Notification>()
                .HasQueryFilter(n => !n.IsDeleted);
            modelBuilder.Entity<MessageTranslation>()
                .HasQueryFilter(mt => !mt.IsDeleted);
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
            // Doctor relationships with User
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // MedicalRecord relationships
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Appointment)
                .WithOne(a => a.MedicalRecord)
                .HasForeignKey<MedicalRecord>(mr => mr.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Doctor)
                .WithMany()
                .HasForeignKey(mr => mr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review relationships
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Appointment)
                .WithMany()
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Doctor)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Facility)
                .WithMany(f => f.Reviews)
                .HasForeignKey(r => r.FacilityId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            // Favorite relationships
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Doctor)
                .WithMany(d => d.Favorites)
                .HasForeignKey(f => f.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Facility)
                .WithMany(fac => fac.Favorites)
                .HasForeignKey(f => f.FacilityId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            // Notification relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Appointment)
                .WithMany()
                .HasForeignKey(n => n.AppointmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // PasswordResetToken relationships
            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(prt => prt.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(prt => prt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // FacilitySpecialty relationships
            modelBuilder.Entity<FacilitySpecialty>()
                .HasOne(fs => fs.Facility)
                .WithMany(f => f.Specialties)
                .HasForeignKey(fs => fs.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FacilitySpecialty>()
                .HasOne(fs => fs.Specialty)
                .WithMany(s => s.Facilities)
                .HasForeignKey(fs => fs.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
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
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Email)
                .IsUnique();

            // Review indexes
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.DoctorId, r.Rating });
            modelBuilder.Entity<Review>()
                .HasIndex(r => new { r.FacilityId, r.Rating });
            modelBuilder.Entity<Review>()
                .HasIndex(r => r.PatientId);

            // Favorite indexes
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => f.PatientId);
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.PatientId, f.DoctorId });
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.PatientId, f.FacilityId });

            // Notification indexes
            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.UserId);
            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.IsRead });

            // PasswordResetToken indexes
            modelBuilder.Entity<PasswordResetToken>()
                .HasIndex(prt => prt.Token)
                .IsUnique();
            modelBuilder.Entity<PasswordResetToken>()
                .HasIndex(prt => prt.UserId);

            // MessageTranslation unique index
            modelBuilder.Entity<MessageTranslation>()
                .HasIndex(mt => new { mt.Code, mt.Language })
                .IsUnique();
        }
    }
}