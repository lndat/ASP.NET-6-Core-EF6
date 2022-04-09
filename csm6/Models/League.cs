namespace csm6.Models
{
    public class League
    {
        public int Id { get; set; }
        public string LeagueName { get; set; } = String.Empty;

        public ICollection<Team>? Teams { get; set; }
        public ICollection<Match>? Matches { get; set; }
    }
}
