using ClinicBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public PacientController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pacient>> GetAllPacients()
        {
            return Ok(_repository.Pacients.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Pacient> GetPacient(int id)
        {
            var pacient = _repository.GetPacientById(id);
            if (pacient == null)
                return NotFound();
            return Ok(pacient);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePacient(Pacient pacient)
        {
            _repository.AddPacient(pacient);
            return CreatedAtAction(nameof(GetPacient), new { id = pacient.PacientID }, pacient);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdatePacient(int id, Pacient pacient)
        {
            if (id != pacient.PacientID)
                return BadRequest();

            _repository.UpdatePacient(pacient);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeletePacient(int id)
        {
            var pacient = _repository.GetPacientById(id);
            if (pacient == null)
                return NotFound();

            _repository.DeletePacient(pacient);
            return NoContent();
        }
    }
}
