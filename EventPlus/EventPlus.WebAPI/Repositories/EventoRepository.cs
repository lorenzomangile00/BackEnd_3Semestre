using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metodo que atualiza um evento especifico
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="evento">Nome do evento que foi atualizado</param>
    public void Atualizar(Guid id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(evento);

        if(eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.DataEvento = evento.DataEvento;
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
            eventoBuscado.IdInstituicao = evento.IdInstituicao;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca pelo id um evento especifico
    /// </summary>
    /// <param name="id">Id do evento a ser buscado</param>
    /// <returns>O evento que foi buscado pelo id</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id);
    }

    /// <summary>
    /// Novo evento a ser cadastrado
    /// </summary>
    /// <param name="evento">Nome do evento que sera cadastrado</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Metodo que deleta um evento
    /// </summary>
    /// <param name="IdEvento">Id do evento a ser deletado</param>
    public void Deletar(Guid IdEvento)
    {
        var eventoBuscado = _context.Eventos.Find(IdEvento);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que lista os eventos
    /// </summary>
    /// <returns>Metodo que retorna os eventos listados</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
        .OrderBy(e => e.Nome)
        .ToList();
    }

    /// <summary>
    /// Metodo que busca eventos no qual um usuario confirmou presenca
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser buscado</param>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }

    /// <summary>
    /// Metodo que traz a lista de proximos eventos
    /// </summary>
    /// <returns>Retorna uma lista de eventos</returns>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
