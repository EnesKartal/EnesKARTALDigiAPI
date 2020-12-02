using EnesKARTALDigiAPI.Data.Models;
using System.Linq;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(int id);
        User GetUserByName(string name);
        IQueryable<User> GetAllUserWithoutPass();
    }
}
