using ClinicAppointmentSystem.Data;
using ClinicAppointmentSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    public class SpecializationsController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public SpecializationsController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific specialization based on searched name / get all specializations
        [HttpGet]
        public async Task<ActionResult<List<Specialization>>> Get([FromQuery] string search)
        {
            List<Specialization> specializations = new List<Specialization>();
            if (!string.IsNullOrEmpty(search))
            {
                specializations = await _clinicAppointmentSystemDbContext.Specializations.Where(x => x.Name.Contains(search)).ToListAsync();
            }
            else
            {
                specializations = await _clinicAppointmentSystemDbContext.Specializations.ToListAsync();
            }
            return specializations;
        }

        //GET specialization by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> Get(int id)
        {
            var specialization = await _clinicAppointmentSystemDbContext.Specializations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (specialization == null) return BadRequest();

            return specialization;
        }

        //CREATE specialization
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Specialization specialization)
        {
            _clinicAppointmentSystemDbContext.Specializations.Add(specialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return specialization.Id;
        }

        //UPDATE specialization by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Specialization specialization)
        {
            if (id != specialization.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Specializations.Update(specialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE specialization by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var specialization = await _clinicAppointmentSystemDbContext.Specializations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (specialization == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Specializations.Remove(specialization);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
