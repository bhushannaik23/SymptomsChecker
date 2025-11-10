using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Data;
using Symptoms_Checker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpecialtiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all specialties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialty>>> GetSpecialties()
        {
            var specialties = await _context.Specialties.ToListAsync();
            return Ok(specialties);
        }
    }
}
