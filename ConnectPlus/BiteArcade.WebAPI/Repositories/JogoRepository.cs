using BiteArcade.WebAPI.BdContextJogosContext;
using BiteArcade.WebAPI.Interfaces;
using BiteArcade.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteArcade.WebAPI.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly JogosContext _context;

        public JogoRepository(JogosContext context)
        {
            _context = context;
        }

        public void Cadastrar(Jogo novoJogo)
        {
            novoJogo.IdJogo = Guid.NewGuid();

            _context.Jogos.Add(novoJogo);
            _context.SaveChanges();
        }

        public List<Jogo> Listar()
        {
            return _context.Jogos
                .Include(j => j.IdGeneroNavigation)
                .ToList();
        }

        public Jogo BuscarPorId(Guid id)
        {
            return _context.Jogos
                .Include(j => j.IdGeneroNavigation)
                .FirstOrDefault(j => j.IdJogo == id)!;
        }

        public void AtualizarIdCorpo(Jogo jogoAtualizado)
        {
            var jogoBuscado =
                _context.Jogos.Find(jogoAtualizado.IdJogo);

            if (jogoBuscado != null)
            {
                jogoBuscado.Titulo = jogoAtualizado.Titulo;
                jogoBuscado.Imagem = jogoAtualizado.Imagem;
                jogoBuscado.IdGenero = jogoAtualizado.IdGenero;

                _context.Jogos.Update(jogoBuscado);
                _context.SaveChanges();
            }
        }

        public void AtualizarIdUrl(Guid id, Jogo jogoAtualizado)
        {
            var jogoBuscado =
                _context.Jogos.Find(id);

            if (jogoBuscado != null)
            {
                jogoBuscado.Titulo = jogoAtualizado.Titulo;
                jogoBuscado.Imagem = jogoAtualizado.Imagem;
                jogoBuscado.IdGenero = jogoAtualizado.IdGenero;

                _context.Jogos.Update(jogoBuscado);
                _context.SaveChanges();
            }
        }

        public void Deletar(Guid id)
        {
            var jogoBuscado =
                _context.Jogos.Find(id);

            if (jogoBuscado != null)
            {
                _context.Jogos.Remove(jogoBuscado);
                _context.SaveChanges();
            }
        }
    }
}