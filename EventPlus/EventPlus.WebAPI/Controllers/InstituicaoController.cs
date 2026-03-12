using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de listar as instituicoes
    /// </summary>
    /// <returns>Status code 200 e a lista de  instituicoes</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para um metodo de buscar uma instituicao especifica
    /// </summary>
    /// <param name="id">id da instituicao buscada</param>
    /// <returns>status code 200 e a instituicao buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de cadastrar uma instituicao
    /// </summary>
    /// <param name="instituicao">Instituicao a ser cadastrada</param>
    /// <returns>Status code 201 e a instituicao a ser cadastrada</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,

                Cnpj = instituicao.Cnpj!,

                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Cadastrar(novaInstituicao);

            return StatusCode(201, novaInstituicao);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de atualizar uma instituicao
    /// </summary>
    /// <param name="id">Id da instituicao com dados atualizados</param>
    /// <param name="instituicao">Instituicao a ser atualizada</param>
    /// <returns>Status code 204 e instituicao atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,

                Cnpj = instituicao.Cnpj!,

                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);

            return StatusCode(204, instituicaoAtualizada);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de deletar uma instituicao
    /// </summary>
    /// <param name="id">Id da instituicao a ser excluida</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
