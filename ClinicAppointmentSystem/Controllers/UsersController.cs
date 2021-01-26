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
    public class UsersController : ControllerBase
    {
        private readonly ClinicAppointmentSystemDbContext _clinicAppointmentSystemDbContext;
        public UsersController(ClinicAppointmentSystemDbContext clinicAppointmentSystemDbContext)
        {
            _clinicAppointmentSystemDbContext = clinicAppointmentSystemDbContext;
        }
        //GET specific user based on searched name / get all users
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get([FromQuery] string search)
        {
            List<User> users = new List<User>();
            if (!string.IsNullOrEmpty(search))
            {
                users = await _clinicAppointmentSystemDbContext.Users.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.DOB.ToString().Contains(search)).ToListAsync();
            }
            else
            {
                users = await _clinicAppointmentSystemDbContext.Users.ToListAsync();
            }
            return users;
        }

        //GET user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _clinicAppointmentSystemDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();

            return user;
        }

        //CREATE user
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] User user)
        {
            _clinicAppointmentSystemDbContext.Users.Add(user);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return user.Id;
        }

        //UPDATE user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id) return BadRequest();

            _clinicAppointmentSystemDbContext.Users.Update(user);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _clinicAppointmentSystemDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest();
            _clinicAppointmentSystemDbContext.Users.Remove(user);
            await _clinicAppointmentSystemDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
