using System.Collections.Generic;

namespace EnesKARTALDigiAPI.Data.Models
{
    public partial class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
