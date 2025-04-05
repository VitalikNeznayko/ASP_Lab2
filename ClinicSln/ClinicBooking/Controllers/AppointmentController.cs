using ClinicBooking.Models.ViewModels;
using ClinicBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class AppointmentController : Controller
{
    private readonly BookDbContext _context;

    public AppointmentController(BookDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        var doctors = _context.Doctors.ToList();
        var pacients = _context.Pacients.ToList();
        var viewModel = new AppointmentViewModel
        {
            Doctors = doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorID.ToString(),
                Text = $"{d.DoctorName} - {d.Specialization}"
            }).ToList(),
            Pacients = pacients.Select(d => new SelectListItem
            {
                Value = d.PacientID.ToString(),
                Text = $"{d.FullName}"
            }).ToList(),
        };

        var appointmentFormJson = HttpContext.Session.GetString("AppointmentForm");
        if (!string.IsNullOrEmpty(appointmentFormJson))
        {
            var sessionData = JsonConvert.DeserializeObject<AppointmentViewModel>(appointmentFormJson);
            viewModel.DoctorID = sessionData.DoctorID;
            viewModel.PacientID = sessionData.PacientID;
            viewModel.AppointmentDate = sessionData.AppointmentDate;
            viewModel.Notes = sessionData.Notes;
        }

        else
        {
            var doctorID = HttpContext.Session.GetString("DoctorID");
            var pacientID = HttpContext.Session.GetString("PacientID");
            var appointmentDate = HttpContext.Session.GetString("AppointmentDate");
            var notes = HttpContext.Session.GetString("Notes");

            if (doctorID != null && pacientID != null && appointmentDate != null)
            {
                viewModel.DoctorID = int.Parse(doctorID);
                viewModel.PacientID = int.Parse(pacientID);
                viewModel.AppointmentDate = DateTime.Parse(appointmentDate);
                viewModel.Notes = notes;
            }
            if(appointmentDate == null)
            {
                viewModel.AppointmentDate = DateTime.Now;
            }
        }
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create(AppointmentViewModel appointmentViewModel)
    {
        if (ModelState.IsValid)
        {
            HttpContext.Session.SetString("DoctorID", appointmentViewModel.DoctorID.ToString());
            HttpContext.Session.SetString("PacientID", appointmentViewModel.PacientID.ToString());
            HttpContext.Session.SetString("AppointmentDate", appointmentViewModel.AppointmentDate.ToString());
            HttpContext.Session.SetString("Notes", appointmentViewModel.Notes ?? "");

            var json = JsonConvert.SerializeObject(appointmentViewModel);
            HttpContext.Session.SetString("AppointmentForm", json);

            return RedirectToAction("Confirm");
        }
        return View(appointmentViewModel);
    }

    public IActionResult Confirm()
    {
        var doctorID = HttpContext.Session.GetString("DoctorID");
        var pacientID = HttpContext.Session.GetString("PacientID");
        var appointmentDate = HttpContext.Session.GetString("AppointmentDate");
        var notes = HttpContext.Session.GetString("Notes");

        if (doctorID == null || pacientID == null || appointmentDate == null)
        {
            return RedirectToAction("Create");
        }

        var pacients = _context.Pacients.ToList();
        var pacient = pacients.FirstOrDefault(d => d.PacientID == int.Parse(pacientID));
        if (pacient == null) return RedirectToAction("Create");

        var doctors = _context.Doctors.ToList();
        var doctor = doctors.FirstOrDefault(d => d.DoctorID == int.Parse(doctorID));
        if (doctor == null) return RedirectToAction("Create");

        var appointmentDetails = new AppointmentViewModel
        {
            DoctorID = int.Parse(doctorID),
            PacientID = int.Parse(pacientID),
            AppointmentDate = DateTime.Parse(appointmentDate),
            Notes = notes,
            Doctors = doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorID.ToString(),
                Text = $"{d.DoctorName} - {d.Specialization}"
            }).ToList(),
            Pacients = pacients.Select(d => new SelectListItem
            {
                Value = d.PacientID.ToString(),
                Text = $"{d.FullName}"
            }).ToList()
        };

        return View(appointmentDetails);
    }

    [HttpPost]
    public IActionResult ConfirmAppointment()
    {
        var json = HttpContext.Session.GetString("AppointmentForm");

        if (string.IsNullOrEmpty(json))
        {
            return RedirectToAction("Create");
        }

        var appointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(json);

        if (!appointmentViewModel.DoctorID.HasValue ||
            !appointmentViewModel.PacientID.HasValue ||
            !appointmentViewModel.AppointmentDate.HasValue)
        {
            return RedirectToAction("Create");
        }

        var appointment = new Appointment
        {
            DoctorID = appointmentViewModel.DoctorID.Value,
            PacientID = appointmentViewModel.PacientID.Value,
            AppointmentDate = appointmentViewModel.AppointmentDate.Value,
            Notes = appointmentViewModel.Notes
        };

        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult SaveAppointmentSession()
    {
        var model = new AppointmentViewModel
        {
            DoctorID = int.TryParse(Request.Form["DoctorID"], out int doctorID) ? (int?)doctorID : null,
            PacientID = int.TryParse(Request.Form["PacientID"], out int pacientID) ? (int?)pacientID : null,
            AppointmentDate = DateTime.TryParse(Request.Form["AppointmentDate"], out DateTime appointmentDate) ? (DateTime?)appointmentDate : null,
            Notes = Request.Form["Notes"].FirstOrDefault()
        };

        var json = JsonConvert.SerializeObject(model);
        HttpContext.Session.SetString("AppointmentForm", json);

        return Ok();
    }

}