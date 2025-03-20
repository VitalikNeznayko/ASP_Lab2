using ClinicBooking.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBooking.Models
{
    public class Pacient
    {
        public int PacientID { get; set; } 
        public string FullName { get; set; } = string.Empty; 
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string MedicalHistory { get; set; } = string.Empty; 
        public string BloodType { get; set; } = string.Empty;
        public List<Appointment> Appointments { get; set; } = new();
    }
}