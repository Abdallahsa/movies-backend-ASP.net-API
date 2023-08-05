using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IMoviceService
    {
        Task<IEnumerable<Movie>> GitAll();
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
        Genre GetByIdGenreAsync(int id);
        Task<IActionResult> GetByIdGenre(int id);
    }
}
