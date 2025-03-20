using Microsoft.AspNetCore.Mvc;
using ClinicBooking.Models;
using ClinicBooking.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace ClinicBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDbContext _context;

        public HomeController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 3;

            var totalItems = _context.Appointments.Count();

            var appointments = _context.Appointments
                .Include(a => a.Pacient)
                .Include(a => a.Doctor) 
                .OrderBy(a => a.AppointmentDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList(); 

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
