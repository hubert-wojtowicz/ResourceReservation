using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure;

namespace ReservationApi.Application.Repository
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly ReservationDbContext _ctx;
        private DbSet<Reservation> Colection => _ctx.Set<Reservation>();

        public ReservationRepository(ReservationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Add(Reservation entity)
        {
            await Colection.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(Reservation entity)
        {
            _ctx.Set<Reservation>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Reservation>> GetAll(IPaginable paginationParams)
        {
            return await Colection.AsNoTracking()
                .OrderBy(x => x.ReservationId)
                .Take(paginationParams.Take)
                .Skip(paginationParams.Skip)
                .ToListAsync();
        }
    }
}
