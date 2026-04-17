namespace PokeHub.API.Models
{
    public class Hunt
    {
        public int Id { get; set; }
        public string PokemonName { get; set; }
        public string Method { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; } // active / completed / paused

        public DateTime StartDate { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public TimeSpan AccumulatedTime { get; set; }
        public DateTime? EndDate { get; set; }
    }
}