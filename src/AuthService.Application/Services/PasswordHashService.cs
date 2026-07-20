using AuthService.Application.Interfaces;

namespace AuthService.Application.Services;

public class PasswordHashService : IPasswordHashService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            if (hashedPassword.StartsWith("$2a$") || hashedPassword.StartsWith("$2b$") || hashedPassword.StartsWith("$2y$"))
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
