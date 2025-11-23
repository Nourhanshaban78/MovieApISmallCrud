using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }





        [HttpGet] //Get all Genres
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genresService.GetAllGenre();
            return Ok(genres);
        }




        [HttpPost]
        public async Task<IActionResult> AddGenres(CreateGenresDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _genresService.Add(genre);
            return Ok(genre);
        }




        [HttpPut("{id}")]  //update genres
        public async Task<IActionResult> UpdateGenres(byte id, [FromBody] GenreDto dto)
        {
            var genre = await _genresService.GenreById(id);
            if (genre == null)
                return NotFound($"Not Found This Id {id}");

            genre.Name = dto.Name;
            _genresService.Update(genre);
            return Ok(genre);

        }



        [HttpDelete("{id}")]  //Delete Genres
        public async Task<IActionResult>DeleteGener(byte id)
        {
            var genre = await _genresService.GenreById(id);
            if (genre == null)
                return NotFound($"Not Found This Id {id}");

           _genresService.Delete(genre);
            return Ok(genre);

        }



    }
}
