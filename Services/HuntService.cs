using PokeHub.API.Data;
using PokeHub.API.DTOs;
using PokeHub.API.Models;
using PokeHub.API.Repositories;

namespace PokeHub.API.Services;

public class HuntService
{
    private readonly IHuntRepository _huntRepository;
    private readonly PokemonService _pokemonService;

    public HuntService(IHuntRepository huntRepository, PokemonService pokemonService)
    {
        _huntRepository = huntRepository;
        _pokemonService = pokemonService;
    }

    public async Task<Hunt> CreateHuntAsync(Hunt hunt)
    {
        // 1. Valida se o Pokémon existe de fato na PokeAPI
        var exists = await _pokemonService.GetPokemonData(hunt.PokemonName);
        if (exists == null)
        {
            throw new ArgumentException("Pokémon não encontrado.");
        }

        // 2. Valida se a Hunt já existe no banco de dados
        var huntExists = await _huntRepository.HasActiveHuntAsync(hunt.PokemonName);
        if (huntExists)
        {
            hunt.Status = "active";
            throw new InvalidOperationException("Já existe uma caçada em andamento para este Pokémon.");
        }


        hunt.Attempts = 0;
        hunt.Status = "active";
        hunt.StartDate = DateTime.UtcNow;
        hunt.LastActiveDate = DateTime.UtcNow;
        hunt.AccumulatedTime = TimeSpan.Zero;

        await _huntRepository.AddAsync(hunt);

        return hunt;
    }

    public async Task<IEnumerable<HuntResponse>> GetAllHuntsAsync()
    {
        var hunts = await _huntRepository.GetAllAsync();

        var huntsWithSpritesTasks = hunts.Select(async hunt =>
        {
            var pokemonData = await _pokemonService.GetPokemonData(hunt.PokemonName);
            return new HuntResponse
            {
                Id = hunt.Id,
                PokemonName = hunt.PokemonName,
                Method = hunt.Method,
                Attempts = hunt.Attempts,
                Status = hunt.Status,
                Sprite = pokemonData?.Sprites?.Other?.Showdown?.FrontShiny ?? pokemonData?.Sprites?.FrontShiny,
                StartDate = hunt.StartDate,
                EndDate = hunt.EndDate,
                TotalTime = hunt.Status.ToLower() == "active" && hunt.LastActiveDate.HasValue 
                    ? hunt.AccumulatedTime + (DateTime.UtcNow - hunt.LastActiveDate.Value)
                    : hunt.AccumulatedTime
            };
        });

        return await Task.WhenAll(huntsWithSpritesTasks);
    }

    public async Task<Hunt?> IncrementAttempts(int Id)
    {
        var hunt = await _huntRepository.GetByIdAsync(Id);
        if (hunt == null)
            return null;

        if (hunt.Status.ToLower() != "active")
        {
            throw new InvalidOperationException("Não há caçada para incrementar tentativas.");
        }

        hunt.Attempts++;

        await _huntRepository.UpdateAsync(hunt);

        return hunt;
    }

    public async Task<Hunt?> StatusHunt(int Id, string status)
    {
        var hunt = await _huntRepository.GetByIdAsync(Id);
        if (hunt == null)
            return null;

        var newStatus = status.ToLower();
        if (newStatus != "active" && newStatus != "completed" && newStatus != "paused")
        {
            throw new ArgumentException("Status inválido.");
        }

        if (hunt.Status.ToLower() == newStatus)
        {
            return hunt;
        }

        if (hunt.Status.ToLower() == "active" && hunt.LastActiveDate.HasValue)
        {
            hunt.AccumulatedTime += DateTime.UtcNow - hunt.LastActiveDate.Value;
        }

        if (newStatus == "active")
        {
            hunt.LastActiveDate = DateTime.UtcNow;
            hunt.EndDate = null;
        }
        else if (newStatus == "completed")
        {
            hunt.EndDate = DateTime.UtcNow;
        }

        hunt.Status = newStatus;
        await _huntRepository.UpdateAsync(hunt);

        return hunt;
    }

    public async Task<IEnumerable<HuntResponse>> GetByStatusAsync(string status)
    {
        var hunts = await _huntRepository.GetByStatusAsync(status);

        var huntsWithSpritesTasks = hunts.Select(async hunt =>
        {
            var pokemonData = await _pokemonService.GetPokemonData(hunt.PokemonName);
            return new HuntResponse
            {
                Id = hunt.Id,
                PokemonName = hunt.PokemonName,
                Method = hunt.Method,
                Attempts = hunt.Attempts,
                Status = hunt.Status,
                Sprite = pokemonData?.Sprites?.Other?.Showdown?.FrontShiny ?? pokemonData?.Sprites?.FrontShiny,
                StartDate = hunt.StartDate,
                EndDate = hunt.EndDate,
                TotalTime = hunt.Status.ToLower() == "active" && hunt.LastActiveDate.HasValue 
                    ? hunt.AccumulatedTime + (DateTime.UtcNow - hunt.LastActiveDate.Value)
                    : hunt.AccumulatedTime
            };
        });

        return await Task.WhenAll(huntsWithSpritesTasks);
    }

    public async Task<HuntResponse?> GetHuntByIdAsync(int Id)
    {
        var hunt = await _huntRepository.GetByIdAsync(Id);
        if (hunt == null)
            return null;

        var pokemonData = await _pokemonService.GetPokemonData(hunt.PokemonName);
        return new HuntResponse
        {
            Id = hunt.Id,
            PokemonName = hunt.PokemonName,
            Method = hunt.Method,
            Attempts = hunt.Attempts,
            Status = hunt.Status,
            Sprite = pokemonData?.Sprites?.Other?.Showdown?.FrontShiny ?? pokemonData?.Sprites?.FrontShiny,
            StartDate = hunt.StartDate,
            EndDate = hunt.EndDate,
            TotalTime = hunt.Status.ToLower() == "active" && hunt.LastActiveDate.HasValue 
                ? hunt.AccumulatedTime + (DateTime.UtcNow - hunt.LastActiveDate.Value)
                : hunt.AccumulatedTime
        };
    }

    public async Task<bool> DeleteHuntAsync(int Id)
    {
        var hunt = await _huntRepository.GetByIdAsync(Id);
        if (hunt == null)
            return false;

        await _huntRepository.DeleteAsync(hunt);
        return true;
    }
}





