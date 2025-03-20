using ClinicBooking.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models.SportsStore.Models;

namespace SportsStore.Models
{
    public class EFBookRepository : IBookRepository
    {
        private BookDbContext context;
        public EFBookRepository(BookDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Appointment> Appointments => context.Appointments;

        public IQueryable<Pacient> Pacients => context.Pacients;

        public IQueryable<Doctor> Doctors => context.Doctors;
    }
}
