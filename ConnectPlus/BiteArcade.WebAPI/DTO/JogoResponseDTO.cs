namespace BiteArcade.WebAPI.DTO
{
    public class JogoResponseDTO
    {
        public Guid? IdJogo { get; set; } 
        public string? Imagem { get; set; }
        public string Titulo { get; set; } = null!;
        public Guid? IdGenero { get; set; }

        public string? NomeGenero { get; set; }
    }
}