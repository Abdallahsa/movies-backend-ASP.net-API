using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GitAll(int id=0);
        Task<Genre> GetById(int id);
        Task<Genre>  Create(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> Isvalid(int id);


    }
}
