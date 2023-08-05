using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private new List<string> _AllowedExtention=new List<string> {".png",".jpg"};
        private long _maxSize = 1024*1024;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GitAllAsync()
        {
            var movies = await _context.Movies.Include(x =>x.Genre).OrderBy(x => x.rate).ToListAsync();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync( id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpGet("GetByIdGenre/{id}")]
        public async Task<IActionResult> GetByIdGenreAsync(int id)
        {
          var genre=await _context.Movies.DefaultIfEmpty().Where(x=>x.GenreId==id).ToListAsync();
            if(genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMovieDto dto)
        {
            if (!_AllowedExtention.Contains(Path.GetExtension(dto.poster.FileName.ToLower()))){
                return BadRequest(" allow extention only file .png or .jpg");
            }
            if (dto.poster.Length > _maxSize)
            {
                return BadRequest("the maximum size is 1M ");
            }
             var _isValid=await _context.Genres.AnyAsync(x => x.Id == dto.GenreId);
            if (!_isValid)
            {
                return BadRequest("the genre is not valid");
            }
            using var dataStream = new MemoryStream();
            await dto.poster.CopyToAsync(dataStream);
            var movie = new Movie
            {
                Title = dto.Title,
                GenreId = dto.GenreId,
                Year = dto.Year,
                rate=dto.rate,
                Storeline = dto.Storeline,
                poster = dataStream.ToArray()
            };
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateMovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            if(dto.poster != null)
            {
                if (!_AllowedExtention.Contains(Path.GetExtension(dto.poster.FileName.ToLower())))
                {
                    return BadRequest(" allow extention only file .png or .jpg");
                }
                if (dto.poster.Length > _maxSize)
                {
                    return BadRequest("the maximum size is 1M ");
                }
                using var dataStream = new MemoryStream();
                await dto.poster.CopyToAsync(dataStream);
                movie.poster = dataStream.ToArray();
            }
            var _isValid = await _context.Genres.AnyAsync(x => x.Id == dto.GenreId);
            if (!_isValid)
            {
                return BadRequest("the genre is not valid");
            }
            
            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.rate = dto.rate;
            movie.Storeline = dto.Storeline;
           
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }
    }
}
