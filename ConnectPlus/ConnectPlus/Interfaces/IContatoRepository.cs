using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface IContatoRepository
{
    void Cadastrar(Contato contato);
    List<Contato> Listar();
    Contato BuscarPorId(Guid id);
    void Atualizar(Contato contato);
    void Deletar(Guid id);
}
