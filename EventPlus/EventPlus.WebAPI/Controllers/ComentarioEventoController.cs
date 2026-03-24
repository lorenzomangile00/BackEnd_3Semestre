using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;

    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// Endpoint da API que cadastra e modera um comentario
    /// </summary>
    /// <param name="comentarioEvento">comentario a ser moderado</param>
    /// <returns>Status code 201 e o comentario criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado nao pode estar vazio.");
            }

            // criar um objeto de analise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            //chamar a API do Azure Content Safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            //verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.idUsuario,
                IdEvento = comentarioEvento.idEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comentario vai ser exibido
                Exibe = !temConteudoImproprio
            };

            //cadastrar o comentario
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que lista os comentarios pelo Id do evento 
    /// </summary>
    /// <param name="IdEvento">Listar comentarios por Id do evento</param>
    /// <returns>Status code 200 e uma lista de comentarios por id do evento</returns>
    [HttpGet("{IdEvento}")]
    public IActionResult Listar(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(IdEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que busca o id por usuario
    /// </summary>
    /// <param name="IdUsuario">Busca o id do usuario para o comentario</param>
    /// <param name="IdEvento">Busca o comentario pelo id do evento</param>
    /// <returns>Status code 200 e o id do Usuario do comentario buscado</returns>
    [HttpGet("{IdUsuario}/{IdEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        try
        {
            var comentario = _comentarioEventoRepository.BuscarPorIdUsuario(IdUsuario, IdEvento);
            if (comentario == null)
            {
                return NotFound("Comentario nao encontrado");
            }

            return Ok(comentario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que o listar de comentarios apenas exibe
    /// </summary>
    /// <param name="IdEvento">Metodo listar e exibido</param>
    /// <returns>STatus code 200 e o metodo listar de comentarios é apenas exibido</returns>
    [HttpGet("listarSomenteExibe/{IdEvento}")]
    public IActionResult ListarSomenteExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que deleta o comentario pelo id
    /// </summary>
    /// <param name="id">Comentario pelo id do comentario</param>
    /// <returns>Status code 204 e o comentario deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
