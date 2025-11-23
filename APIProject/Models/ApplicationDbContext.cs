using Microsoft.EntityFrameworkCore;

namespace APIProject.Models
{
    public class ApplicationDbContext :DbContext 
    {
        //To Add A Class Model 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Genre>Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

       
    }
}
