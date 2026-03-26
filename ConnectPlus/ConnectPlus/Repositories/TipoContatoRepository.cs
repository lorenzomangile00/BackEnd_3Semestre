using ConnectPlus.BdContextConnectPlus;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly ConnectPlusContext _connectPlusContext;

    public TipoContatoRepository(ConnectPlusContext connectPlusContext  )
    {
        _connectPlusContext = connectPlusContext;
    }

    /// <summary>
    /// Metodo que atualiza um TIpo de contato especifico
    /// </summary>
    /// <param name="tipoContato">Nome do tipo de contato atualizado</param>
    public void Atualizar(TipoContato tipoContato)
    {
        var tipoContatoBuscado = _connectPlusContext.TipoContatos.Find(tipoContato.IdTipoContato);
        if (tipoContatoBuscado != null)
        {
            tipoContatoBuscado.Titulo = tipoContato.Titulo;

            _connectPlusContext.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca um tipo de contato pelo id
    /// </summary>
    /// <param name="id">Id do tipo de contato a ser buscado</param>
    /// <returns>Tipo de contato que foi buscado pelo id</returns>
    public TipoContato BuscarPorId(Guid id)
    {
        return _connectPlusContext.TipoContatos.Find(id);
    }

    /// <summary>
    /// Metodo que cadastra um novo tipo de contato
    /// </summary>
    /// <param name="tipoContato">Nome do tipo de contato a ser cadastrado</param>
    public void Cadastrar(TipoContato tipoContato)
    {
        _connectPlusContext.TipoContatos.Add(tipoContato);
        _connectPlusContext.SaveChanges();
    }

    /// <summary>
    /// Metodo que deleta um tipo de contato especifico
    /// </summary>
    /// <param name="id">Id do tipo de contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoContatoBuscado = _connectPlusContext.TipoContatos.Find(id);
        if (tipoContatoBuscado != null)
        {
            _connectPlusContext.TipoContatos.Remove(tipoContatoBuscado);
            _connectPlusContext.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que lista os tipos de contatos
    /// </summary>
    /// <returns>Lista dos tipos de contato</returns>
    public List<TipoContato> Listar()
    {
        return _connectPlusContext.TipoContatos
            .OrderBy(t => t.Titulo)
            .ToList();
    }
}
