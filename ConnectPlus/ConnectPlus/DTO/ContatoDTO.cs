namespace ConnectPlus.DTO;

public class ContatoDTO
{
    public string Nome { get; set; }
    public IFormFile? Imagem { get; set; }
    public string FormaContato { get; set; }
    public Guid IdTipoContato { get; set; }
}
