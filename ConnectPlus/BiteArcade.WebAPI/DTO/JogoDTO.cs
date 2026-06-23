using Microsoft.AspNetCore.Http;

namespace BiteArcade.WebAPI.DTO
{
    public class JogoDTO
    {
        public IFormFile? Imagem { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public Guid IdGenero { get; set; }
    }
}