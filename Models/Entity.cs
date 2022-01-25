using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopsAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Entity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}
