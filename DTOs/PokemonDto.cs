using System.Text.Json.Serialization;

namespace PokeHub.API.DTOs;

public class PokemonDto
{
    public string Name { get; set; }
    public SpriteDto Sprites { get; set; }
}

public class SpriteDto
{
    [JsonPropertyName("front_shiny")]
    public string FrontShiny { get; set; }

    [JsonPropertyName("other")]
    public OtherSprites Other { get; set; }
}

public class OtherSprites
{
    [JsonPropertyName("showdown")]
    public ShowdownSprites Showdown { get; set; }
}

public class ShowdownSprites
{
    [JsonPropertyName("front_shiny")]
    public string FrontShiny { get; set; }
}