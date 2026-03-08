using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;

namespace AuthService.Application.Services;

public class AuthenticationService(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
{
    /// <summary>
    /// Registra un nuevo usuario encriptando su contraseña y almacenándolo en la base de datos.
    /// </summary>
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null) throw new Exception("El usuario ya existe.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHasher.Hash(request.Password),
            Role = request.Role
        };

        await userRepository.AddAsync(user);
        var token = jwtProvider.Generate(user);

        return new AuthResponseDto(token, user.FullName, user.Role);
    }

    /// <summary>
    /// Autentica a un usuario verificando sus credenciales para generar su respectivo token de acceso.
    /// </summary>
    public async Task<AuthResponseDto> LoginAsync(LoginDto request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        
        if (user == null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new Exception("Credenciales inválidas.");

        if (!user.IsActive) 
            throw new Exception("Usuario inactivo.");

        var token = jwtProvider.Generate(user);
        return new AuthResponseDto(token, user.FullName, user.Role);
    }
}