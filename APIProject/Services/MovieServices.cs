using APIProject.Dtos;
using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
             await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Task<Movie> Delete(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _context.Movies.
                Where(m=>m.GenreId == genreId || genreId == 0).
                OrderByDescending(m => m.Rate).Include(m => m.Genre).
                ToListAsync();
        }

        public async Task<Movie> MovieById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id); 
        }

        Movie IMovieServices.Delete(Movie movie)
        {

            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        Movie IMovieServices.Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return  movie;
        }
    }
}
