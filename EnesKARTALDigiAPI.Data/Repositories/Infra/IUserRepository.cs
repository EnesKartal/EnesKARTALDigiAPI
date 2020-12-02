using EnesKARTALDigiAPI.Data.Models;
using System.Threading.Tasks;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        User GetUserByName(string name);
    }
}
