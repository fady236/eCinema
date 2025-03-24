using eCinema.Data.Base;
using eCinema.Models;

namespace eCinema.Data.Services
{
    public interface ICinemasService : IEntityBaseRepository<Cinema>
    {
        Task SaveChangesAsync();
    }
}
