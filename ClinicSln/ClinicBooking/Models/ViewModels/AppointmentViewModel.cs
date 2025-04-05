using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicBooking.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public int? DoctorID { get; set; }
        public int? PacientID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<SelectListItem> Doctors { get; set; } = new();
        public List<SelectListItem> Pacients { get; set; } = new();
    }
}
