using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza a instituicao com um rastreamento automatico
    /// </summary>
    /// <param name="id">id da instituicao a ser atualizada</param>
    /// <param name="instituicao">Novos dados da instituicao</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = String.IsNullOrWhiteSpace(instituicao.NomeFantasia) ? instituicaoBuscada.NomeFantasia : instituicao.NomeFantasia; 

            instituicaoBuscada.Cnpj = String.IsNullOrWhiteSpace(instituicao.Cnpj) ? instituicaoBuscada.Cnpj : instituicao.Cnpj;

            instituicaoBuscada.Endereco = String.IsNullOrWhiteSpace(instituicao.Endereco) ? instituicaoBuscada.Endereco : instituicao.Endereco; 

            //O SaveChanges() detecta mudanca na propriedade titulo automaticamente
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma instituicao pelo id
    /// </summary>
    /// <param name="id">id das instituicoes a ser buscado</param>
    /// <returns>Retorna o objeto da instituicao com as informacoes</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra uma instituicao
    /// </summary>
    /// <param name="instituicao">Instituicao a ser cadastrada</param>
    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }



    /// <summary>
    /// Deleta uma instituicao
    /// </summary>
    /// <param name="id">id da instituicao a ser deletada</param>
    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);
        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges(); 
        }
    }

    /// <summary>
    /// Lista as instituicoes cadastradas
    /// </summary>
    /// <returns>Lista as instituicoes</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos
            .OrderBy(Instituicao => Instituicao).ToList();
    }
}
