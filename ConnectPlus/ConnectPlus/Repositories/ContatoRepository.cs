using ConnectPlus.BdContextConnectPlus;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly ConnectPlusContext _connectPlusContext;

    public ContatoRepository(ConnectPlusContext connectPlusContext)
    {
        _connectPlusContext = connectPlusContext;
    }

    /// <summary>
    /// Metodo que atualiza um contato especifico
    /// </summary>
    /// <param name="contato">Nome do contato que foi atualizado</param>

    public void Atualizar(Guid id, Contato contato)
    {
        var contatoBuscado = _connectPlusContext.Contatos.Find(id);
        if (contatoBuscado != null)
        {
            contatoBuscado.Nome = contato.Nome;
            contatoBuscado.Imagem = contato.Imagem;
            contatoBuscado.FormaContato = contato.FormaContato;
            contatoBuscado.IdTipoContato = contato.IdTipoContato;

            _connectPlusContext.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca o id de um contato especifico
    /// </summary>
    /// <param name="id">Id do contat especifico buscado</param>
    /// <returns>O contato foi buscado pelo id</returns>
    /// <returns>O contato foi buscado pelo id</returns>
    public Contato BuscarPorId(Guid id)
    {
        return _connectPlusContext.Contatos.Find(id);
    }

    /// <summary>
    /// Metodo que cadastra um novo contato
    /// </summary>
    /// <param name="contato">Nome do novo contato a ser cadastrado</param>
    public void Cadastrar(Contato contato)
    {
        _connectPlusContext.Contatos.Add(contato);
        _connectPlusContext.SaveChanges();
    }

    /// <summary>
    /// Metodo que deleta um contato pelo id
    /// </summary>
    /// <param name="id">Id do contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var contatoBuscado = _connectPlusContext.Contatos.Find(id);
        if (contatoBuscado != null)
        {
            _connectPlusContext.Contatos.Remove(contatoBuscado);
            _connectPlusContext.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo que busca listar os contatos
    /// </summary>
    /// <returns>Lista de todos os contatos</returns>
    public List<Contato> Listar()
    {
        return _connectPlusContext.Contatos
        .OrderBy(c => c.Nome)
        .ToList();
    }
}
