using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;

namespace BasketballClubAPI.Repositories {
    public class CoachRepository: ICoachRepository {
        private readonly DataContext _dataContext;

        public CoachRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public ICollection<Coach> GetAllCoaches() {
            return _dataContext.Coach.ToList();
        }

        public Coach GetCoachById(int id) {
            return _dataContext.Coach.FirstOrDefault(c => c.Id == id);
        }

        public bool CoachExists(int id) {
            return _dataContext.Coach.Any(c => c.Id == id);
        }

        public bool CreateCoach(Coach coach) {
            _dataContext.Add(coach);
            return Save();
        }

        public bool UpdateCoach(Coach coach) {
            _dataContext.Update(coach);
            return Save();
        }
        public bool DeleteCoach(Coach coach) {
            _dataContext.Remove(coach);
            return Save();
        }
        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
