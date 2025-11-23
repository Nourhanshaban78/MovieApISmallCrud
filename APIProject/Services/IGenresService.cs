using APIProject.Models;

namespace APIProject.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAllGenre();
        Task <Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<Genre> GenreById(byte  id);
        Task<bool> IsValidGenre(byte id); 
       

    }
}
