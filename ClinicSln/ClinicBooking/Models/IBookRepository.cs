
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicBooking.Models
{

    public interface IBookRepository
    {
        IQueryable<Appointment> Appointments { get; }
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
        Appointment? GetAppointmentById(int id);

        IQueryable<Pacient> Pacients { get; }
        void AddPacient(Pacient pacient);
        void UpdatePacient(Pacient pacient);
        void DeletePacient(Pacient pacient);
        Pacient? GetPacientById(int id);

        IQueryable<Doctor> Doctors { get; }
        void AddDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
        Doctor? GetDoctorById(int id);

    }

}
