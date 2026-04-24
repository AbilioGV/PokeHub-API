using PokeHub.API.Models;
using PokeHub.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace PokeHub.API.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Register(string name, string email, string password, bool isAdmin)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new ArgumentException("Usuario já existe");
        }

        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            IsAdmin = isAdmin
        };

        // Utilizando o PasswordHasher padrão do ASP.NET Core
        var hasher = new PasswordHasher<User>();
        newUser.Password = hasher.HashPassword(newUser, password);

        return await _userRepository.addAsync(newUser);
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            throw new ArgumentException("Email ou senha inválidos.");
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.Password, password);

        if (result == PasswordVerificationResult.Failed)
            throw new ArgumentException("Email ou senha inválidos.");
        return user;
    }
}