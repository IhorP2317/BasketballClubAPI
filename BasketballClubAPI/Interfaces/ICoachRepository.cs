using BasketballClubAPI.Models;

namespace BasketballClubAPI.Interfaces {
    public interface ICoachRepository {
        ICollection<Coach> GetAllCoaches();
        Coach GetCoachById(int id);
        bool CoachExists(int id);
        bool CreateCoach(Coach coach);
        bool UpdateCoach(Coach coach);
        bool DeleteCoach(Coach coach);
        bool Save();
    }
}
