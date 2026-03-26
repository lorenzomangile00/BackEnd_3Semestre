using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;
    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    /// <summary>
    /// Endpoint da API que cadastra um novo contato
    /// </summary>
    /// <param name="contatoDTO">Nome do novo contato cadastrado</param>
    /// <returns>Status code 201 e um novo contato cadastrado</returns>
    /// <summary>
    /// Endpoint da API que cadastra um novo contato
    /// </summary>
    /// <param name="contato">Dados do novo contato cadastrado</param>
    /// <returns>Status code 200 e os dados do contato cadastrado</returns> 
    [HttpPost]
    public IActionResult Cadastrar([FromForm] ContatoDTO contato)
    {
        if (String.IsNullOrEmpty(contato.Nome) || String.IsNullOrEmpty(contato.FormaContato))
        {
            return BadRequest("O nome e a forma de contato são obrigatórios");
        }
        Contato novoContato = new Contato();
        if (contato.Imagem != null && contato.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(contato.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                contato.Imagem.CopyToAsync(stream);
            }

            novoContato.Imagem = nomeArquivo;
        }
        novoContato.Nome = contato.Nome;
        novoContato.FormaContato = contato.FormaContato;
        novoContato.IdTipoContato = contato.IdTipoContato;
        try
        {
            _contatoRepository.Cadastrar(novoContato);
            return StatusCode(201, novoContato);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que lista todos os contatos
    /// </summary>
    /// <returns>Status code 200 e lista dos contatos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_contatoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que busca um contato pelo id
    /// </summary>
    /// <param name="id">Id do contato que foi buscado</param>
    /// <returns>Status code 200 e o contato que foi buscado pelo id</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_contatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API para atualizar os dados de um contato com base no seu id
    /// </summary>
    /// <param name="id">Id do contato a ser atualizado</param> 
    /// <param name="contato">Dados do contato atualizado</param>
    /// <returns>Status Code 200 e os dados do contato atualizado</returns> 
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromForm] ContatoDTO contato)
    {
        var contatoBuscado = _contatoRepository.BuscarPorId(id);
        if (contatoBuscado == null)
        
            return NotFound("Contato não encontrado");
        contatoBuscado.Nome = contato.Nome;
        contatoBuscado.FormaContato = contato.FormaContato;
        contatoBuscado.IdTipoContato = contato.IdTipoContato;

        if (contato.Imagem != null && contato.Imagem.Length > 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);
            if (!String.IsNullOrEmpty(contatoBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, contatoBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                {
                    System.IO.File.Delete(caminhoAntigo);
                }
            }

            var extensao = Path.GetExtension(contato.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await contato.Imagem.CopyToAsync(stream);
            }
            contatoBuscado.Imagem = nomeArquivo;
        }
        try
        {
            _contatoRepository.Atualizar(id, contatoBuscado);
            return Ok(contatoBuscado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que deleta o contato pelo id
    /// </summary>
    /// <param name="id">Id do contato a ser deletado</param>
    /// <returns>Status code 204 e contato a ser deletado</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _contatoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
