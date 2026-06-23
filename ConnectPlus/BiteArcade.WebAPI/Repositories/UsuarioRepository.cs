using BiteArcade.WebAPI.BdContextJogosContext;
using BiteArcade.WebAPI.Interfaces;
using BiteArcade.WebAPI.Models;


namespace BiteArcade.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly JogosContext _context;

        public UsuarioRepository(JogosContext context)
        {
            _context = context;
        }
        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.IdUsuario = Guid.NewGuid().ToString();

                _context.Usuarios.Add(novoUsuario);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Usuario> Listar()
        {
            try
            {
                List<Usuario> listaUsuarios = _context.Usuarios.ToList();
                return listaUsuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorId(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.Find(id.ToString())!;
                return usuarioBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.Find(id.ToString())!;
                if (usuarioBuscado != null)
                {
                    _context.Usuarios.Remove(usuarioBuscado);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios
                    .FirstOrDefault(u => u.Email == email && u.Senha == senha)!;

                return usuarioBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}