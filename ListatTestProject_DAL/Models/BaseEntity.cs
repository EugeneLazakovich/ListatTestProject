using System.ComponentModel.DataAnnotations;

namespace ListatTestProject_DAL.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
