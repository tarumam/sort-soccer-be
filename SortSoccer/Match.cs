using System;
using System.ComponentModel;

namespace SortSoccer
{
    public class Match
    {

        public Guid Id { get; set; }
        public int PlayersPerTeam { get; set; }
        public int MatchDuration { get; set; }
        public int MaxPlayers { get; set; }
        public int MinPlayers { get; set; }
        public List<string>? Rules { get; set; }
        public List<Player> RegisteredPlayers { get; set; } = new List<Player>();
        public List<Player> ConfirmedPlayers { get; set; } = new List<Player>();
        public List<Player> SupplentPlayers { get; set; } = new List<Player>();
        public int NumberOfConfirmedGoalKeepers => ConfirmedPlayers.Where(p => p.PreferredPosition == Position.GOL).Count();
        public int NumberOfTeams => (int)Math.Ceiling((decimal)ConfirmedPlayers.Count / (decimal)PlayersPerTeam);

        public void DistributeTeam()
        {
            var teams = new List<Team>();
            var chosen = new List<MatchPlayer>();

            // Get Key players
            var teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var kp = ConfirmedPlayers.Where(a => a.IsKey && chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderByDescending(a => a.Stars).Take(NumberOfTeams).ToList();
            chosen.AddRange(kp.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.KeyPlayer, TeamId = teamIds[index] }));

            // Get Worse players
            teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var wp = ConfirmedPlayers.Where(a => !a.IsKey && a.PreferredPosition != Position.GOL && chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderBy(a => a.Stars).Take(NumberOfTeams).ToList();
            chosen.AddRange(wp.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.WorsePlayer, TeamId = teamIds[index] }));

            // Get fastest Players
            teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var fp = ConfirmedPlayers.Where(a => !a.IsKey && chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderByDescending(a => a.Speed).Take(NumberOfTeams).ToList();
            chosen.AddRange(fp.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.FastestPlayer, TeamId = teamIds[index] }));

            // Get Attack Players
            teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var ap = ConfirmedPlayers.Where(a => !a.IsKey && chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderByDescending(a => a.Attack).Take(NumberOfTeams).ToList();
            chosen.AddRange(ap.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.AttackPlayer, TeamId = teamIds[index] }));

            // Get Defense Players
            teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var dp = ConfirmedPlayers.Where(a => !a.IsKey && chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderByDescending(a => a.Defense).Take(NumberOfTeams).ToList();
            chosen.AddRange(dp.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.DefensePlayer, TeamId = teamIds[index] }));

            // Get the players which are not on the above lists
            teamIds = Enumerable.Range(0, NumberOfTeams).OrderBy(n => new Random().Next()).ToList();
            var co = ConfirmedPlayers.Where(a => chosen.Select(a => a.PlayerId).Contains(a.Id) == false).OrderByDescending(a => a.Stars).ToList();
            chosen.AddRange(co.Select((p, index) => new MatchPlayer { PlayerId = p.Id, ChosenAs = ChosenAsEnum.Rest, TeamId = teamIds[index] }));

            // Create the teams
            for (int t = 0; t < NumberOfTeams; t++)
            {
                teams.Add(new Team(t, $"Time {t + 1}"));
                var selectedTeam = from cp in ConfirmedPlayers
                                   join ch in chosen on cp.Id equals ch.PlayerId
                                   where ch.TeamId == t
                                   select cp;
                teams[t].Players.AddRange(selectedTeam);
            }

            foreach (var team in teams)
            {
                Console.WriteLine(team.Name);
                foreach (var player in team.Players)
                {
                    Console.WriteLine(player.Name);
                }
            }
        }
    }
}
