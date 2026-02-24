using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;

namespace Filmes.WebAPI.Repositories;

public class FilmeRepository : IFilmeRepository
{
    public void AtualizarIdCorpo(Filme filmeAtualizado)
    {
        throw new NotImplementedException();
    }

    public void AtualizarIdUrl(Guid id, Filme filmeAtualizado)
    {
        throw new NotImplementedException();
    }

    public Filme BuscarPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Cadastrar(Filme novoFilme)
    {
        throw new NotImplementedException();
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Filme> Listar()
    {
        throw new NotImplementedException();
    }
}
