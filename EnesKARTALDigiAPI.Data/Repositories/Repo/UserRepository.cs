using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using System.Linq;

namespace EnesKARTALDigiAPI.Data.Repositories.Repo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DigiBlogDBContext digiBlogDBContext) : base(digiBlogDBContext)
        {
        }

        public User GetUserById(int id)
        {
            return GetAll().Select(x => new User
            {
                Id = x.Id,
                Email = x.Email,
                Token = x.Token,
                Password = "" //güvenlik nedeniyle göndermedim.
            }).ToList().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<User> GetAllUserWithoutPass()
        {
            return GetAll().Select(x => new User
            {
                Id = x.Id,
                Email = x.Email,
                Token = x.Token,
                Password = "" //güvenlik nedeniyle göndermedim.
            });
        }

        public User GetUserByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Email == name);
        }
    }
}