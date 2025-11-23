using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    { 
        private readonly IMovieServices _movieServices;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;
        public MoviesController(IMovieServices movieServices, IGenresService genresService, IMapper mapper)
        {
            _movieServices = movieServices;
            _genresService = genresService;
            _mapper = mapper;
        }



        private new  List<string> _allowExtentions = new List<string>{ ".jpg", ".png" };
        private long _maxAllowedsize = 1048576;



        [HttpGet]
        public async Task<IActionResult> GetAllMovies(byte genreId)
        {
            var movies = await _movieServices.GetAll();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }


        [HttpGet("{id}")]   //Get Movie By Id
        public async Task<IActionResult> FindMovieByID(int id)
        {
            var movie = await _movieServices.MovieById(id);

            if (movie == null)
                return BadRequest("Not Found");

            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }






        [HttpGet("GetGenreById")]  //Get Genre ById
        public async Task<IActionResult> GetGenreMovieById(byte genreId)
        {
            var movie = await _movieServices.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movie);


            return Ok(data);
        }




        [HttpPost]   //Add Movie Post
        public async Task<IActionResult> AddMovies([FromForm]CreateMovie dto)
        {

            if (!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png And .jpg images are allowed!");
            if (dto.Poster.Length > _maxAllowedsize)
                return BadRequest("Max Length size for Poster is 1Mb!");
            var isInvaliedGenre = await _genresService.IsValidGenre(dto.GenreId);

            if (!isInvaliedGenre)
                return BadRequest("There's No Invalied Genre ID ");


                    using var datastream = new MemoryStream();
                    await dto.Poster.CopyToAsync(datastream);

                    var movie = _mapper.Map<Movie>(dto);  // ana ba5od men elmovie 3lashan ahwlo l dto 
                    movie.Poster = datastream.ToArray(); 
                    _movieServices.Add(movie);
                    return Ok(movie);


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatMovie([FromForm] UpdateMovie dto, int id)
        {
            var movie = await _movieServices.MovieById(id);
            if (movie == null)
                return BadRequest($"There's Not Found This ID :{id}");

            var isInvaliedGenre = await _genresService.IsValidGenre(dto.GenreId);
            if (!isInvaliedGenre)
                return BadRequest("There's No Invalied Genre ID ");

            if (dto.Poster != null)
            {
                if (!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png And .jpg images are allowed!");
                if (dto.Poster.Length > _maxAllowedsize)
                    return BadRequest("Max Length size for Poster is 1Mb!");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();

            }
            movie.Title = dto.Title;
            movie.storyline = dto.storyline;
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;

            _movieServices.Update(movie);
            return Ok(movie);
        }



        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieServices.MovieById(id);

            if (movie == null)
                return BadRequest($"There's Not Found This ID :{id}");

            _movieServices.Delete(movie);
            return Ok(movie);

        }
      

    }
}
