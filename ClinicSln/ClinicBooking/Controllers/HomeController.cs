using Microsoft.AspNetCore.Mvc;
using ClinicBooking.Models;
using ClinicBooking.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDbContext _context;

        public HomeController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? specialization, int page = 1)
        {
            int pageSize = 3;
            var appointmentsQuery = _context.Appointments
                .Include(a => a.Pacient)
                .Include(a => a.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(specialization))
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.Doctor.Specialization == specialization);
            }

            int totalItems = appointmentsQuery.Count();

            var appointments = appointmentsQuery
                .OrderBy(a => a.AppointmentDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.SelectedSpecialization = specialization;

            var viewModel = new ViewModel<Appointment>
            {
                Collections = appointments,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                }
            };

            return View(viewModel);
        }

    }
}

