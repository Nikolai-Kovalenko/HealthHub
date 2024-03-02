using HealthHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace HealthHub.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Определение первичного ключа для IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });


            modelBuilder.Entity<Appointment>().ToTable(nameof(Appointment), "dbo", u => u.IsTemporal());

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.FK_UserId)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.FK_DoctorId)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<MedCard>().ToTable(nameof(MedCard), "dbo", u => u.IsTemporal());

            modelBuilder.Entity<MedCard>()
                .HasOne(m => m.FK_UserId)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedCard>()
                .HasOne(m => m.FK_CreateUserId)
                .WithMany()
                .HasForeignKey(m => m.CreateUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedCard>()
                .HasOne(m => m.FK_ChangeUserId)
                .WithMany()
                .HasForeignKey(m => m.ChangeUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientDoctorRelation>().ToTable(nameof(PatientDoctorRelation), "dbo", u => u.IsTemporal());

            modelBuilder.Entity<PatientDoctorRelation>()
                .HasOne(pdr => pdr.FK_UserId)
                .WithMany()
                .HasForeignKey(pdr => pdr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientDoctorRelation>()
                .HasOne(pdr => pdr.FK_DoctorId)
                .WithMany()
                .HasForeignKey(pdr => pdr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }  
        public DbSet<MedCard> MedCards { get; set; }
        public DbSet<PatientDoctorRelation> PatientDoctorRelations { get; set; }
    }
}