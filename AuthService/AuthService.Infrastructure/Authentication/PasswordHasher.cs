using AuthService.Application.Interfaces;

namespace AuthService.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Genera un hash criptográfico unidireccional para la contraseña ingresada.
    /// </summary>
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    /// <summary>
    /// Comprueba si la contraseña en texto plano coincide con el hash almacenado.
    /// </summary>
    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}