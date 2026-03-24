using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
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
    public IActionResult Cadastrar(EventoDTO eventoDTO)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = eventoDTO.Nome!,
                DataEvento = (DateTime)eventoDTO.DataEvento!,
                Descricao = eventoDTO.Descricao!,
                IdTipoEvento = eventoDTO.IdTipoEvento,
                IdInstituicao = eventoDTO.IdInstituicao!
            };

            _eventoRepository.Cadastrar(novoEvento);

            return StatusCode(201, novoEvento);

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que atualiza um evento
    /// </summary>
    /// <param name="eventoDTO">Nome do evento a ser atualizado</param>
    /// <returns>Status code 204 e o evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, EventoDTO eventoDTO)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = eventoDTO.Nome!,
                DataEvento = (DateTime)eventoDTO.DataEvento!,
                Descricao = eventoDTO.Descricao!,
                IdTipoEvento = eventoDTO.IdTipoEvento,
                IdInstituicao = eventoDTO.IdInstituicao!
            };

            _eventoRepository.Atualizar(id, eventoAtualizado);

            return StatusCode(204, eventoAtualizado);

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que realiza o metodo de deletar
    /// </summary>
    /// <param name="id">Id do evento a ser deletado</param>
    /// <returns>Status code 204 e o evento deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    
}
