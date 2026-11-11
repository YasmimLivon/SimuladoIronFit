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
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriarAlunoDto>>> GetAlunos()
        {
            return await _context.Alunos.Select(a => new CriarAlunoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Telefone = a.Telefone,
                CPF = a.CPF
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CriarAlunoDto>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return new CriarAlunoDto
            {
                Nome = aluno.Nome,
                Telefone = aluno.Telefone
            };

        }

            [HttpPost]
            public async Task<ActionResult<CriarAlunoDto>> CreateAluno(CriarAlunoDto alunoDto)
            {
                var aluno = new Aluno
                {
                    Nome = alunoDto.Nome,
                    Telefone = alunoDto.Telefone,
                    CPF = alunoDto.CPF,
                    PersonalId = alunoDto.PersonalId
                };

                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, alunoDto);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, CriarAlunoDto alunoDto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            aluno.Nome = alunoDto.Nome;
            aluno.Telefone = alunoDto.Telefone;
            aluno.CPF = alunoDto.CPF;
            aluno.PersonalId = alunoDto.PersonalId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return NoContent();
        }
      }
    }
