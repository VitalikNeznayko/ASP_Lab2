
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicBooking.Models
{

    public interface IBookRepository
    {
        IQueryable<Appointment> Appointments { get; }
        IQueryable<Pacient> Pacients { get; }
        IQueryable<Doctor> Doctors { get; }
    }

}
