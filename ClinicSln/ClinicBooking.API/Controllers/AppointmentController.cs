using ClinicBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public AppointmentController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAllAppointments()
        {
            return Ok(_repository.Appointments.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = _repository.GetAppointmentById(id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAppointment(Appointment appointment)
        {
            _repository.AddAppointment(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentID }, appointment);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentID)
                return BadRequest();

            _repository.UpdateAppointment(appointment);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = _repository.GetAppointmentById(id);
            if (appointment == null)
                return NotFound();

            _repository.DeleteAppointment(appointment);
            return NoContent();
        }
    }
}
