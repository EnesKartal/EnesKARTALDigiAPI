using EnesKARTALDigiAPI.Data.Models;

namespace EnesKARTALDigiAPI.Data.Repositories.Infra
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment GetCommentById(int id);
    }
}
