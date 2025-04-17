using ClinicBooking.Models;
using System.Numerics;

public class EFBookRepository : IBookRepository
{
    private readonly BookDbContext context;

    public EFBookRepository(BookDbContext ctx)
    {
        context = ctx;
    }

    public IQueryable<Doctor> Doctors => context.Doctors;
    public IQueryable<Pacient> Pacients => context.Pacients;
    public IQueryable<Appointment> Appointments => context.Appointments;
    public void AddPacient(Pacient pacient)
    {
        context.Pacients.Add(pacient);
        context.SaveChanges();
    }

    public void UpdatePacient(Pacient pacient)
    {
        context.Pacients.Update(pacient);
        context.SaveChanges();
    }

    public void DeletePacient(Pacient pacient)
    {
        context.Pacients.Remove(pacient);
        context.SaveChanges();
    }

    public Pacient? GetPacientById(int id)
    {
        return context.Pacients
            .FirstOrDefault(p => p.PacientID == id);
    }
    public void AddDoctor(Doctor doctor)
    {
        context.Doctors.Add(doctor);
        context.SaveChanges();
    }

    public void UpdateDoctor(Doctor doctor)
    {
        context.Doctors.Update(doctor);
        context.SaveChanges();
    }

    public void DeleteDoctor(Doctor doctor)
    {
        context.Doctors.Remove(doctor);
        context.SaveChanges();
    }

    public Doctor? GetDoctorById(int id)
    {
        return context.Doctors
            .FirstOrDefault(d => d.DoctorID == id);
    }

    public void AddAppointment(Appointment appointment)
    {
        context.Appointments.Add(appointment);
        context.SaveChanges();
    }

    public void UpdateAppointment(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        context.SaveChanges();
    }

    public void DeleteAppointment(Appointment appointment)
    {
        context.Appointments.Remove(appointment);
        context.SaveChanges();
    }

    public Appointment? GetAppointmentById(int id)
    {
        return context.Appointments
            .FirstOrDefault(a => a.AppointmentID == id);
    }
}
