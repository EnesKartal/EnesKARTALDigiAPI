using System.Linq;
using System.Threading.Tasks;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
