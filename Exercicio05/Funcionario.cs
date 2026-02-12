

namespace Exercicio05
{
    public class Funcionario : Pessoa
    {
        public double Salario;

         public Funcionario(string nome, int idade, double salario) : base(nome, idade)
    
        {
            Salario = salario;
        }

        public void ExibirDados()
        {
            base.ExibirDados();
            Console.WriteLine($"Salário: {Salario}");
        }
    }
}