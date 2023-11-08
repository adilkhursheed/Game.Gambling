using Game.Gambling.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(long id);
        Task<IReadOnlyCollection<T>> Get(Expression<Func<T, bool>> expression);
        Task<int> Save(T entity);
        Task<bool> Update(T entity);

    }
}
