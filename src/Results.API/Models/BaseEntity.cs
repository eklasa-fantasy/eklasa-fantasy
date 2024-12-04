using System.ComponentModel.DataAnnotations;

namespace Results.API.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id {get; set;}
    }
}