using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;

    public ComentarioEventoRepository(EventContext eventContext)
    { 
        _context = eventContext;
    }

    /// <summary>
    /// Faz a busca por um comentario especifico atraves do id do usuario
    /// </summary>
    /// <param name="IdUsuario">Nome do Id do usuario buscado para o comentario</param>
    /// <param name="IdEvento">Id do evento buscado ao comentarios</param>
    /// <returns></returns>
    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
    }

    /// <summary>
    /// Faz o cadastro de um comentario do evento
    /// </summary>
    /// <param name="comentarioEvento">Nome do comentario do evento a ser cadastrado</param>
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta o comentario do evento
    /// </summary>
    /// <param name="id">Id do comentario a ser deletado</param>
    public void Deletar(Guid id)
    {
        var comentarioEventoBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioEventoBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista os comentarios do evento
    /// </summary>
    /// <param name="IdEvento">Nome da lista de comentarios do evento</param>
    /// <returns>Lista de comentarios do evento</returns>
    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .OrderBy(comentarioEvento => comentarioEvento.Descricao)
            .ToList();
    }

    /// <summary>
    /// Lista dos comentarios do evento com a funcao de apenas exibi-los
    /// </summary>
    /// <param name="IdEvento">Nome da lista de comentarios do evento exibe</param>
    /// <returns>Lista de comentarios do evento exibi</returns>
    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Where(comentarioEvento => comentarioEvento.Exibe == true)
            .OrderBy(comentarioEvento => comentarioEvento.Descricao)
            .ToList();
    }
}
