using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Person
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}