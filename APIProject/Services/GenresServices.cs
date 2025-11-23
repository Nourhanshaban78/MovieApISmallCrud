using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Services
{
    public class GenresServices : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<Genre> GenreById(byte id)
        {
            return  await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
             return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public Task<bool> IsValidGenre(byte id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
