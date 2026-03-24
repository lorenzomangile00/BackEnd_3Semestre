namespace EventPlus.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public string Descricao { get; set; }
    public Guid idEvento { get; set; }
    public Guid idUsuario { get; set; }
}
