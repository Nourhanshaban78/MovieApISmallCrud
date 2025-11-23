using APIProject.Models;

namespace APIProject.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> MovieById(int id);
        Task <Movie> Add(Movie movie );
        Movie Update(Movie movie);
        Movie Delete(Movie movie );


    }
}
