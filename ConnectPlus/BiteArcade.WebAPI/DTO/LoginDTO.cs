using System.ComponentModel.DataAnnotations;

namespace BiteArcade.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O email do usuario e obrigatorio!")]

    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha do usuario e obrigatoria!")]

    public string? Senha { get; set; }
}
