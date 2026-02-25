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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...existing configuration...
            ConfigureGuidKeys(modelBuilder);
            ConfigureCompositeKeys(modelBuilder);
            ConfigureRelationships(modelBuilder);
            ConfigureIndexes(modelBuilder);

            // Seed data (call each seed class)
            SeedData.SpecialtySeed.Seed(modelBuilder);
            SeedData.FacilitySeed.Seed(modelBuilder);
            SeedData.DoctorSeed.Seed(modelBuilder);
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
    }
}