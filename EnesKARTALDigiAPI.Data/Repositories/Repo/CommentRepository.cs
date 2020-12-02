using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using System.Linq;

namespace EnesKARTALDigiAPI.Data.Repositories.Repo
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DigiBlogDBContext digiBlogDBContext) : base(digiBlogDBContext)
        {
        }

        public Comment GetCommentById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}