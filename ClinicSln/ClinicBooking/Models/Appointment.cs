using ClinicBooking.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBooking.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PacientID { get; set; } 
        public Pacient Pacient { get; set; } = null!;

        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; } 
        public string Notes { get; set; } = string.Empty;
    }
}