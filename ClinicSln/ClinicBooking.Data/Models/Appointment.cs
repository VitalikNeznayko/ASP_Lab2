using ClinicBooking.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBooking.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PacientID { get; set; } 
        public Pacient? Pacient { get; set; } = null!;

        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; } = string.Empty;
    }
}