using System;
using System.Linq;
using System.Linq.Expressions;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);

    }
}
