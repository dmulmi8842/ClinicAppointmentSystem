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
    public class DoctorAvailabilitiesController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public DoctorAvailabilitiesController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific doctor availability based on searched name / get all doctor availabilities
        [HttpGet]
        public async Task<ActionResult<List<DoctorAvailability>>> Get([FromQuery] string search)
        {
            List<DoctorAvailability> doctorAvailabilities = new List<DoctorAvailability>();
            if (!string.IsNullOrEmpty(search))
            {
                doctorAvailabilities = await _clinicAppointmentSystemDbContext.DoctorAvailabilities.Where(x => x.Date.ToString().Contains(search) || x.Doctor.User.FirstName.Contains(search) || x.Doctor.User.LastName.Contains(search) || x.Doctor.User.DOB.ToString().Contains(search)).ToListAsync();
            }
            else
            {
                doctorAvailabilities = await _clinicAppointmentSystemDbContext.DoctorAvailabilities.ToListAsync();
            }
            return doctorAvailabilities;
        }

        //GET doctor availability by id
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorAvailability>> Get(int id)
        {
            var doctorAvailability = await _clinicAppointmentSystemDbContext.DoctorAvailabilities
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctorAvailability == null) return BadRequest();

            return doctorAvailability;
        }

        //CREATE doctor availability
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] DoctorAvailability doctorAvailability)
        {
            _clinicAppointmentSystemDbContext.DoctorAvailabilities.Add(doctorAvailability);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return doctorAvailability.Id;
        }

        //UPDATE doctor availability by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorAvailability doctorAvailability)
        {
            if (id != doctorAvailability.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.DoctorAvailabilities.Update(doctorAvailability);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE doctor availability by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctorAvailability = await _clinicAppointmentSystemDbContext.DoctorAvailabilities
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctorAvailability == null) return BadRequest();
            _clinicAppointmentSystemDbContext.DoctorAvailabilities.Remove(doctorAvailability);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
