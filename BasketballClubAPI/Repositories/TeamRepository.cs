using BasketballClubAPI.Data;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubAPI.Repositories {
    public class TeamRepository: ITeamRepository {
        private readonly DataContext _dataContext;

        public TeamRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }
        public ICollection<Team> GetAllTeams()
        {
           return _dataContext.Team
               .Include(t => t.Players)
               .Include(t => t.HeadCoach)
               .ToList();
        }

        public Team GetTeamById(int id)
        {
           return _dataContext.Team
               .Include(t => t.Players)
               .Include(t => t.HeadCoach)
               .FirstOrDefault(t => t.Id == id);
        }

       



        public bool TeamExists(int id)
        {
            return _dataContext.Team.Any(t => t.Id == id);
        }

        public bool CreateTeam(Team team){
            if (!_dataContext.Coach.Any(c => c.Id == team.HeadCoachId)) {
            // Coach with the provided HeadCoachid does not exist, return an error
            return false;
        }
        
            _dataContext.Add(team);
            return Save();
        }

        public bool UpdateTeam(Team team)
        {
            if (!_dataContext.Coach.Any(c => c.Id == team.HeadCoachId)) {
                // Coach with the provided HeadCoachid does not exist, return an error
                return false;
            }

            _dataContext.Update(team);
            return Save();
        }

        public bool DeleteTeam(Team team)
        {
            _dataContext.Remove(team);
            return Save();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
