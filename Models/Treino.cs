namespace SimuladoIronFit.Models
{
    public class Treino
    {
        public int Id { get; set; }
        public DateOnly Data { get; set; }
        public string TipoTreino { get; set; }
        public string NivelDificuldade { get; set; }

        public int PersonalId { get; set; }
        public int AlunoId { get; set; }
    }
}
