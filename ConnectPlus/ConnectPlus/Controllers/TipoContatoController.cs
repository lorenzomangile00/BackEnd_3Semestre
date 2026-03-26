using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private readonly ITipoContatoRepository _tipoContatoRepository;

    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }

    /// <summary>
    /// Endpoint da API que cadastra um novo tipo de contato
    /// </summary>
    /// <param name="tipoContatoDTO">Nome do novo tipo de contato a ser cadastradi</param>
    /// <returns>Status coe 201 e um novo tipo de contato cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContatoDTO.Titulo
            };

            _tipoContatoRepository.Cadastrar(novoTipoContato);

            return StatusCode(201, tipoContatoDTO);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que lista todos os tipos de contato
    /// </summary>
    /// <returns>Status code 200 e a lista dos tipos de contato</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoContatoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que busca um tipo de contato pelo seu id
    /// </summary>
    /// <param name="id">Nome do tipo de contato buscado pelo id</param>
    /// <returns>Status code 200 e o tipo de contato buscado pelo id</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoContatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que atualiza um tipo de contato atraves do id
    /// </summary>
    /// <param name="tipoContatoDTO">Nome do tipo de contato a ser atualizado</param>
    /// <returns>Status code 204 e o tipo de contato atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            var tipoContatoAtualizado = new TipoContato
            {
                IdTipoContato = id,
                Titulo = tipoContatoDTO.Titulo
            };

            _tipoContatoRepository.Atualizar(tipoContatoAtualizado);

            return StatusCode(204, tipoContatoAtualizado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que deleta um tipo de contato atraves de seu id
    /// </summary>
    /// <param name="id">Id do tipo de contato a ser deletado</param>
    /// <returns>Status code 204 e o tipo de contato deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoContatoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
