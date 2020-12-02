using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnesKARTALDigiAPI.Data.Repositories.Repo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DigiBlogDBContext digiBlogDBContext) : base(digiBlogDBContext)
        {
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public User GetUserByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Email == name);
        }
    }
}