using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MoviceService : IMoviceService
    {
        private readonly ApplicationDbContext _context;

        public MoviceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;

        }

        public Movie Delete(Movie movie)
        {
            return _context.Movies.Remove(movie).Entity;
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
            
        }

        public async Task<IActionResult> GetByIdGenre(int id)
        {
          var genre = await _context.Movies.DefaultIfEmpty().Where(x => x.GenreId == id).ToListAsync();
               
                return new OkObjectResult(genre);
        }


        public async Task<IEnumerable<Movie>> GitAll()
        {
            return await _context.Movies.OrderBy(x =>x.rate).ToListAsync();
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
