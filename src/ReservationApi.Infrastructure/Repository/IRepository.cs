using ReservationApi.Infrastructure;

namespace ReservationApi.Application.Repository
{
    public interface IRepository<TEntity>
    {
        Task<ICollection<TEntity>> GetAll(IPaginable paginationParams);

        Task Add(TEntity entity);

        Task Delete(TEntity entity);
    }
}
