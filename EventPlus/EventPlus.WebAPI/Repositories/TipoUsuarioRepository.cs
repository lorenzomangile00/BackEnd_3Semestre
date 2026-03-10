using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza o tipo de Usuario com um rastreamento automatico
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipoUsuario">Novos dados do tipo de usuario</param>
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
            //O SaveChanges() detecta mudanca na propriedade titulo automaticamente
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de usuario por id
    /// </summary>
    /// <param name="id">id do tipo de usuario a ser buscado</param>
    /// <returns>Objeto do tipo de usuario com as informacoes</returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de usuario
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuario a ser cadastrado</param>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um tipo de usuario
    /// </summary>
    /// <param name="id">Id do tipo de usuario a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de usuarios cadastrados
    /// </summary>
    /// <returns>Uma lista de tipos de usuarios</returns>
    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios
            .OrderBy(tipoUsuario => tipoUsuario.Titulo)
            .ToList();
    }
}
