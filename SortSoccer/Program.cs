// See https://aka.ms/new-console-template for more information
using SortSoccer;

Console.WriteLine("Hello, World!");



var players = new List<Player>
{
    new Player{ Name= "Taruma", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.ATA, IsKey=true, Speed=4},
    new Player{ Name= "Bruno", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.DEF, Speed=2},
    new Player{ Name= "Ramon", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.MID, Speed=2},
    new Player{ Name= "Rafael", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.MID, Speed=3},
    new Player{ Name= "Pedro", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.ATA, IsKey=true, Speed=5},
    new Player{ Name= "Uanderson", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.MID, IsKey=true, Speed=5},
    new Player{ Name= "Rodrigo V", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.DEF, Speed=2},
    new Player{ Name= "Matheus", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.MID, Speed=3},
    new Player{ Name= "Kleber", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.MID, Speed=3},
    new Player{ Name= "Luis", Attack=4, Defense=4, BallControl= 4, PreferredPosition=Position.ATA, IsKey=true, Speed=4},
    new Player{ Name= "Gleidson", Attack=1, Defense=2, BallControl= 2, PreferredPosition=Position.DEF, IsKey=false, Speed=1},
};

var match = new Match()
{
    Id = Guid.NewGuid(),
    MaxPlayers = 20,
    MinPlayers = 10,
    PlayersPerTeam = 5,
    ConfirmedPlayers = players
};

match.DistributeTeam();

Console.ReadLine();

// How many goalkeepers
int goalkeepers = players.Where(a => a.PreferredPosition == Position.GOL).Count();

// Calc the amount of teams based on the players and players per match
