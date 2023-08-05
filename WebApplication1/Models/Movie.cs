using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double rate { get; set; }
        [MaxLength(2500)]
        public string Storeline { get; set; } = null!;

        public byte[] poster { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
