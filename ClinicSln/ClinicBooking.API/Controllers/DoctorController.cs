using ClinicBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public DoctorController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Ok(_repository.Doctors.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctor(int id)
        {
            var doctor = _repository.GetDoctorById(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateDoctor(Doctor doctor)
        {
            _repository.AddDoctor(doctor);
            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorID }, doctor);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.DoctorID)
                return BadRequest();

            _repository.UpdateDoctor(doctor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDoctor(int id)
        {
            var doctor = _repository.GetDoctorById(id);
            if (doctor == null)
                return NotFound();

            _repository.DeleteDoctor(doctor);
            return NoContent();
        }
    }
}
