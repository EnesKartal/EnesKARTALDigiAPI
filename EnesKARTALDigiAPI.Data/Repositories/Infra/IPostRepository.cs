using EnesKARTALDigiAPI.Data.Models;
using System.Collections.Generic;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface IPostRepository : IRepository<Post>
    {
        Post GetPostById(int id);
        List<Post> GetAllPosts();
    }
}
