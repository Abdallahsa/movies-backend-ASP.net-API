using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IMoviceService
    {
        Task<IEnumerable<Movie>> GetAll(int id=0);
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
        
    }
}
