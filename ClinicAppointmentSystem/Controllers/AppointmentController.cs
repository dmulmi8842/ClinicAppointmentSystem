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
    public class AppointmentController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public AppointmentController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific appointment based on searched name / get all appointments
        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> Get([FromQuery] string search)
        {
            List<Appointment> appointments = new List<Appointment>();
            if (!string.IsNullOrEmpty(search))
            {
                appointments = await _clinicAppointmentSystemDbContext.Appointments.Where(x => x.Notes.Contains(search) || x.Date.ToString().Contains(search) || x.Status.ToString().Contains(search) || x.Doctor.User.FirstName.Contains(search) || x.Doctor.User.LastName.Contains(search) || x.Doctor.User.DOB.ToString().Contains(search) || x.Patient.User.FirstName.Contains(search) || x.Patient.User.LastName.Contains(search) || x.Patient.User.DOB.ToString().Contains(search)).ToListAsync();
            }
            else
            {
                appointments = await _clinicAppointmentSystemDbContext.Appointments.ToListAsync();
            }
            return appointments;
        }

        //GET appointment by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> Get(int id)
        {
            var appointment = await _clinicAppointmentSystemDbContext.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null) return BadRequest();

            return appointment;
        }

        //CREATE appointment
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Appointment appointment)
        {
            _clinicAppointmentSystemDbContext.Appointments.Add(appointment);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return appointment.Id;
        }

        //UPDATE appointment by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Appointments.Update(appointment);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE appointment by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _clinicAppointmentSystemDbContext.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Appointments.Remove(appointment);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
