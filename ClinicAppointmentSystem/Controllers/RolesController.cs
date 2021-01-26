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
    public class RolesController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public RolesController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }

        //GET specific role based on searched name / get all roles
        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get([FromQuery] string search)
        {
            List<Role> roles = new List<Role>();
            if (!string.IsNullOrEmpty(search))
            {
                roles = await _clinicAppointmentSystemDbContext.Roles.Where(x => x.Name.Contains(search)).ToListAsync();
            }
            else
            {
                roles = await _clinicAppointmentSystemDbContext.Roles.ToListAsync();
            }
            return roles;
        }

        //GET role by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var role = await _clinicAppointmentSystemDbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role == null) return BadRequest();

            return role;
        }

        //CREATE role
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Role role)
        {
            _clinicAppointmentSystemDbContext.Roles.Add(role);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return role.Id;
        }

        //UPDATE role by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role role)
        {
            if (id != role.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Roles.Update(role);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE role by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _clinicAppointmentSystemDbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == id);
            if (role == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Roles.Remove(role);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
