using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BasketballClubAPI.Controllers {
    [Route("api/teams")]
    [ApiController]
    public class TeamController: ControllerBase {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository teamRepository, ICoachRepository coachRepository, IMapper mapper) {
            _teamRepository = teamRepository; 
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDto>))]
        public IActionResult GetAllPlayers() {
            var teams = _teamRepository.GetAllTeams();

            var responseList = _mapper.Map<List<TeamDto>>(teams);
            return Ok(responseList);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTeamById([FromRoute] int id) {
            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var team = _teamRepository.GetTeamById(id);
            var response = _mapper.Map<TeamDto>(team);

            return Ok(response);
        }
        [HttpGet("{teamId}/players")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllPlayersByTeam(int teamId) {
            if (!_teamRepository.TeamExists(teamId))
                return NotFound();

            var players = _teamRepository.GetTeamById(teamId).Players;
            var responseList = _mapper.Map<List<PlayerDto>>(players);
            return Ok(responseList);
        }
        [HttpGet("{teamId}/headCoach")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoachDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCoachByTeam(int teamId) {

            if (!_teamRepository.TeamExists(teamId))
                return NotFound();

            var headCoach = _teamRepository.GetTeamById(teamId).HeadCoach;
            var response = _mapper.Map<CoachDto>(headCoach);
            return Ok(headCoach);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateTeam([FromBody] TeamDto teamDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var team = _mapper.Map<Team>(teamDto);

            if (!_teamRepository.CreateTeam(team)) {
                // Handle the error when TeamId is not valid
                ModelState.AddModelError("HeadCoachId", "Invalid HeadCoachId. Head coach with the provided HeadCoachId does not exist.");
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateTeam([FromRoute] int id, [FromBody] TeamDto teamDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var teamToUpdate = _mapper.Map<Team>(teamDto);
            teamToUpdate.Id = id;

            if (!_teamRepository.UpdateTeam(teamToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTeam([FromRoute] int id) {
            if (!_teamRepository.TeamExists(id))
                return NotFound();

            var teamToDelete = _teamRepository.GetTeamById(id);

            if (!_teamRepository.DeleteTeam(teamToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}
