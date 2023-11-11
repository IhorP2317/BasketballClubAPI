using BasketballClubAPI.Models;

namespace BasketballClubAPI.Interfaces {
    public interface IPlayerRepository {
        ICollection<Player> GetAllPlayers();
        Player GetPlayerById(int id);
        bool PlayerExists(int id);
        bool CreatePlayer(Player player);
        bool UpdatePlayer(Player player);
        bool DeletePlayer(Player player);
        bool Save();
    }
}
