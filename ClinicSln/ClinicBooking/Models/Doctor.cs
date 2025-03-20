using ClinicBooking.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBooking.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; } = string.Empty; 
        public string Specialization { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Appointment> Appointments { get; set; } = new();
        public string Cabinet { get; set; } = string.Empty;
    }
}