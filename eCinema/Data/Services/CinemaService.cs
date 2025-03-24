using eCinema.Data.Base;
using eCinema.Models;

namespace eCinema.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context) { }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
