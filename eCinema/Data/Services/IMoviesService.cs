using eCinema.Data.Base;
using eCinema.Data.ViewModels;
using eCinema.Models;

namespace eCinema.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
        Task SaveChangesAsync();
    }
}
