using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;

namespace BasketballClubAPI.Repositories {
    public class PlayerRepository:IPlayerRepository {
        private readonly DataContext _dataContext;

        public PlayerRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public ICollection<Player> GetAllPlayers() {
            return _dataContext.Player.ToList();
        }

        public Player GetPlayerById(int id) {
            return _dataContext.Player.FirstOrDefault(p => p.Id == id);
        }

        public bool PlayerExists(int id) {
            return _dataContext.Player.Any(p => p.Id == id);
        }

        public bool CreatePlayer(Player player)
        {
            if (!_dataContext.Team.Any(t => t.Id == player.TeamId)) {
                // Team with the provided TeamId does not exist, return an error
                return false;
            }
            _dataContext.Add(player);
            return Save();
        }

        public bool UpdatePlayer(Player player) {
            if (!_dataContext.Team.Any(t => t.Id == player.TeamId)) {
                // Team with the provided TeamId does not exist, return an error
                return false;
            }
            _dataContext.Update(player);
            return Save();
        }
        public bool DeletePlayer(Player player) {
            _dataContext.Remove(player);
            return Save();
        }
        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

    }
}
