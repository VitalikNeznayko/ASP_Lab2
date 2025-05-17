    using ClinicBooking.Hubs;
    using ClinicBooking.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    namespace ClinicBooking.Controllers
    {
        public class AppointmentsController : Controller
        {
            private readonly BookDbContext _context;
            private readonly IHubContext<AppointmentHub> _hubContext;

            public AppointmentsController(BookDbContext context, IHubContext<AppointmentHub> hubContext)
            {
                _context = context;
                _hubContext = hubContext;
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
                    var newAppointment = await _context.Appointments
                        .Include(a => a.Doctor)
                        .Include(a => a.Pacient)
                        .FirstOrDefaultAsync(a => a.AppointmentID == appointment.AppointmentID);

                    await _hubContext.Clients.All.SendAsync("ReceiveAppointmentCreate", newAppointment);

                    await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Новий запис додано!");

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
                        var updatedAppointment = await _context.Appointments
                            .Include(a => a.Doctor)
                            .Include(a => a.Pacient)
                            .FirstOrDefaultAsync(a => a.AppointmentID == appointment.AppointmentID);

                        await _hubContext.Clients.All.SendAsync("ReceiveAppointmentEdit", updatedAppointment);
                        await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Запис оновлено!");
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

                    await _hubContext.Clients.All.SendAsync("ReceiveAppointmentDelete", id);
                    await _hubContext.Clients.All.SendAsync("ReceiveAppointmentUpdate", "Запис видалено!");
                }
                return RedirectToAction(nameof(Index));
            }

            private bool AppointmentExists(int id)
            {
                return _context.Appointments.Any(e => e.AppointmentID == id);
            }
        }
    }