namespace PokeHub.API.DTOs;

public class HuntResponse
{
    public int Id { get; set; }
    public string PokemonName { get; set; }
    public string Method { get; set; }
    public int Attempts { get; set; }
    public string Status { get; set; }
    public string Sprite { get; set; }
    public TimeSpan TotalTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}