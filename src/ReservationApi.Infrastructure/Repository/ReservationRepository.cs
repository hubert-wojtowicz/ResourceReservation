using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure;

namespace ReservationApi.Application.Repository
{
    public class ReservationRepository : IRepository<ReservationDbEntity>
    {
        private readonly ReservationDbContext _ctx;
        private DbSet<ReservationDbEntity> Colection => _ctx.Set<ReservationDbEntity>();

        public ReservationRepository(ReservationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Add(ReservationDbEntity entity)
        {
            await Colection.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(ReservationDbEntity entity)
        {
            _ctx.Set<ReservationDbEntity>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<ReservationDbEntity>> GetAll(IPaginable paginationParams)
        {
            return await Colection.AsNoTracking()
                .OrderBy(x => x.ReservationId)
                .Take(paginationParams.Take)
                .Skip(paginationParams.Skip)
                .ToListAsync();
        }

        public async Task<ReservationDbEntity?> Get(Guid id)
        {
            return await Colection.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ReservationId == id);
        }
    }
}
