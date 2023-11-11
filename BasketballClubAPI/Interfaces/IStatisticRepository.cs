using BasketballClubAPI.Models;

namespace BasketballClubAPI.Interfaces {
    public interface IStatisticRepository {
        ICollection<Statistic> GetAllStatistics();
        Statistic GetStatisticByPrimaryKey(int matchId, int playerId);
        bool StatisticExists(int matchId, int playerId);
        bool CreateStatistic(Statistic statistic);
        bool UpdateStatistic(Statistic statistic);
        bool DeleteStatistic(Statistic statistic);
        bool Save();
    }
}
