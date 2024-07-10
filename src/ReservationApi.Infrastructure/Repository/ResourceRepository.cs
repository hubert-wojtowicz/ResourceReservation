using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure;
using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Application.Repository
{
    public class ResourceRepository : IRepository<Resource>
    {
        private readonly ReservationDbContext _ctx;
        private DbSet<Resource> Colection => _ctx.Set<Resource>();

        public ResourceRepository(ReservationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Add(Resource entity)
        {
            await Colection.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(Resource entity)
        {
            Colection.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Resource>> GetAll(IPaginable paginationParams)
        {
            return await Colection.AsNoTracking()
                .OrderBy(x => x.ResourceId)
                .Take(paginationParams.Take)
                .Skip(paginationParams.Skip)
                .ToListAsync();
        }

        public async Task<Resource?> Get(Guid id)
        {
            return await Colection.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ResourceId == id);
        }
    }
}
