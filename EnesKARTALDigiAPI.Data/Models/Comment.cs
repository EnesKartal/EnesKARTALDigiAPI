namespace EnesKARTALDigiAPI.Data.Models
{
    public partial class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public string Description { get; set; }
    }
}
