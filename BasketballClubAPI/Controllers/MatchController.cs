using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BasketballClubAPI.Controllers {
    [Route("api/matches")]
    [ApiController]
    public class MatchController: ControllerBase {
        private readonly IMatchRepository  _matchRepository;
        private readonly ITeamRepository _teamRepository;
        IMapper _mapper;
        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository, IMapper mapper) {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchDto>))]
        public IActionResult GetAllMatches() {
            var matches = _matchRepository.GetAllMatches();

            var responseList = _mapper.Map<List<MatchDto>>(matches);
            return Ok(responseList);

        }

        [HttpGet("team/{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllMatchesByTeamId([FromRoute] int teamId)
        {
            if (!_teamRepository.TeamExists(teamId))
                return NotFound();
            var matches = _matchRepository.GetAllMtachesByTeamId(teamId);
            var responseList = _mapper.Map<List<MatchDto>>(matches);
            return Ok(responseList);
        }

        [HttpGet("match/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMatchById([FromRoute] int id) {
            if (!_matchRepository.MatchExists(id))
                return NotFound();

            var match = _matchRepository.GetMatchById(id);
            var response = _mapper.Map<MatchDto>(match);

            return Ok(response);
        }
        [HttpGet("{matchId}/homeTeam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHomeTeamByMatch(int matchId) {
            if (!_matchRepository.MatchExists(matchId))
                return NotFound();

            var homeTeam = _matchRepository.GetMatchById(matchId).HomeTeam;
            var response = _mapper.Map<TeamDto>(homeTeam);
            return Ok(response);
        }
        [HttpGet("{matchId}/awayTeam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAwayTeamByMatch(int matchId) {
            if (!_matchRepository.MatchExists(matchId))
                return NotFound();

            var awayTeam = _matchRepository.GetMatchById(matchId).AwayTeam;
            var response = _mapper.Map<TeamDto>(awayTeam);
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateMatch([FromBody] MatchDto matchDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_teamRepository.TeamExists(matchDto.AwayTeamId) || !_teamRepository.TeamExists(matchDto.HomeTeamId)) {
                ModelState.AddModelError("TeamId", "Invalid Id of Home or Away Team. Team with the provided Id does not exist.");
                return BadRequest(ModelState);
            }
            var match = _mapper.Map<Match>(matchDto);

            if (!_matchRepository.CreateMatch(match)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetMatchById), new { id = match.Id }, match);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateCoach([FromRoute] int id, [FromBody] MatchDto matchDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_matchRepository.MatchExists(id))
                return NotFound();
            if (!_teamRepository.TeamExists(matchDto.AwayTeamId) || !_teamRepository.TeamExists(matchDto.HomeTeamId)) {
                ModelState.AddModelError("TeamId", "Invalid Id of Home or Away Team. Team with the provided Id does not exist.");
                return BadRequest(ModelState);
            }

            var matchToUpdate = _mapper.Map<Match>(matchDto);
            matchToUpdate.Id = id;

            if (!_matchRepository.UpdateMatch(matchToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTeam([FromRoute] int id) {
            if (!_matchRepository.MatchExists(id))
                return NotFound();

            var matchToDelete = _matchRepository.GetMatchById(id);

            if (!_matchRepository.DeleteMatch(matchToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}
