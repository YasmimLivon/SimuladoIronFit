namespace SimuladoIronFit.Models.DTOs
{
    public class CriarTreinoDto
    {
        public DateOnly Data { get; set; }
        public string TipoTreino { get; set; }
        public string NivelDificuldade { get; set; }

        public int PersonalId { get; set; }
        public int AlunoId { get; set; }
    }
}
