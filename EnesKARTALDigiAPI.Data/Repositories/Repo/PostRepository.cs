using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EnesKARTALDigiAPI.Data.Repositories.Repo
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DigiBlogDBContext digiBlogDBContext) : base(digiBlogDBContext)
        {
        }

        public Post GetPostById(int id)
        {
            return GetAll().Include(t => t.Comments).FirstOrDefault(x => x.Id == id);
        }

        public List<Post> GetAllPosts()
        {
            return GetAll().Include(t => t.Comments).ToList();
        }
    }
}