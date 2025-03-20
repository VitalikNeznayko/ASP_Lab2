using ClinicBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Pacient> Pacients => Set<Pacient>();
    }
}
