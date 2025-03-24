using eCinema.Data.Base;
using eCinema.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCinema.Data.Services
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
        Task SaveChangesAsync();
    }
}
