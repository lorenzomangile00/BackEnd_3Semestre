

namespace Exercicio05
{
    public class Pessoa
    {
         public string Nome="";

        public int Idade;

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public void ExibirDados()
        {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
        }

          public void DefinirIdade(int valor)
        {
            if (valor > 0)
            {
                Idade = valor;
            }
            else
            {
                System.Console.WriteLine("A idade é inválida");
            }
            }
    }
}