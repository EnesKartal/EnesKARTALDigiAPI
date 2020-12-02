using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnesKARTALDigiAPI.Data.Repositories.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DigiBlogDBContext digiBlogDBContext;

        public Repository(DigiBlogDBContext digiBlogDBContext)
        {
            this.digiBlogDBContext = digiBlogDBContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return digiBlogDBContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"kayıtlar listelenemedi: {ex.Message}");
            }
        }

        public bool Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} kayıt boş olamaz");
            }

            try
            {
                digiBlogDBContext.Add(entity);
                digiBlogDBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} kaydedilemedi: {ex.Message}");
            }
        }

        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} kayıt boş olamaz");
            }

            try
            {
                digiBlogDBContext.Update(entity);
                digiBlogDBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} güncellenemedi: {ex.Message}");
            }
        }

        public bool Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} kayıt boş olamaz");
            }

            try
            {
                digiBlogDBContext.Remove(entity);
                digiBlogDBContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} silinemedi: {ex.Message}");
            }
        }
    }
}
