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
    public class DoctorsController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public DoctorsController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific doctor based on searched name / get all doctors
        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> Get([FromQuery] string search)
        {
            List<Doctor> doctors = new List<Doctor>();
            if (!string.IsNullOrEmpty(search))
            {
                doctors = await _clinicAppointmentSystemDbContext.Doctors.Where(x => x.User.FirstName.Contains(search) || x.User.LastName.Contains(search) || x.User.DOB.ToString().Contains(search)).ToListAsync();
            }
            else
            {
                doctors = await _clinicAppointmentSystemDbContext.Doctors.ToListAsync();
            }
            return doctors;
        }

        //GET doctor by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> Get(int id)
        {
            var doctor = await _clinicAppointmentSystemDbContext.Doctors
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctor == null) return BadRequest();

            return doctor;
        }

        //CREATE doctor
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Doctor doctor)
        {
            _clinicAppointmentSystemDbContext.Doctors.Add(doctor);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return doctor.Id;
        }

        //UPDATE doctor by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Doctors.Update(doctor);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE doctor by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _clinicAppointmentSystemDbContext.Doctors
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctor == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Doctors.Remove(doctor);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
