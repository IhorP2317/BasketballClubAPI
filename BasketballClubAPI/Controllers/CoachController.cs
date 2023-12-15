using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using BasketballClubAPI.Models;

namespace BasketballClubAPI.Controllers {
    [Route("api/coaches")]
    [ApiController]
    public class CoachController : ControllerBase {
        private readonly ICoachRepository _coachRepository;
        private readonly ITeamRepository _teeamRepository;
        private readonly IMapper _mapper;
        public CoachController(ICoachRepository coachRepository,  IMapper mapper, ITeamRepository teeamRepository) {
            _coachRepository = coachRepository;
            _mapper = mapper;
            _teeamRepository = teeamRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoachDto>))]
        public IActionResult GetAllCoaches() {
            var coaches = _coachRepository.GetAllCoaches();

            var responseList = _mapper.Map<List<CoachDto>>(coaches);
            return Ok(responseList);

        }
        [HttpGet("team/{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoachDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCoachByTeamId([FromRoute] int teamId) {
            if (!_teeamRepository.TeamExists(teamId))
                return NotFound();
            var team = _teeamRepository.GetTeamById(teamId);

            var coach = _coachRepository.GetCoachById((int)team.HeadCoachId);
            var response = _mapper.Map<CoachDto>(coach);

            return Ok(response);
        }
        [HttpGet("coach/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoachDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCoachById([FromRoute] int id) {
            if (!_coachRepository.CoachExists(id))
                return NotFound();

            var coach = _coachRepository.GetCoachById(id);
            var response = _mapper.Map<CoachDto>(coach);

            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CoachDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateCoach([FromBody] CoachDto coachDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var coach = _mapper.Map<Coach>(coachDto);

            if (!_coachRepository.CreateCoach(coach)) {
                throw new DataException("Something went wrong while saving");
            }

            return CreatedAtAction(nameof(GetCoachById), new { id = coach.Id }, coach);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdateCoach([FromRoute] int id, [FromBody] CoachDto CoachDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_coachRepository.CoachExists(id))
                return NotFound();

            var CoachToUpdate = _mapper.Map<Coach>(CoachDto);
            CoachToUpdate.Id = id;

            if (!_coachRepository.UpdateCoach(CoachToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteCoach([FromRoute] int id) {
            if (!_coachRepository.CoachExists(id))
                return NotFound();

            var coachToDelete = _coachRepository.GetCoachById(id);

            if (!_coachRepository.DeleteCoach(coachToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
}
