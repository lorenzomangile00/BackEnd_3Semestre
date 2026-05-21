using CodingDojo.Models;

namespace CodingDojo.Interfaces;

public interface ITarefaRepository
{
    void Cadastrar(Tarefa tarefa);

    List<Tarefa> ListarTarefas();

    List<Tarefa> ListarPorID(Guid id);

    void Atualizar(Guid id);

    void Deletar(Guid id);
}
