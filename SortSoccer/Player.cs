namespace SortSoccer
{
    public class Player
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public float Stars => (Defense + Attack + BallControl + Speed) / 4;
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int BallControl { get; set; }
        public int Speed { get; set; }
        public bool IsKey { get; set; }
        public Position PreferredPosition { get; set; }
        public Team? Team { get; set; }
    }
}
