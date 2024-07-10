using ReservationApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApi.Application.Repository
{
    public class ReservationRepository : IRepository<Reservation>
    {
        public ReservationRepository()
        {
            
        }

        public Reservation Add(Reservation entity)
        {
            throw new NotImplementedException();
        }

        public Reservation Delete(Reservation entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Reservation> GetAll(int take, int skip, Expression<Func<Reservation>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
