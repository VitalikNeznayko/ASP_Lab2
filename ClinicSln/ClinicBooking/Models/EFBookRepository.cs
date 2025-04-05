
using Microsoft.EntityFrameworkCore.Migrations;


namespace ClinicBooking.Models
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
