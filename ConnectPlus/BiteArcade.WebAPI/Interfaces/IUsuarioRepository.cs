using BiteArcade.WebAPI.Models;
using BiteArcade.WebAPI.Models;

namespace BiteArcade.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario novoUsuario);

    Usuario BuscarPorId(Guid id);

    Usuario BuscarPorEmailESenha(string email, string senha);
}
