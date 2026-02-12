using Exercicio08;

IAutenticavel usuario = new Usuario();
IAutenticavel admin = new Administrador();

Console.WriteLine($"usuario.Autenticar{1234}");  
Console.WriteLine($"admin.Autenticar{admin}");   
Console.WriteLine($"admin.Autenticar{1234}");