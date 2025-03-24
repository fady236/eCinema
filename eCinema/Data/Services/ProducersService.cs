using eCinema.Data.Base;
using eCinema.Models;

namespace eCinema.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer> , IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) { }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
