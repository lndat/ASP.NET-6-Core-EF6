namespace csm6.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = String.Empty;
        public ICollection<Player>? Players { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
