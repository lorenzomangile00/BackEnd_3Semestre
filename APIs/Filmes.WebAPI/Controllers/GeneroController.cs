using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository generoRepository;
        private IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        { 
            _generoRepository = generoRepository;
        }

        [HttpPost]

        public IActionResult Post(Genero novoGenero) 
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);
                return StatusCode(201);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
