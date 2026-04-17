namespace PokeHub.API.Models
{
    public class Hunt
    {
        public int Id { get; set; }
        public string PokemonName { get; set; }
        public string Method { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; } // hunting / completed
    }
}