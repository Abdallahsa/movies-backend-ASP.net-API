using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class CreateMovieDto
    {
        
        [MaxLength(250)]
        public string Title { get; set; } = null!;
       
        public int Year { get; set; }
        
        public double rate { get; set; }
        
        [MaxLength(2500)]
        public string Storeline { get; set; } = null!;
       
        public IFormFile poster { get; set; } = null!;
       
        public int GenreId { get; set; }
    }
}
