using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace dotnetapp.Controllers
{
    [Route("api/plants")]
    [ApiController]
    [Authorize] 
    public class PlantController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public PlantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var plants = await _context.Plants.ToListAsync();
            return Ok(plants);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Post([FromBody] Plant plant)
        {
            if (plant == null)
            {
                return BadRequest("Plant object is null");
            }

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = plant.PlantId }, plant);
        }
    }
}
