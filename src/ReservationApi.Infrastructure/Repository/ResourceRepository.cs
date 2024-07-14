using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure;
using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Application.Repository
{
    public class ResourceRepository : IRepository<ResourceDbEntity>
    {
        private readonly ReservationDbContext _ctx;
        private DbSet<ResourceDbEntity> Colection => _ctx.Set<ResourceDbEntity>();

        public ResourceRepository(ReservationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Add(ResourceDbEntity entity)
        {
            await Colection.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(ResourceDbEntity entity)
        {
            Colection.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<ResourceDbEntity>> GetAll(IPaginable paginationParams)
        {
            return await Colection.AsNoTracking()
                .OrderBy(x => x.ResourceId)
                .Take(paginationParams.Take)
                .Skip(paginationParams.Skip)
                .ToListAsync();
        }

        public async Task<ResourceDbEntity?> Get(Guid id)
        {
            return await Colection.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ResourceId == id);
        }
    }
}
