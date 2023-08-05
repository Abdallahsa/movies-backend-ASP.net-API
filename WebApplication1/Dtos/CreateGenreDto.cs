using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
