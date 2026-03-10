namespace EventPlus.WebAPI.Utils;

public static class Criptografia
{
    public static string GerarHash(string senha) 
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public static bool CompararHash(string senhaFormada, string senhaBanco) 
    {
        return BCrypt.Net.BCrypt.Verify(senhaFormada, senhaBanco);
    }
}
