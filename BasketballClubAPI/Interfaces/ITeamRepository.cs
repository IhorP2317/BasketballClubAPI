using BasketballClubAPI.Models;

namespace BasketballClubAPI.Interfaces {
    public interface ITeamRepository {
        ICollection<Team> GetAllTeams();
        Team GetTeamById(int id);
        Team GetTeamByName(string name);
        bool TeamExists(string name);
        bool TeamExists(int id);
        bool CreateTeam(Team team);
        bool UpdateTeam(Team team);
        bool DeleteTeam(Team team);
        bool Save();
    }
}
