using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<Genre> Create(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;

        }

        public async Task<Genre> Create(Genre genre)
        {
           await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<Genre> GetById(int id)
        {
             return await _context.Genres.FindAsync(id);
            
        }

        public async Task<IEnumerable<Genre>> GitAll(int id = 0)
        {
            return await _context.Genres.OrderBy(x => x.Name).ToListAsync();
        }

        public  Task<bool> Isvalid(int id)
        {
            return  _context.Genres.AnyAsync(x => x.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return genre;

        }
    }
}
