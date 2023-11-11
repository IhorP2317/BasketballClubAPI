using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubAPI.Repositories {
    public class MatchRepository: IMatchRepository {
        private readonly DataContext _dataContext;

        public MatchRepository(DataContext dataContext) {
            _dataContext  = dataContext;
        }
        public ICollection<Match> GetAllMatches()
        {
            return _dataContext.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Statistics)
                .ToList();
        }

        public Match GetMatchById(int id)
        {
            return _dataContext.Match
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Statistics)
                .FirstOrDefault(m => m.Id == id);
        }

        public bool MatchExists(int id)
        {
            return _dataContext.Match.Any(m => m.Id == id);
        }

        public bool CreateMatch(Match match)
        {
            _dataContext.Add(match);
            return Save();
        }

        public bool UpdateMatch(Match match)
        {
            _dataContext.Update(match);
            return Save();
        }

        public bool DeleteMatch(Match match)
        {
            _dataContext.Remove(match);
            return Save();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
