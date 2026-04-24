using Microsoft.EntityFrameworkCore;
using PokeHub.API.Data;
using PokeHub.API.Models;

namespace PokeHub.API.Repositories;

public class HuntRepository : IHuntRepository
{
    private readonly AppDbContext _context;

    public HuntRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Hunt?> GetByIdAsync(int id)
    {
        return await _context.Hunts.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IEnumerable<Hunt>> GetAllAsync()
    {
        return await _context.Hunts.ToListAsync();
    }

    public async Task<IEnumerable<Hunt>> GetByStatusAsync(string status)
    {
        return await _context.Hunts.Where(h => h.Status.ToLower() == status.ToLower()).ToListAsync();
    }

    public async Task<bool> HasActiveHuntAsync(string pokemonName)
    {
        return await _context.Hunts.AnyAsync(h => h.PokemonName.ToLower() == pokemonName.ToLower() && h.Status.ToLower() == "active");
    }

    public async Task AddAsync(Hunt hunt)
    {
        _context.Hunts.Add(hunt);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hunt hunt)
    {
        _context.Hunts.Update(hunt);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Hunt hunt)
    {
        _context.Hunts.Remove(hunt);
        await _context.SaveChangesAsync();
    }
}