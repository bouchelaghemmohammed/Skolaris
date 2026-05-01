using Microsoft.AspNetCore.Mvc;
using Skolaris.Models;
using Skolaris.Services;

namespace Skolaris.Controllers
{
    [ApiController]
    [Route("api/absences")]
    public class AbsencesApiController : ControllerBase
    {
        private readonly AbsenceService _absenceService;

        public AbsencesApiController(AbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        // GET: api/absences
        [HttpGet]
        public IActionResult GetAbsences()
        {
            return Ok(_absenceService.GetAllAbsences());
        }

        // GET: api/absences/{id}
        [HttpGet("{id}")]
        public IActionResult GetAbsence(int id)
        {
            var absence = _absenceService.GetAbsenceById(id);
            if (absence == null)
                return NotFound();
            return Ok(absence);
        }

        // GET: api/absences/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetAbsencesByUser(int userId)
        {
            return Ok(_absenceService.GetAbsencesByUser(userId));
        }

        // GET: api/absences/cours/{coursOffertId} (parallèle à Jeff)
        [HttpGet("cours/{coursOffertId}")]
        public IActionResult GetAbsencesByCoursOffert(int coursOffertId)
        {
            return Ok(_absenceService.GetAbsencesByCoursOffert(coursOffertId));
        }

        // POST: api/absences — Input présence/absence (ABS-01)
        [HttpPost]
        public IActionResult CreateAbsence([FromBody] Absence absence)
        {
            var result = _absenceService.CreateAbsence(absence);
            if (!result)
                return BadRequest("Utilisateur introuvable.");
            return Ok();
        }

        // PUT: api/absences/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAbsence(int id, [FromBody] Absence absence)
        {
            var result = _absenceService.UpdateAbsence(id, absence);
            if (!result)
                return NotFound();
            return Ok();
        }

        // DELETE: api/absences/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAbsence(int id)
        {
            var result = _absenceService.DeleteAbsence(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
