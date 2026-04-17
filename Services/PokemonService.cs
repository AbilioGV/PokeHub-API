using PokeHub.API.DTOs;
using System.Text.Json;

namespace PokeHub.API.Services;

public class PokemonService
{
    private readonly HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonDto> GetPokemonData(string name)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<PokemonDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return pokemon;
    }
}