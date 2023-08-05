using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GitAll();
        Task<Genre> GetById(int id);
        Task<Genre>  Create(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        bool IsExists(int id);


    }
}
