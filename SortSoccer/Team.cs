namespace SortSoccer
{
    public class Team
    {
        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; } =  new List<Player>();

    }
}
