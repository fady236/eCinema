 using eCinema.Data.Base;
using eCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCinema.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor> , IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context) : base(context) { }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
