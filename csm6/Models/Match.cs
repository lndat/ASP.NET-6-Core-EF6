namespace csm6.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string? Score { get; set; }

        public League League { get; set; }
        public int? LeagueId { get; set; }

    }
}
