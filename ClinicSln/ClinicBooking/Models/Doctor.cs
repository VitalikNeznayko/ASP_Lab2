using ClinicBooking.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicBooking.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        [Required(ErrorMessage = "Please enter doctor's name")]
        public string DoctorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter specialization")]
        public string Specialization { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        public List<Appointment> Appointments { get; set; } = new();

        [Required(ErrorMessage = "Please enter cabinet number")]
        public string Cabinet { get; set; } = string.Empty;
    }
}