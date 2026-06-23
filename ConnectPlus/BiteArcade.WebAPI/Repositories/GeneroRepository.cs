using BiteArcade.WebAPI.BdContextJogosContext;
using BiteArcade.WebAPI.DTO;
using BiteArcade.WebAPI.Interfaces;
using BiteArcade.WebAPI.Models;

namespace BiteArcade.WebAPI.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly JogosContext _context;

        public GeneroRepository(JogosContext context)
        {
            _context = context;
        }

        public void Cadastrar(Genero novoGenero)
        {
            try
            {
                novoGenero.IdGenero = Guid.NewGuid();

                _context.Generos.Add(novoGenero);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Genero> Listar()
        {
            return _context.Generos.ToList();
        }

        public Genero? BuscarPorId(Guid id)
        {
            return _context.Generos.Find(id)!;
        }

        public void AtualizarIdCorpo(Genero generoAtualizado)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(generoAtualizado.IdGenero)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = generoAtualizado.Nome;

                    _context.Generos.Update(generoBuscado);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarIdUrl(Guid id, GeneroDTO generoAtualizado)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = generoAtualizado.Nome;

                    _context.Generos.Update(generoBuscado);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarIdUrl(Guid id, Genero generoAtualizado)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = generoAtualizado.Nome;

                    _context.Generos.Update(generoBuscado);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            var generoBuscado = _context.Generos.Find(id);

            if (generoBuscado != null)
            {
                _context.Generos.Remove(generoBuscado);
                _context.SaveChanges();
            }
        }
    }
}