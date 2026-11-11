using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Models;

namespace SimuladoIronFit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
