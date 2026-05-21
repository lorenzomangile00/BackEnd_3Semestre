using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Models;

namespace Filmes.WebAPI.Interfaces;

public interface IGeneroRepository
{
    void Cadastrar(Genero novoGenero);
    void AtualizarIdCorpo(Genero generoAtualizado);
    void AtualizarIdUrl(Guid id, Genero generoAtualizado);
    List<Genero> Listar();
    void Deletar(Guid id);
    Genero BuscarPorId(Guid id);

}
