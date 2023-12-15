using BasketballClubAPI.Models;

namespace BasketballClubAPI.Interfaces {
    public interface IMatchRepository {
        ICollection<Match> GetAllMatches();
        ICollection<Match> GetAllMtachesByTeamId(int teamId);
        Match GetMatchById(int id);
        bool MatchExists(int id);
        bool CreateMatch(Match match);
        bool UpdateMatch(Match match);
        bool DeleteMatch(Match match);
        bool Save();
    }
}
