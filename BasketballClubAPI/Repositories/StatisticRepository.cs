using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubAPI.Repositories {
    public class StatisticRepository: IStatisticRepository {
        private readonly DataContext _dataContext;

        public StatisticRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public ICollection<Statistic> GetAllStatistics()
        {
            return _dataContext.Statistic
                .Include(s => s.Match)
                .Include(s => s.Player)
                .ToList();
        }

        public Statistic GetStatisticByPrimaryKey(int matchId, int playerId)
        {
            return _dataContext.Statistic
                .Include(s => s.Match)
                .Include(s => s.Player)
                .FirstOrDefault(s => s.MatchId == matchId && s.PlayerId == playerId);
        }

        public bool StatisticExists(int matchId, int playerId)
        {
            return _dataContext.Statistic.Any(s => s.MatchId == matchId && s.PlayerId == playerId);
        }

        public bool CreateStatistic(Statistic statistic)
        {
            _dataContext.Add(statistic);
            return Save();
        }

        public bool UpdateStatistic(Statistic statistic)
        {
            _dataContext.Update(statistic);
            return Save();
        }

        public bool DeleteStatistic(Statistic statistic)
        {
            _dataContext.Remove(statistic);
            return Save();
        }

        public bool Save() => _dataContext.SaveChanges() > 0;


    }
}
