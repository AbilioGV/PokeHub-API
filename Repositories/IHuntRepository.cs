using PokeHub.API.Models;

namespace PokeHub.API.Repositories;

public interface IHuntRepository
{
    Task<Hunt?> GetByIdAsync(int id);
    Task<IEnumerable<Hunt>> GetAllAsync();
    Task<IEnumerable<Hunt>> GetByStatusAsync(string status);
    Task<bool> HasActiveHuntAsync(string pokemonName);
    Task AddAsync(Hunt hunt);
    Task UpdateAsync(Hunt hunt);
    Task DeleteAsync(Hunt hunt);
}