using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //metodo construtor que aplica a injecao de dependencia
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca o usuario pelo email e valida o hash da senha
    /// </summary>
    /// <param name="Email">Email do usuario a ser buscado</param>
    /// <param name="Senha">Senha para validar o usuario</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //primeiro, buscando o usuario pelo e-mail 
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.Email == Email);

        //verificamos se o usuario for encontrado
        if (usuarioBuscado != null)
        {
            //comparamos o hash da senha digitada com o que esta no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
             
        }
            return null!;
    }

    /// <summary>
    /// busca um usuario pelo id, incluindo os dados do seu tipo de usuario
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Usuario buscado e seu tipo de usuario</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Cadastra um novo usuario. A senha e criptografada e o id e gerado pelo banco
    /// </summary>
    /// <param name="usuario">usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}
