using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;

namespace Filmes.WebAPI.Repositories;

public class FilmeRepository : IFilmeRepository
{

    private readonly FilmeContext _context;

    public FilmeRepository(FilmeContext context)
    {
        _context = context;
    }



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
        try
        {
            novoFilme.IdFilme = Guid.NewGuid().ToString();

            _context.Filmes.Add(novoFilme);
            _context.SaveChanges();
        }
        catch (Exception)
        {
        }
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Filme> Listar()
    {
        try
        {
            List<Filme> listaFilmes = _context.Filmes.ToList();

            return listaFilmes;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
