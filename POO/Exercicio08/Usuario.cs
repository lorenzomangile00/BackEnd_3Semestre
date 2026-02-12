

namespace Exercicio08
{
    public class Usuario : IAutenticavel
    {
        private string Senha = "4321";

        public bool Autenticar(string senha)
        {
            return senha == Senha;
        }
    }
}