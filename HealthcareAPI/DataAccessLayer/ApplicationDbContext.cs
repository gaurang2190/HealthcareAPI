using HealthcareAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HealthcareAPI.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<HealthcareProfessional> HealthcareProfessionals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appointment>()
             .HasOne(a => a.User)
             .WithMany(u => u.Appointments)
             .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.HealthcareProfessional)
                .WithMany(h => h.Appointments)
                .HasForeignKey(a => a.HealthcareProfessionalId);
        }
    }
}
