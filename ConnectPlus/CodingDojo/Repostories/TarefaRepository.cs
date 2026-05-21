using CodingDojo.BdContextCodingDojoContext;
using CodingDojo.Interfaces;
using CodingDojo.Models;

namespace CodingDojo.Repostories;

public class TarefaRepository : ITarefaRepository
{
    private readonly CodingDojoContext _context;


    public void Cadastrar(Tarefa tarefa)
    {
        throw new NotImplementedException();
    }

    public List<Tarefa> ListarTarefas()
    {
        throw new NotImplementedException();
    }

    public List<Tarefa> ListarPorID(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    

}
