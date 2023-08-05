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
             _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
            
        }

        


        public async Task<IEnumerable<Movie>> GetAll(int id = 0)
        {
            return await _context.Movies.Include(x=>x.Genre).Where(x=>x.GenreId==id ||id==0).OrderBy(x =>x.rate).ToListAsync();
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
