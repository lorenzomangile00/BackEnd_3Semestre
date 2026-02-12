

namespace Exercicio08
{
    public class Administrador : IAutenticavel
    {
         private string Senha = "admin";

        public bool Autenticar(string senha)
        {
            return senha == Senha;
        }
    }
}