﻿using AutoMapper;
using BasketballClubAPI.Dto;
using BasketballClubAPI.Interfaces;
using BasketballClubAPI.Models;
using BasketballClubAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BasketballClubAPI.Controllers {
    [Route("api/players")]
    [ApiController]
    public class PlayerController: ControllerBase {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public PlayerController(IPlayerRepository playerRepository,
            IMapper mapper) {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
        public IActionResult GetAllPlayers() {
            var players = _playerRepository.GetAllPlayers();

            var responseList = _mapper.Map<List<PlayerDto>>(players);
            return Ok(responseList);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlayerById([FromRoute] int id) {
            if (!_playerRepository.PlayerExists(id))
                return NotFound();

            var test = _playerRepository.GetPlayerById(id);
            var response = _mapper.Map<PlayerDto>(test);
            return Ok(response);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PlayerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreatePlayer([FromBody] PlayerDto PlayerDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var player = _mapper.Map<Player>(PlayerDto);

            if (!_playerRepository.CreatePlayer(player)) {
                // Handle the error when TeamId is not valid
                ModelState.AddModelError("TeamId", "Invalid TeamId. Team with the provided TeamId does not exist.");
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult UpdatePlayer([FromRoute] int id, [FromBody] PlayerDto playerDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_playerRepository.PlayerExists(id))
                return NotFound();

            var playerToUpdate = _mapper.Map<Player>(playerDto);
            playerToUpdate.Id = id;

            if (!_playerRepository.UpdatePlayer(playerToUpdate)) {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Deleteplayer([FromRoute] int id) {
            if (!_playerRepository.PlayerExists(id))
                return NotFound();

            var playerToDelete = _playerRepository.GetPlayerById(id);

            if (!_playerRepository.DeletePlayer(playerToDelete)) {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
    }
   
}