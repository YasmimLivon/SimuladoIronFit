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
    public class TreinosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Treino>> CreateTreino(CriarTreinoDto treinoDto)
        {
            var treino = new Treino
            {
                Data = treinoDto.Data,
                TipoTreino = treinoDto.TipoTreino,
                NivelDificuldade = treinoDto.NivelDificuldade,
                PersonalId = treinoDto.PersonalId,
                AlunoId = treinoDto.AlunoId
            };

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTreino), new { id = treino.Id }, treino);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoTreinosDto>>> GetTreinos()
        {
            var treinos = await _context.Treinos
                .Select(t => new InfoTreinosDto
                {
                    Id = t.Id,
                    Data = t.Data,
                    TipoTreino = t.TipoTreino,
                    NivelDificuldade = t.NivelDificuldade,
                    PersonalId = t.PersonalId,
                    AlunoId = t.AlunoId
                })
                .ToListAsync();

            return Ok(treinos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InfoTreinosDto>> GetTreino(int id)
        {
            var treino = await _context.Treinos
                .FirstOrDefaultAsync(t => t.Id == id);
            if (treino == null)
            {
                return NotFound();
            }
            var treinoDto = new InfoTreinosDto
            {
                Id = treino.Id,
                Data = treino.Data,
                TipoTreino = treino.TipoTreino,
                NivelDificuldade = treino.NivelDificuldade,
                PersonalId = treino.PersonalId,
                AlunoId = treino.AlunoId
            };
            
            return Ok(treinoDto);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreino(int id, CriarTreinoDto treinoDto)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            treino.Data = treinoDto.Data;
            treino.TipoTreino = treinoDto.TipoTreino;
            treino.NivelDificuldade = treinoDto.NivelDificuldade;
            treino.PersonalId = treinoDto.PersonalId;
            treino.AlunoId = treinoDto.AlunoId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreino(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
