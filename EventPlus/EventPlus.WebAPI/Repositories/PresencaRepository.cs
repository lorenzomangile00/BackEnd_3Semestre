using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _context;

    public PresencaRepository(EventContext eventContext)
    {
        _context = eventContext;
    }

    /// <summary>
    /// Metodo que atualiza a presenca
    /// </summary>
    /// <param name="id">Id da presenca a ser atualizada</param>
    /// <param name="presenca">Nome da presenca a ser atualizada</param>
    public void Atualizar(Guid id, Presenca presenca)
    {
        var presencaBuscada = _context.Presencas.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presenca.Situacao;
            

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma presenca por id
    /// </summary>
    /// <param name="id">Id da presenca a ser buscada</param>
    /// <returns>Retorna a presenca buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    /// <summary>
    /// Metodo que deleta uma presenca
    /// </summary>
    /// <param name="id">Id da presenca a ser deletada</param>
    public void Deletar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _context.Presencas.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Inscreve uma nova presenca
    /// </summary>
    /// <param name="Inscricao">Nome da presenca inscrita</param>
    public void Inscrever(Presenca Inscricao)
    {
        _context.Presencas.Add(Inscricao);
        _context.SaveChanges();
    }

    /// <summary>
    /// Lista as presencas no evento
    /// </summary>
    /// <returns>Uma lista das presencas</returns>
    public List<Presenca> Listar()
    {
        return _context.Presencas
            .OrderBy(e => e.IdPresenca)
            .ToList();
    }

    /// <summary>
    /// Lista as presencas de um usuario especifico 
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Uma lista de presencas de um usuario especifico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
