using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    { 
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de listar eventos filtrado pelo Id do usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada do metodo de listar os proximos eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que lista os eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que cadastra um novo evento
    /// </summary>
    /// <param name="evento">Nome do evento a ser cadastrado</param>
    /// <returns>Status code 201 e um novo evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(Evento evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome!,
                DataEvento = evento.DataEvento!,
                Descricao = evento.Descricao!,
                IdTipoEvento = evento.IdTipoEvento,
                IdInstituicao = evento.IdInstituicao!
            };

            _eventoRepository.Cadastrar(novoEvento);

            return StatusCode(201, novoEvento);

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    
}
