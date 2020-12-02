using System.ComponentModel.DataAnnotations;

namespace EnesKARTALDigiAPI.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
