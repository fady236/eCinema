using eCinema.Data.Base;
using eCinema.Models;

namespace eCinema.Data.Services
{
    public interface IProducersService : IEntityBaseRepository<Producer>
    {
        Task SaveChangesAsync();
    }
}
