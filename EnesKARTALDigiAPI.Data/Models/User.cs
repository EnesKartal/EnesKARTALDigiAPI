namespace EnesKARTALDigiAPI.Data.Models
{
    public partial class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Token { get; set; }
    }
}
