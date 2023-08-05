using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class ShowMovieDto
    {
       
        public string Title { get; set; } = null!;

        public int Year { get; set; }

        public double rate { get; set; }

        public string Storeline { get; set; } = null!;

        //public IFormFile poster { get; set; } = null!;

        public int GenreId { get; set; }
        public string GenreName { get; set;}
    }
}
