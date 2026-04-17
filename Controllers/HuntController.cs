using Microsoft.AspNetCore.Mvc;
using PokeHub.API.Models;
using PokeHub.API.Services;

namespace PokeHub.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class HuntController : ControllerBase
    {

    private readonly HuntService _huntService;
    private readonly PokemonService _pokeService;

    public HuntController(HuntService huntService, PokemonService pokeService)
    {
        _huntService = huntService;
        _pokeService = pokeService;
    }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var huntsWithSprites = await _huntService.GetAllHuntsAsync();
            return Ok(huntsWithSprites);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hunt hunt)
        {
            try
            {
                var createdHunt = await _huntService.CreateHuntAsync(hunt);
                return Ok(createdHunt);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/increment")]
        public async Task<IActionResult> Increment(int id)
    {
        var hunt = await _huntService.IncrementAttempts(id);

        if (hunt == null)
        {
            return NotFound();
        }
        return Ok(hunt);
    }

        [HttpPatch("{id}/status/{status}")]
        public async Task<IActionResult> Status(int id, string status)
        {
            try
            {
                var hunt = await _huntService.StatusHunt(id, status);

                if (hunt == null)
                {
                    return NotFound();
                }
                return Ok(hunt);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("pokemon/{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            var result = await _pokeService.GetPokemonData(name);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
    {
        var hunts = await _huntService.GetByStatusAsync(status);
        return Ok(hunts);
    }

}