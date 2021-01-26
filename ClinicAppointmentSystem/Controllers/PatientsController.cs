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
    public class PatientsController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public PatientsController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }
        //GET specific patient based on searched name / get all patients
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> Get([FromQuery] string search)
        {
            List<Patient> patients = new List<Patient>();
            if (!string.IsNullOrEmpty(search))
            {
                patients = await _clinicAppointmentSystemDbContext.Patients.Where(x => x.User.FirstName.Contains(search) || x.User.LastName.Contains(search) || x.User.DOB.ToString().Contains(search)).ToListAsync();
            }
            else
            {
                patients = await _clinicAppointmentSystemDbContext.Patients.ToListAsync();
            }
            return patients;
        }

        // GET patient by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _clinicAppointmentSystemDbContext.Patients
                .FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null) return BadRequest();
            return patient;
        }

        //CREATE patient
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Patient patient)
        {
            _clinicAppointmentSystemDbContext.Patients.Add(patient);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return patient.Id;
        }

        //UPDATE patient by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Patients.Update(patient);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE patient by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _clinicAppointmentSystemDbContext.Patients
                .FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Patients.Remove(patient);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
