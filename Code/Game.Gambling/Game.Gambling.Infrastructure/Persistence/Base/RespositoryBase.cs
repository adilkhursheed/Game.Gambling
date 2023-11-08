using Game.Gambling.Domain.Entities;
using Game.Gambling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gambling.Infrastructure.Persistence.Base
{
    /// <summary>
    /// Generic base repository for regular CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RespositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly GamblingDBConext gamblingDBConext;
        #endregion

        #region Constructor
        public RespositoryBase(GamblingDBConext gamblingDBConext)
        {
            this.gamblingDBConext = gamblingDBConext;
        }
        #endregion

        #region Public Methods
        public async Task<IReadOnlyCollection<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await gamblingDBConext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await gamblingDBConext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await gamblingDBConext.Set<T>().FindAsync(id);
        }

        public async Task<int> Save(T entity)
        {
            await gamblingDBConext.Set<T>().AddAsync(entity);
            return await gamblingDBConext.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
            gamblingDBConext.Set<T>().Update(entity);
            var res = await gamblingDBConext.SaveChangesAsync();
            return res > 0 ? true : false;
        }

        #endregion
    }
}
