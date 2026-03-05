using System.Text;

namespace Filmes.WebAPI.DTO;

public class FilmeDTO
{
    public string? Nome { get; set; }
    public IFormFile? Imagem { get; set; }
    public Guid? IdGenero { get; set; }
}
