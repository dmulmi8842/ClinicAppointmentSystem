using ClinicAppointmentSystem.Data;
using ClinicAppointmentSystem.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecializationsController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public DoctorSpecializationsController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific doctor specialization based on searched name / get all doctor specializations
        [HttpGet]
        public async Task<ActionResult<List<DoctorSpecialization>>> Get([FromQuery] string search)
        {
            List<DoctorSpecialization> doctorSpecializations = new List<DoctorSpecialization>();
            if (!string.IsNullOrEmpty(search))
            {
                doctorSpecializations = await _clinicAppointmentSystemDbContext.DoctorSpecializations.Where(x => x.Specialization.Name.Contains(search)).ToListAsync();
            }
            else
            {
                doctorSpecializations = await _clinicAppointmentSystemDbContext.DoctorSpecializations.ToListAsync();
            }
            return doctorSpecializations;
        }

        //GET doctor specialization by id
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorSpecialization>> Get(int id)
        {
            var doctorSpecialization = await _clinicAppointmentSystemDbContext.DoctorSpecializations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctorSpecialization == null) return BadRequest();

            return doctorSpecialization;
        }

        //CREATE doctor specialization
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] DoctorSpecialization doctorSpecialization)
        {
            _clinicAppointmentSystemDbContext.DoctorSpecializations.Add(doctorSpecialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return doctorSpecialization.Id;
        }

        //UPDATE doctor specialization by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorSpecialization doctorSpecialization)
        {
            if (id != doctorSpecialization.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.DoctorSpecializations.Update(doctorSpecialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE doctor specialization by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctorSpecialization = await _clinicAppointmentSystemDbContext.DoctorSpecializations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctorSpecialization == null) return BadRequest();
            _clinicAppointmentSystemDbContext.DoctorSpecializations.Remove(doctorSpecialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
