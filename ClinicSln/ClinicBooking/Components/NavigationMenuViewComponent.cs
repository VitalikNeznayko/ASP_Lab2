using ClinicBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly BookDbContext _context;

        public NavigationMenuViewComponent(BookDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var specializations = _context.Doctors
                .Select(d => d.Specialization)
                .Distinct()
                .OrderBy(s => s);
            string? selectedSpecialization = Request.Query["specialization"];

            ViewBag.SelectedSpecialization = selectedSpecialization;

            return View(specializations);
        }
    }
}
