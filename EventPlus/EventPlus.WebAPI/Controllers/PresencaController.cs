using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    { 
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna uma presenca por id
    /// </summary>
    /// <param name="id">Id da presenca a ser buscada</param>
    /// <returns>Status code 200 e presenca buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna uma lista de presencas filtradas por usuario
    /// </summary>
    /// <param name="idUsuario">Id do usuario para filtragem</param>
    /// <returns>Lista de presencas filtradas por usuario</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que lista as presencas
    /// </summary>
    /// <returns>Lista das presencas</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que inscreve as presencas
    /// </summary>
    /// <param name="presencaDTO">Nome das presencas inscritas</param>
    /// <returns>Status code 201 e a presenca inscrita</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presencaDTO)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presencaDTO.Situacao!,
                IdUsuario = presencaDTO.IdUsuario!,
                IdEvento = presencaDTO.IdEvento!
            };

            _presencaRepository.Inscrever(novaPresenca);

            return StatusCode(201, novaPresenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que atualiza a presenca de um id especifico
    /// </summary>
    /// <param name="id">Id da presenca que deve ser atualizada</param>
    /// <param name="presencaDTO">Nome da presenca atualizada</param>
    /// <returns>Status code 204 e a presenca atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presencaDTO)
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                Situacao = presencaDTO.Situacao!,
                IdUsuario = presencaDTO.IdUsuario!,
                IdEvento = presencaDTO.IdEvento!
            };

            _presencaRepository.Atualizar(id, presencaAtualizada);

            return StatusCode(204, presencaAtualizada);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que deleta a presenca de um id especifico
    /// </summary>
    /// <param name="id">Id da presenca a ser deletada</param>
    /// <returns>Presenca a ser deletada</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
