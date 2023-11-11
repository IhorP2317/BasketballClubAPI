using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BasketballClubAPI.Models;

namespace BasketballClubAPI.Controllers {
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController: ControllerBase {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public StatisticController(IStatisticRepository statisticRepository, IMatchRepository matchRepository,
            IPlayerRepository playerRepository, IMapper mapper)
        {
            _statisticRepository = statisticRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _mapper  = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StatisticDto>))]
        public IActionResult GetAllCoaches() {
            var statistics = _statisticRepository.GetAllStatistics();

            var responseList = _mapper.Map<List<StatisticDto>>(statistics);
            return Ok(responseList);

        }
        [HttpGet("{matchId}/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatisticDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStatisticByPrimaryKey([FromRoute] int matchId, [FromRoute] int playerId) {
            if (!_statisticRepository.StatisticExists(matchId, playerId))
                return NotFound();

            var statistic = _statisticRepository.GetStatisticByPrimaryKey(matchId, playerId);
            var response = _mapper.Map<StatisticDto>(statistic);

            return Ok(response);
        }
        [HttpGet("{matchId}/{playerId}/match")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMatchByStatistic(int matchId, int playerId) {
            if (!_statisticRepository.StatisticExists(matchId, playerId))
                return NotFound();

            var match = _statisticRepository.GetStatisticByPrimaryKey(matchId, playerId).Match;
            var response = _mapper.Map<MatchDto>(match);
            return Ok(response);
        }
        [HttpGet("{matchId}/{playerId}/player")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlayerByStatistic(int matchId, int playerId) {
            if (!_statisticRepository.StatisticExists(matchId, playerId))
                return NotFound();

            var player = _statisticRepository.GetStatisticByPrimaryKey(matchId, playerId).Player;
            var response = _mapper.Map<PlayerDto>(player);
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MatchDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateStatistic([FromBody] StatisticDto statisticDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_matchRepository.MatchExists(statisticDto.MatchId) || !_playerRepository.PlayerExists(statisticDto.PlayerId)) {
                ModelState.AddModelError("Id", "Invalid Id of Match or Player. Match or Player with the provided Id does not exist.");
                return BadRequest(ModelState);
            }
            var statistic = _mapper.Map<Statistic>(statisticDto);

            if (!_statisticRepository.CreateStatistic(statistic)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetStatisticByPrimaryKey), new { matchId = statistic.MatchId,playerId = statistic.PlayerId  }, statistic);
        }
        [HttpPut("{matchId}/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateStatistic([FromRoute] int matchId, [FromRoute] int playerId, [FromBody] StatisticDto statisticDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_statisticRepository.StatisticExists(matchId, playerId))
                return NotFound();
            if (!_matchRepository.MatchExists(statisticDto.MatchId) || !_playerRepository.PlayerExists(statisticDto.PlayerId)) {
                ModelState.AddModelError("Id", "Invalid Id of Match or Player. Match or Player with the provided Id does not exist.");
                return BadRequest(ModelState);
            }

            var statisticToUpdate = _mapper.Map<Statistic>(statisticDto);
            statisticToUpdate.MatchId = matchId;
            statisticToUpdate.PlayerId = playerId;

            if (!_statisticRepository.UpdateStatistic(statisticToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }
        [HttpDelete("{matchId}/{playerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTeam([FromRoute] int matchId, [FromRoute] int playerId) {
            if (!_statisticRepository.StatisticExists(matchId, playerId))
                return NotFound();

            var statisticToDelete = _statisticRepository.GetStatisticByPrimaryKey( matchId,  playerId);

            if (!_statisticRepository.DeleteStatistic(statisticToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }

    }
}
