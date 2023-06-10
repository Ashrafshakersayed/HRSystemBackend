using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_Employees.Entities;
using AutoMapper;
using HR_Employees.Dtos;

namespace HR_Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigningController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SigningController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Signing
        [HttpGet("GetWorkingHours")]
        public async Task<ActionResult<IEnumerable<WorkingHourDto>>> GetWorkingHours()
        {
            if (_context.WorkingHours == null)
            {
                return NotFound();
            }
            return _mapper
                .Map<List<WorkingHourDto>>( _context.WorkingHours.Where(w=>w.SignoutTime == null));
        }

        // GET: api/Signing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkingHour>> GetWorkingHour(int id)
        {
            if (_context.WorkingHours == null)
            {
                return NotFound();
            }
            var workingHour = await _context.WorkingHours.FindAsync(id);

            if (workingHour == null)
            {
                return NotFound();
            }

            return workingHour;
        }

        // PUT: api/Signing/SignOut/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("SignOut/{id}")]
        public async Task<IActionResult> SignOut(int id)
        {

            var workingHour = _context.WorkingHours.FirstOrDefault(w => w.EmployeeID == id && w.SignoutTime == null);

            if (workingHour == null)
            {
                return BadRequest();
            }

            workingHour.SignoutTime = DateTime.Now;
            workingHour.WorkingHours = workingHour.SignoutTime - workingHour.SigninTime;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingHourExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Signing/SignIn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("SignIn/{id}")]
        public async Task<ActionResult<WorkingHour>> SignIn(int id)
        {
            if (_context.WorkingHours == null)
            {
                return Problem("Entity set 'DBContext.WorkingHours'  is null.");
            }
            var workingHour = _context.WorkingHours.FirstOrDefault(w => w.EmployeeID == id && w.SignoutTime == null);

            if (workingHour != null)
            {
                return BadRequest("Already Sign In");
            }
            workingHour = new WorkingHour
            {
                EmployeeID = id,
                SigninTime = DateTime.Now,


            };
            _context.WorkingHours.Add(workingHour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkingHour", new { id = workingHour.Id }, workingHour);
        }

        // DELETE: api/Signing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkingHour(int id)
        {
            if (_context.WorkingHours == null)
            {
                return NotFound();
            }
            var workingHour = await _context.WorkingHours.FindAsync(id);
            if (workingHour == null)
            {
                return NotFound();
            }

            _context.WorkingHours.Remove(workingHour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkingHourExists(int id)
        {
            return (_context.WorkingHours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
