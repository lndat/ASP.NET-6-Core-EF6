namespace csm6.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; } = String.Empty;
        public int Age { get; set; }
        public int Aim { get; set; }
        public int Experience { get; set; }

        public Team? Team { get; set; }

    }
}
