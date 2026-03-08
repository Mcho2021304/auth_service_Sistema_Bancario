using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    /// <summary>
    /// Consulta y retorna un usuario específico filtrando por su correo electrónico.
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Inserta un nuevo registro de usuario en la base de datos.
    /// </summary>
    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}