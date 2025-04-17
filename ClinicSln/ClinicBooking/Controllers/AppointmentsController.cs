using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicBooking.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly BookDbContext _context;

        public AppointmentsController(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookDbContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Pacient);
            return View(await bookDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Pacient)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public IActionResult Create()
        {
            ViewData["DoctorID"] = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    DisplayText = $"{d.DoctorName} ({d.Specialization})"
                }),
                "DoctorID",
                "DisplayText"
            );
            ViewBag.PacientID = new SelectList(_context.Pacients, "PacientID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacientID,DoctorID,AppointmentDate,Notes")] Appointment appointment)
        {
            ModelState.Remove("AppointmentID");

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DoctorID = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    DisplayText = $"{d.DoctorName} ({d.Specialization})"
                }),
                "DoctorID",
                "DisplayText",
                appointment.DoctorID
            );
            ViewBag.PacientID = new SelectList(_context.Pacients, "PacientID", "FullName", appointment.PacientID);
            return View(appointment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.DoctorID = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    DisplayText = $"{d.DoctorName} ({d.Specialization})"
                }),
                "DoctorID",
                "DisplayText",
                appointment.DoctorID
            );
            ViewBag.PacientID = new SelectList(_context.Pacients, "PacientID", "FullName", appointment.PacientID);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentID,PacientID,DoctorID,AppointmentDate,Notes")] Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DoctorID = new SelectList(
                _context.Doctors.Select(d => new
                {
                    d.DoctorID,
                    DisplayText = $"{d.DoctorName} ({d.Specialization})"
                }),
                "DoctorID",
                "DisplayText",
                appointment.DoctorID
            );
            ViewBag.PacientID = new SelectList(_context.Pacients, "PacientID", "FullName", appointment.PacientID);
            return View(appointment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Pacient)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentID == id);
        }
    }
}