using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Data;
using SimuladoIronFit.Models;
using SimuladoIronFit.Models.DTOs;

namespace SimuladoIronFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonaisController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> GetPersonais()
        {
            return await _context.Personais.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CriarPersonalDto>> GetPersonal(int id)
        {
            var personal = await _context.Personais.Include(p => p.Nome).FirstOrDefaultAsync(p => p.Id == id);

            if (personal == null)
            {
                return NotFound();
            }

            return new CriarPersonalDto
            {
                Nome = personal.Nome,
                CPF = personal.CPF,
                Telefone = personal.Telefone
            };
        }

        [HttpPost]
        public async Task<ActionResult<Personal>> CreatePersonal(CriarPersonalDto criarPersonalDto)
        {
            var personal = new Personal
            {
                Id = criarPersonalDto.Id,
                Nome = criarPersonalDto.Nome,
               CPF = criarPersonalDto.CPF,
               Telefone = criarPersonalDto.Telefone
            };

            _context.Personais.Add(personal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonal), new { id = personal.Id }, personal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonal(int id, CriarPersonalDto criarPersonalDto)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            personal.Nome = criarPersonalDto.Nome;
            personal.CPF = criarPersonalDto.CPF;
            personal.Telefone = criarPersonalDto.Telefone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal = await _context.Personais.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            _context.Personais.Remove(personal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
