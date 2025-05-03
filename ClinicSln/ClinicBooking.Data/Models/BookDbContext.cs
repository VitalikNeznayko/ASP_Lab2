using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Pacient> Pacients => Set<Pacient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pacient)
                .WithMany()
                .HasForeignKey(a => a.PacientID);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorID);
        }
    }
}