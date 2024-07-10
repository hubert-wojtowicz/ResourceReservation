using System.Linq.Expressions;

namespace ReservationApi.Application.Repository
{
    public interface IRepository<TEntity>
    {
        ICollection<TEntity> GetAll(int take, int skip, Expression<Func<TEntity>> filter);

        TEntity Add(TEntity entity);

        TEntity Delete(TEntity entity);
    }
}
