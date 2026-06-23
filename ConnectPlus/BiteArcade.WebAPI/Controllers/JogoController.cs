using BiteArcade.WebAPI.DTO;
using BiteArcade.WebAPI.Interfaces;
using BiteArcade.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiteArcade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoController(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_jogoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var jogo = _jogoRepository.BuscarPorId(id);

                if (jogo == null)
                    return NotFound();

                var dto = new JogoResponseDTO
                {
                    IdJogo = jogo.IdJogo,
                    Imagem = jogo.Imagem,
                    Titulo = jogo.Titulo,
                    IdGenero = jogo.IdGenero,
                    NomeGenero = jogo.IdGeneroNavigation?.Nome
                };

                return Ok(dto);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] JogoDTO jogo)
        {
            if (string.IsNullOrWhiteSpace(jogo.Titulo))
                return BadRequest("Título é obrigatório.");

            Jogo novoJogo = new Jogo();

            if (jogo.Imagem != null)
            {
                var extensao = Path.GetExtension(jogo.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                    Directory.CreateDirectory(caminhoPasta);

                var caminhoCompleto = Path.Combine(
                    caminhoPasta,
                    nomeArquivo);

                using (var stream = new FileStream(
                    caminhoCompleto,
                    FileMode.Create))
                {
                    await jogo.Imagem.CopyToAsync(stream);
                }

                novoJogo.Imagem = nomeArquivo;
            }

            novoJogo.Titulo = jogo.Titulo;
            novoJogo.IdGenero = jogo.IdGenero;

            try
            {
                _jogoRepository.Cadastrar(novoJogo);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] JogoDTO jogoAtualizado)
        {
            var jogoBuscado = _jogoRepository.BuscarPorId(id);

            if (jogoBuscado == null)
                return NotFound("Jogo não encontrado.");

            if (!string.IsNullOrWhiteSpace(jogoAtualizado.Titulo))
                jogoBuscado.Titulo = jogoAtualizado.Titulo;

            jogoBuscado.IdGenero = jogoAtualizado.IdGenero;

            if (jogoAtualizado.Imagem != null)
            {
                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    pastaRelativa);

                if (!string.IsNullOrEmpty(jogoBuscado.Imagem))
                {
                    var caminhoAntigo = Path.Combine(
                        caminhoPasta,
                        jogoBuscado.Imagem);

                    if (System.IO.File.Exists(caminhoAntigo))
                        System.IO.File.Delete(caminhoAntigo);
                }

                var extensao = Path.GetExtension(
                    jogoAtualizado.Imagem.FileName);

                var nomeArquivo =
                    $"{Guid.NewGuid()}{extensao}";

                var caminhoCompleto =
                    Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(
                    caminhoCompleto,
                    FileMode.Create))
                {
                    await jogoAtualizado.Imagem.CopyToAsync(stream);
                }

                jogoBuscado.Imagem = nomeArquivo;
            }

            try
            {
                _jogoRepository.AtualizarIdUrl(id, jogoBuscado);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _jogoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}