using ClinicBooking.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Models
{
    namespace SportsStore.Models
    {
        public interface IBookRepository
        {
            IQueryable<Appointment> Appointments { get; }
            IQueryable<Pacient> Pacients { get; }
            IQueryable<Doctor> Doctors { get; }
        }
    }

}
